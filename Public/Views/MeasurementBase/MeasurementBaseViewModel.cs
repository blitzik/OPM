using System.Threading;
using System.Threading.Tasks;

namespace Public.Views
{
    public class MeasurementBaseViewModel : BaseConductorAllActive
    {
        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var t = base.OnInitializeAsync(cancellationToken);

            
            
            return t;
        }
    }
}