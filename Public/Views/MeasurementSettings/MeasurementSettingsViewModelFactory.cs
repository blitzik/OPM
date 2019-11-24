using Measurement.Entities;
using MSettings = Measurement.Entities.MeasurementSettings;

namespace Public.Views
{
    public class MeasurementSettingsViewModelFactory
    {
        private readonly OrderSelectionViewModelFactory _orderSelectionViewModelFactory;
        private readonly InitialMultimeterSettingsViewModelFactory _multimeterSettingsViewModelFactory;
        private readonly WavelengthSettingsViewModelFactory _wavelengthSettingsViewModelFactory;

        public MeasurementSettingsViewModelFactory(
            OrderSelectionViewModelFactory orderSelectionViewModelFactory,
            InitialMultimeterSettingsViewModelFactory multimeterSettingsViewModelFactory,
            WavelengthSettingsViewModelFactory wavelengthSettingsViewModelFactory
        ) {
            _orderSelectionViewModelFactory = orderSelectionViewModelFactory;
            _multimeterSettingsViewModelFactory = multimeterSettingsViewModelFactory;
            _wavelengthSettingsViewModelFactory = wavelengthSettingsViewModelFactory;
        }


        public MeasurementSettingsViewModel Create()
        {
            var vm = new MeasurementSettingsViewModel(
                _orderSelectionViewModelFactory.Create("Zvolení zakázky", "Zvolení zakázky")
            );
            vm.AddStep(_multimeterSettingsViewModelFactory.Create("Nastavení multimetru", "Nastavení multimetru"));
            vm.AddStep(_wavelengthSettingsViewModelFactory.Create("Nastavení vlnové délky", "Nastavení vlnové délky"));

            return vm;
        }
    }
}