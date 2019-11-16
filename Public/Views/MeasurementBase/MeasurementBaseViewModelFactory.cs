using Common.Factories;

namespace Public.Views
{
    public class MeasurementBaseViewModelFactory : IViewModelFactory
    {
        public MeasurementBaseViewModelFactory()
        {
        }


        public MeasurementBaseViewModel Create()
        {
            return new MeasurementBaseViewModel();
        }
    }
}