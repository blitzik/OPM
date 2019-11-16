using System.Collections.Immutable;
using System.Threading.Tasks;
using Measurement.Entities;
using Measurement.Services.RavenDB;
using Measurement.Services.RavenDB.Indexes;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace Measurement.Services.Items.RavenDB
{
    public class ItemsLoader : IItemsLoader
    {
        private IDocumentSession _db;


        public ItemsLoader(RavenDatabase db)
        {
            _db = db.GetSession();
        }
        

        public Task<ImmutableList<Item>> FindByOrderName(string orderName)
        {
            return Task<ImmutableList<Item>>.Factory.StartNew(() =>
            {
                return _db.Query<Item, Items_ByOrderName>().Where(x => x.Order.Name == orderName, false).ToImmutableList();
            });
        }
    }
}