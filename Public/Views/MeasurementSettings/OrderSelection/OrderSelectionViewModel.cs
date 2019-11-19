using System;
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
            set
            {
                Set(ref _orderName, value);
                OnRaiseCanExecuteChanged?.Invoke();
            }
        }


        private int? _itemNumber;
        public int? ItemNumber
        {
            get => _itemNumber;
            set
            {
                Set(ref _itemNumber, value);
                OnRaiseCanExecuteChanged?.Invoke();
            }
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
        
        
        private Order Order { get; set; }


        public event Action OnRaiseCanExecuteChanged;


        private readonly ContractFacade _contractFacade;
        public OrderSelectionViewModel(string title, string label, ContractFacade contractFacade)
        {
            Title = title;
            Label = label;
            _contractFacade = contractFacade;
            Items = new ObservableCollection<Item>();
        }
        
        
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            var t = base.OnActivateAsync(cancellationToken);

            OnRaiseCanExecuteChanged?.Invoke();
            
            return t;
        }


        public bool CanContinue()
        {
            return !string.IsNullOrEmpty(OrderName) && ItemNumber != null;
        }


        public bool CanGoBack()
        {
            return false;
        }


        public void PrepareBeforeActivation(MeasurementSettings settings)
        {
        }


        public Task<MSettings> ModifySettings(MSettings settings)
        {
            // todo
            return Task<MSettings>.Run(async () =>
            {
                if (Order == null) {
                    var result = await _contractFacade.SaveOrder(new Order(OrderName));
                    Order = result.Value;
                }

                if (SelectedItem == null) {
                    var result = await _contractFacade.SaveItem(new Item(Order, ItemNumber ?? 0));
                    SelectedItem = result.Value;
                }
                
                return new MSettings(SelectedItem);
            });
        }
        

        public Task DoBackgroundWork(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
        
        
        // -----


        public async void SearchForItems()
        {
            Items.Clear();
            ItemNumber = null;
            if (Order == null || !Order.Name.Equals(OrderName)) {
                var orderResult = await _contractFacade.GetOrderByName(OrderName);
                if (!orderResult.Success || orderResult.Value == null) {
                    return;
                }
                Order = orderResult.Value;
            }

            var test = Order.Items;
            
            var itemsResult = await _contractFacade.FindByOrder(Order);
            if (!itemsResult.Success || itemsResult.Value == null) {
                return;
            }
            
            Items.Refill(itemsResult.Value);
        }
    }
}