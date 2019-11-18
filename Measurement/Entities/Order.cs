using System;
using System.Collections.Generic;

namespace Measurement.Entities
{
    public class Order
    {
        public int OrderId { get; private set; }
        
        
        private string _name;
        public string Name
        {
            get => _name;
            set { _name = value; }
        }
        
        
        public List<Item> Items { get; private set; }


        private Order() {}
        
        public Order(string name)
        {
            Name = name ?? throw new ArgumentNullException();
            Items = new List<Item>(); 
        }
    }
}