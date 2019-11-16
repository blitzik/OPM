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
        private readonly IItemsLoader _itemsLoader;


        public ContractFacade(
            IOrdersWriter ordersWriter,
            IItemsLoader itemsLoader
        ) {
            _ordersWriter = ordersWriter;
            _itemsLoader = itemsLoader;
        }


        public ResultObject<Order> SaveOrder(Order order)
        {
            return _ordersWriter.Save(order);
        }


        public Task<ImmutableList<Item>> FindItemsByOrderName(string orderName)
        {
            return _itemsLoader.FindByOrderName(orderName);
        }
    }
}