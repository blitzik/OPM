using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Measurement.Entities;

namespace Public.Views
{
    public class WavelengthSettingsViewModel : BaseScreen, IStepViewModel<MeasurementSettings>
    {
        public string Title { get; }
        public string Label { get; }
        
        
        public event Action OnRaiseCanExecuteChanged;
        
        public WavelengthSettingsViewModel(string title, string label)
        {
            Title = title;
            Label = label;
        }


        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            var t = base.OnActivateAsync(cancellationToken);

            return t;
        }
        

        public void PrepareBeforeActivation(MeasurementSettings settings)
        {
        }


        public bool CanProceed()
        {
            return true;
        }


        public MeasurementSettings ModifySettings(MeasurementSettings settings)
        {
            return settings;
        }
    }
}