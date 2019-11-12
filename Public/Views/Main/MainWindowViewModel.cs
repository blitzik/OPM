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

       public MainWindowViewModel(
            IEventAggregator eventAggregator,
            IViewModelFactoryResolver viewModelFactoryResolver
       ) {
           _eventAggregator = eventAggregator;
           _viewModelFactoryResolver = viewModelFactoryResolver;
       }


       protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var t = base.OnInitializeAsync(cancellationToken);

            _eventAggregator.SubscribeOnPublishedThread(this);

            return t;
        }


        public Task HandleAsync(IChangeViewMessage<IViewModelFactory, IViewModel> message, CancellationToken cancellationToken)
        {
            if (!message.Channel.Equals(nameof(MainWindowViewModel)) && !message.Channel.Equals("all")) {
                return null;
            }
            
            var vm = message.GetViewModel(_viewModelFactoryResolver);
            if (vm == ActiveItem) {
                return null;
            }
            
            ActivateItemAsync(vm, cancellationToken);

            return null;
        }
    }
}
