using Caliburn.Micro;

namespace Measurement.Entities
{
    public class MeasurementSettings : PropertyChangedBase
    {
        private Item _item;
        public Item Item
        {
            get => _item;
            private set { Set(ref _item, value); }
        }


        private bool _isMultimeterReset;
        public bool IsMultimeterReset
        {
            get => _isMultimeterReset;
            set { Set(ref _isMultimeterReset, value); }
        }
        

        public MeasurementSettings(Item item)
        {
            Item = item;
            IsMultimeterReset = false;
        }
    }
}