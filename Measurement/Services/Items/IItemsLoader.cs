using System.Collections;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Measurement.Entities;

namespace Measurement.Services.Items
{
    public interface IItemsLoader
    {
        Task<ImmutableList<Item>> FindByOrderName(string orderName);
    }
}