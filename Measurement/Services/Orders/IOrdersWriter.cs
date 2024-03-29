﻿using System.Threading.Tasks;
using Common.Utils.ResultObject;
using Measurement.Entities;

namespace Measurement.Services.Orders
{
    public interface IOrdersWriter
    {
        Task<ResultObject<Order>> Save(Order order);
    }
}