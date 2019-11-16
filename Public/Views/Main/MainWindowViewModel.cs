using System.Collections.Generic;
using Caliburn.Micro;
using Common.EventAggregator.Messages;
using System.Threading;
using System.Threading.Tasks;
using Common.Factories;
using Common.ViewModelResolver;

namespace Public.Views
{
    public class MainWindowViewModel :
        BaseConductorOneActive,
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
           _uniqueViewModels = new Dictionary<string, IViewModel>();
           _startupViewModel = _viewModelFactoryResolver.Resolve<StartupViewModelFactory>().Create();
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
            string key = message.ViewModel.FullName;
            if (_uniqueViewModels.ContainsKey(key)) {
                vm = _uniqueViewModels[key];

                if (message.IsViewModelUnique == false) _uniqueViewModels.Remove(key);
            }

            // Create new viewmodel when:
            // vm == null && IsUnique == true   YES
            // vm == null && IsUnique == false  YES
            // vm != null && IsUnique == true   NO
            // vm != null && IsUnique == false  YES
            if (vm == null || message.IsViewModelUnique == false) {
                vm = message.GetViewModel(_viewModelFactoryResolver);
                if (message.IsViewModelUnique == true) {
                    _uniqueViewModels.Add(key, vm);
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
