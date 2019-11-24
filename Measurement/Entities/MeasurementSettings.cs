using Caliburn.Micro;

namespace Measurement.Entities
{
    public class MeasurementSettings : PropertyChangedBase
    {
        public string OrderName { get; }
        public int ItemNumber { get; }
        

        private bool _isMultimeterReset;
        public bool IsMultimeterReset
        {
            get => _isMultimeterReset;
            set { Set(ref _isMultimeterReset, value); }
        }
        

        public MeasurementSettings(string orderName, int itemNumber)
        {
            OrderName = orderName;
            ItemNumber = itemNumber;
            IsMultimeterReset = false;
        }
    }
}