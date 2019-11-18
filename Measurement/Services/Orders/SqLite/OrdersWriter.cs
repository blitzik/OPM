using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Measurement.Entities;
using Measurement.Services.SQLite;

namespace Measurement.Services.Orders.SqLite
{
    public class OrdersWriter : IOrdersWriter
    {
        private readonly DatabaseContext _db;

        
        public OrdersWriter(DatabaseContext db)
        {
            _db = db;
        }
        
        
        public Task<ResultObject<Order>> Save(Order order)
        {
            return Task.Factory.StartNew(() =>
            {
                _db.Orders.Update(order);
                _db.SaveChanges();
                return new ResultObject<Order>(true, order); 
            });
        }
    }
}