using System.Windows.Forms.VisualStyles;
using Common.Factories;

namespace Public.Views
{
    public class WavelengthSettingsViewModelFactory
    {

        public WavelengthSettingsViewModelFactory()
        {
            
        }


        public WavelengthSettingsViewModel Create(string title, string label)
        {
            return new WavelengthSettingsViewModel(title, label);
        }
    }
}