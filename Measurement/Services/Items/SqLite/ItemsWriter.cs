using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Measurement.Entities;
using Measurement.Services.SQLite;

namespace Measurement.Services.Items.SqLite
{
    public class ItemsWriter : IItemsWriter
    {
        private readonly DatabaseContext _db;

        
        public ItemsWriter(DatabaseContext db)
        {
            _db = db;
        }
        
        
        public Task<ResultObject<Item>> Save(Item item)
        {
            return Task.Factory.StartNew(() =>
            {
                _db.Update(item);
                _db.SaveChanges();
                return new ResultObject<Item>(true, item);
            });
        }
    }
}