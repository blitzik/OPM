using System.Collections.Generic;
using Caliburn.Micro;
using Common.EventAggregator.Messages;
using Common.ViewModels;
using System.Threading;
using System.Threading.Tasks;
using Common.Factories;
using Common.ViewModelResolver;

namespace Public.Views
{
    public class MainWindowViewModel :
        BaseConductorOneActive<IViewModel>,
        IHandle<IChangeViewMessage<IViewModelFactory, IViewModel>>
    {
        private readonly IViewModelFactoryResolver _viewModelFactoryResolver;
        private readonly IEventAggregator _eventAggregator;

        private Dictionary<string, IViewModel> _uniqueViewModels;
        
        private readonly StartupViewModel _startupViewModel;

       public MainWindowViewModel(
            IEventAggregator eventAggregator,
            IViewModelFactoryResolver viewModelFactoryResolver
       ) {
           _eventAggregator = eventAggregator;
           _viewModelFactoryResolver = viewModelFactoryResolver;
           _startupViewModel = new StartupViewModel();
           _uniqueViewModels = new Dictionary<string, IViewModel>();
       }


       protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var t = base.OnInitializeAsync(cancellationToken);

            _eventAggregator.SubscribeOnPublishedThread(this);

            ActivateItemAsync(_startupViewModel, cancellationToken);
            
            return t;
        }


        public Task HandleAsync(IChangeViewMessage<IViewModelFactory, IViewModel> message, CancellationToken cancellationToken)
        {
            if (!message.Channel.Equals(nameof(MainWindowViewModel)) && !message.Channel.Equals("all")) {
                return null;
            }

            IViewModel vm = null;
            if (_uniqueViewModels.ContainsKey(message.ViewModel.FullName)) {
                vm = _uniqueViewModels[message.ViewModel.FullName];

                if (message.IsViewModelUnique == false) _uniqueViewModels.Remove(message.ViewModel.FullName);
            }

            // Create new viewmodel when:
            // vm == null && IsUnique == true   YES
            // vm == null && IsUnique == false  YES
            // vm != null && IsUnique == true   NO
            // vm != null && IsUnique == false  YES
            if (vm == null || message.IsViewModelUnique == false) {
                vm = message.GetViewModel(_viewModelFactoryResolver);
                if (message.IsViewModelUnique == true) {
                    _uniqueViewModels.Add(message.ViewModel.FullName, vm);
                }
            }

            if (vm == ActiveItem) {
                return null;
            }
            
            ActivateItemAsync(vm, cancellationToken);

            return null;
        }
    }
}
