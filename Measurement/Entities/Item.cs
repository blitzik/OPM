using System;
using SQLitePCL;

namespace Measurement.Entities
{
    public class Item
    {
        public int ItemId { get; private set; }
        
        public int OrderID { get; private set; }

        private Order _order;
        public Order Order
        {
            get => _order;
            set
            {
                _order = value;
                OrderID = value.OrderId;
            }
        }

        public int Number { get; private set; }
        
        
        private Item() {}

        public Item(Order order, int number)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            Order = order;
            Number = number;
        }
    }
}