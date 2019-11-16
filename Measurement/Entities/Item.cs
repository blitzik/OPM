using System;

namespace Measurement.Entities
{
    public class Item
    {
        public Order Order { get; set; }
        
        public int Number { get; set; }
        

        public Item(Order order, int number)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            Order = order;
            Number = number;
        }
    }
}