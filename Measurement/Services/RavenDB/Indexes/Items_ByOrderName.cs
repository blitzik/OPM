using Measurement.Entities;
using Raven.Client.Documents.Indexes;
using System.Linq;

namespace Measurement.Services.RavenDB.Indexes
{
    public class Items_ByOrderName : AbstractIndexCreationTask<Item>
    {
        public Items_ByOrderName()
        {
            Map = items => from Item i in items
                select new
                {
                    Name = i.Order.Name
                };
        }
    }
}