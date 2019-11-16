using Common.Utils.ResultObject;
using Measurement.Entities;

namespace Measurement.Services.Orders
{
    public interface IOrdersWriter
    {
        ResultObject<Order> Save(Order order);
    }
}