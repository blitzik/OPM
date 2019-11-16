using Common.Factories;

namespace Public.Views
{
    public class StartupViewModelFactory : IViewModelFactory
    {
        private readonly MeasurementBaseViewModelFactory _measurementBaseViewModelFactory;
        private readonly MeasurementSettingsViewModelFactory _measurementSettingsViewModelFactory;


        public StartupViewModelFactory(
            MeasurementBaseViewModelFactory measurementBaseViewModelFactory,
            MeasurementSettingsViewModelFactory measurementSettingsViewModelFactory
        ) {
            _measurementBaseViewModelFactory = measurementBaseViewModelFactory;
            _measurementSettingsViewModelFactory = measurementSettingsViewModelFactory;
        }


        public StartupViewModel Create()
        {
            return new StartupViewModel(_measurementBaseViewModelFactory, _measurementSettingsViewModelFactory);
        }
    }
}