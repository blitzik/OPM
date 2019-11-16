using System;
using Common.Utils.ResultObject;
using Measurement.Entities;
using Measurement.Services.RavenDB;
using Raven.Client.Documents.Session;

namespace Measurement.Services.Orders.RavenDB
{
    public class OrdersWriter : IOrdersWriter
    {
        private readonly IDocumentSession _db;

        
        public OrdersWriter(RavenDatabase db)
        {
            _db = db.GetSession();
        }
        

        public ResultObject<Order> Save(Order order)
        {
            ResultObject<Order> result;
            try {
                _db.Store(order);
                result = new ResultObject<Order>(true, order);
                
            } catch (Exception e) {
                result = new ResultObject<Order>(false);
                result.AddMessage("Při ukládání došlo k chybě");
            }

            return result;
        }
    }
}