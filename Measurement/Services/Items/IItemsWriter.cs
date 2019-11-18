using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Measurement.Entities;

namespace Measurement.Services.Items
{
    public interface IItemsWriter
    {
        Task<ResultObject<Item>> Save(Item item);
    }
}