using Measurement.Facades;

namespace Public.Views
{
    public class OrderSelectionViewModelFactory
    {
        private readonly ContractFacade _contractFacade;

        
        public OrderSelectionViewModelFactory(ContractFacade contractFacade)
        {
            _contractFacade = contractFacade;
        }


        public OrderSelectionViewModel Create(string title, string label)
        {
            return new OrderSelectionViewModel(title, label, _contractFacade);
        }
    }
}