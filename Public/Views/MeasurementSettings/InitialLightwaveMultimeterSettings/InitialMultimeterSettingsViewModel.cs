using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;
using Measurement.Entities;
using MSettings = Measurement.Entities.MeasurementSettings;

namespace Public.Views
{
    public class InitialMultimeterSettingsViewModel : BaseScreen, IStepViewModel<MSettings>
    {
        public string Title { get; }
        public string Label { get; }
        
        
        private bool _isDeviceMarkedForReset;
        public bool IsDeviceMarkedForReset
        {
            get { return _isDeviceMarkedForReset; }
            set { Set(ref _isDeviceMarkedForReset, value); }
        }
        
        
        public event Action OnRaiseCanExecuteChanged;
        
        public InitialMultimeterSettingsViewModel(string title, string label)
        {
            Title = title;
            Label = label;
            IsDeviceMarkedForReset = false;
        }


        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            var t = base.OnActivateAsync(cancellationToken);

            return t;
        }
        

        public void PrepareBeforeActivation(MeasurementSettings settings)
        {
            IsDeviceMarkedForReset = settings.IsMultimeterReset;
        }


        public bool CanProceed()
        {
            return true;
        }


        public MSettings ModifySettings(MeasurementSettings settings)
        {
            if (settings == null) throw new ArgumentNullException();

            settings.IsMultimeterReset = IsDeviceMarkedForReset;
            
            return settings;
        }
    }
}
