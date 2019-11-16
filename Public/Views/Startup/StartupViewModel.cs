using System.Threading;
using System.Threading.Tasks;

namespace Public.Views
{
    public class StartupViewModel : BaseConductorOneActive
    {
        private MeasurementBaseViewModel _measurementBaseViewModel;
        public MeasurementBaseViewModel MeasurementBaseViewModel
        {
            get => _measurementBaseViewModel;
            private set => Set(ref _measurementBaseViewModel, value);
        }


        private MeasurementSettingsViewModel _measurementSettingsViewModel;
        public MeasurementSettingsViewModel MeasurementSettingsViewModel
        {
            get => _measurementSettingsViewModel;
            set => Set(ref _measurementSettingsViewModel, value);
        }
        

        public StartupViewModel(
            MeasurementBaseViewModelFactory measurementBaseViewModelFactory,
            MeasurementSettingsViewModelFactory measurementSettingsViewModelFactory
        ) {
            MeasurementBaseViewModel = measurementBaseViewModelFactory.Create();
            MeasurementSettingsViewModel = measurementSettingsViewModelFactory.Create();
        }
        
        
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var t = base.OnInitializeAsync(cancellationToken);

            ActivateItemAsync(MeasurementSettingsViewModel, cancellationToken);
            
            return t;
        }
    }
}