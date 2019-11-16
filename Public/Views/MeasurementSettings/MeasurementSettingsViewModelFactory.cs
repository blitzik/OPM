using Measurement.Entities;
using MSettings = Measurement.Entities.MeasurementSettings;

namespace Public.Views
{
    public class MeasurementSettingsViewModelFactory
    {
        private readonly OrderSelectionViewModelFactory _orderSelectionViewModelFactory;

        
        public MeasurementSettingsViewModelFactory(
            OrderSelectionViewModelFactory orderSelectionViewModelFactory
        ) {
            _orderSelectionViewModelFactory = orderSelectionViewModelFactory;
        }


        public MeasurementSettingsViewModel Create()
        {
            var vm = new MeasurementSettingsViewModel(
                _orderSelectionViewModelFactory.Create("Zvolení zakázky", "Zvolení zakázky")
            );

            return vm;
        }
    }
}