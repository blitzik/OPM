using Measurement.Entities;
using MSettings = Measurement.Entities.MeasurementSettings;

namespace Public.Views
{
    public class MeasurementSettingsViewModelFactory
    {
        private readonly OrderSelectionViewModelFactory _orderSelectionViewModelFactory;
        private readonly InitialMultimeterSettingsViewModelFactory _multimeterSettingsViewModelFactory;


        public MeasurementSettingsViewModelFactory(
            OrderSelectionViewModelFactory orderSelectionViewModelFactory,
            InitialMultimeterSettingsViewModelFactory multimeterSettingsViewModelFactory
        ) {
            _orderSelectionViewModelFactory = orderSelectionViewModelFactory;
            _multimeterSettingsViewModelFactory = multimeterSettingsViewModelFactory;
        }


        public MeasurementSettingsViewModel Create()
        {
            var vm = new MeasurementSettingsViewModel(
                _orderSelectionViewModelFactory.Create("Zvolení zakázky", "Zvolení zakázky")
            );
            vm.AddStep(_multimeterSettingsViewModelFactory.Create("Nastavení multimetru", "Nastavení multimetru"));

            return vm;
        }
    }
}