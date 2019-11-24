using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Measurement.Entities;
using Measurement.Services.SQLite;

namespace Measurement.Services.Items.SqLite
{
    public class ItemsLoader : IItemsLoader
    {
        private readonly DatabaseContext _db;

        
        public ItemsLoader(DatabaseContext db)
        {
            _db = db;
        }


        public Task<ResultObject<Item>> GetByNumber(int orderId, int itemNumber)
        {
            return Task.Factory.StartNew(() =>
            {
                var item = _db.Items.Where(i => i.OrderID == orderId && i.Number == itemNumber).SingleOrDefault();
                
                return new ResultObject<Item>(true, item);
            });
        }

        
        public Task<ResultObject<ImmutableList<Item>>> FindByOrder(Order order)
        {
            return Task.Factory.StartNew(() =>
            {
                var result = _db.Items.Where(i => i.OrderID == order.OrderId).ToImmutableList();
                
                return new ResultObject<ImmutableList<Item>>(true, result);
            });
        }

        
        public Task<ResultObject<ImmutableList<Item>>> FindByOrder(int orderId)
        {
            return Task.Factory.StartNew(() =>
            {
                var result = _db.Items.Where(i => i.OrderID == orderId).ToImmutableList();
                
                return new ResultObject<ImmutableList<Item>>(true, result);
            });
        }
    }
}