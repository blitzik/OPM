using System.Collections;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Measurement.Entities;

namespace Measurement.Services.Items
{
    public interface IItemsLoader
    {
        Task<ResultObject<ImmutableList<Item>>> FindByOrder(Order order);
        Task<ResultObject<ImmutableList<Item>>> FindByOrder(int orderId);
        Task<ResultObject<Item>> GetByNumber(int orderId, int itemNumber);
    }
}