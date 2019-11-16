using MSettings = Measurement.Entities.MeasurementSettings;

namespace Public.Views
{
    public class MeasurementSettingsViewModel : BaseStepSettingsViewModel<MSettings>
    {
        public MeasurementSettingsViewModel(IStepViewModel<MSettings> viewModel) : base(viewModel)
        {
        }

    }
}