namespace Measurement.Entities
{
    public class Order
    {
        public string _name;
        public string Name
        {
            get => _name;
            set { _name = value; }
        }
    }
}