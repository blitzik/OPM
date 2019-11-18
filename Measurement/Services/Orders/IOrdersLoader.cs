using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Measurement.Entities;

namespace Measurement.Services.Orders
{
    public interface IOrdersLoader
    {
        Task<ResultObject<Order>> GetByName(string name);
    }
}