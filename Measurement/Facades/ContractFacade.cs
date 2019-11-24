using System.Collections.Immutable;
using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Measurement.Entities;
using Measurement.Services.Items;
using Measurement.Services.Orders;

namespace Measurement.Facades
{
    public class ContractFacade
    {
        private readonly IOrdersWriter _ordersWriter;
        private readonly IOrdersLoader _ordersLoader;
        private readonly IItemsWriter _itemsWriter;
        private readonly IItemsLoader _itemsLoader;


        public ContractFacade(
            IOrdersWriter ordersWriter,
            IOrdersLoader ordersLoader,
            IItemsWriter itemsWriter,
            IItemsLoader itemsLoader
        ) {
            _ordersWriter = ordersWriter;
            _ordersLoader = ordersLoader;
            _itemsWriter = itemsWriter;
            _itemsLoader = itemsLoader;
        }


        public Task<ResultObject<Order>> SaveOrder(Order order)
        {
            return _ordersWriter.Save(order);
        }


        public Task<ResultObject<Item>> SaveItem(Item item)
        {
            return _itemsWriter.Save(item);
        }


        public Task<ResultObject<Order>> GetOrderByName(string name)
        {
            return _ordersLoader.GetByName(name);
        }


        public Task<ResultObject<Item>> GetByNumber(int orderId, int itemNumber)
        {
            return _itemsLoader.GetByNumber(orderId, itemNumber);
        }
        

        public Task<ResultObject<ImmutableList<Item>>> FindByOrder(Order order)
        {
            return _itemsLoader.FindByOrder(order);
        }

        
        public Task<ResultObject<ImmutableList<Item>>> FindByOrder(int orderId)
        {
            return _itemsLoader.FindByOrder(orderId);
        }

    }
}