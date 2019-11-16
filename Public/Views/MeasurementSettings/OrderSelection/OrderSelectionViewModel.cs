using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Measurement.Entities;
using Measurement.Facades;
using Common.ExtensionMethods;
using MSettings = Measurement.Entities.MeasurementSettings;

namespace Public.Views
{
    public class OrderSelectionViewModel : BaseScreen, IStepViewModel<MSettings>
    {
        public string Title { get; }
        public string Label { get; }
        
        
        // -----


        private string _orderName;
        public string OrderName
        {
            get => _orderName;
            set => Set(ref _orderName, value);
        }


        private int _itemNumber;
        public int ItemNumber
        {
            get => _itemNumber;
            set { Set(ref _itemNumber, value); }
        }


        private ObservableCollection<Item> _items;
        public ObservableCollection<Item> Items
        {
            get => _items;
            private set => _items = value;
        }


        private Item _selectedItem;
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                Set(ref _selectedItem, value);
                ItemNumber = value.Number;
            }
        }


        private readonly ContractFacade _contractFacade;
        public OrderSelectionViewModel(string title, string label, ContractFacade contractFacade)
        {
            Title = title;
            Label = label;
            _contractFacade = contractFacade;
            Items = new ObservableCollection<Item>();
        }


        public bool CanContinue()
        {
            return true;
        }


        public bool CanGoBack()
        {
            return true;
        }


        public void PrepareBeforeActivation(MeasurementSettings settings)
        {
            
        }


        public Task<MSettings> ModifySettings(MSettings settings)
        {
            return Task<MSettings>.Factory.StartNew(() =>
            {
                /*if (settings == null) {
                    settings = new MSettings(); 
                } */
                return new MSettings(new Item(new Order(), 10));
            });
        }
        

        public Task DoBackgroundWork(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
        
        // -----


        public async void SearchForItems()
        {
            var items = await _contractFacade.FindItemsByOrderName(OrderName);
            Items.Refill(items);
        }
    }
}