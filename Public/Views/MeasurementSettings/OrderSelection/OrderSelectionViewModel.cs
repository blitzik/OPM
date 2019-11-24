using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Measurement.Entities;
using Measurement.Facades;
using Common.ExtensionMethods;
using Common.Utils;
using Common.Utils.ResultObject;
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
        public string ItemNumber
        {
            get => _itemNumber.ToString();
            set
            {
                Set(ref _itemNumber, IntegersOnlyUtils.ConvertOnlyPositive(value, _itemNumber, true));
                if (string.IsNullOrEmpty(value)) SelectedItem = null;
                if (SelectedItem != null && !SelectedItem.Number.ToString().Equals(value)) SelectedItem = null;
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
                do {
                    if (value == null) break;
                    ItemNumber = value.Number.ToString();
                } while (false);
                OnRaiseCanExecuteChanged?.Invoke();
            }
        }
        
        
        private Order Order { get; set; }
        

        private readonly ContractFacade _contractFacade;

        public event Action OnRaiseCanExecuteChanged;
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

            return t;
        }


        public void PrepareBeforeActivation(MeasurementSettings settings)
        {
        }


        public bool CanProceed()
        {
            return !string.IsNullOrEmpty(OrderName) && !string.IsNullOrEmpty(ItemNumber);
        }


        public MSettings ModifySettings(MSettings settings)
        {
            return new MSettings(OrderName, int.Parse(ItemNumber));
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

            var itemsResult = await _contractFacade.FindByOrder(Order);
            if (!itemsResult.Success || itemsResult.Value == null) {
                return;
            }
            
            Items.Refill(itemsResult.Value);
        }
    }
}