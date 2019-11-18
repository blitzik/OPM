using System.CodeDom;
using System.Linq;
using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Measurement.Entities;
using Measurement.Services.SQLite;

namespace Measurement.Services.Orders.SqLite
{
    public class OrdersLoader : IOrdersLoader
    {
        private readonly DatabaseContext _db;

        
        public OrdersLoader(DatabaseContext db)
        {
            _db = db;
        }
        
        
        public Task<ResultObject<Order>> GetByName(string name)
        {
            return Task.Factory.StartNew(() =>
            {
                Order o = _db.Orders.SingleOrDefault(o => o.Name == name);
                return new ResultObject<Order>(true, o);
            });
        }
    }
}