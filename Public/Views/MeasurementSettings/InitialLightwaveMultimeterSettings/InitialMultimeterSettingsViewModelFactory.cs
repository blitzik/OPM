using Common.Factories;

namespace Public.Views
{
    public class InitialMultimeterSettingsViewModelFactory : IViewModelFactory
    {
        public InitialMultimeterSettingsViewModelFactory()
        {
        }


        public InitialMultimeterSettingsViewModel Create(string title, string label)
        {
            return new InitialMultimeterSettingsViewModel(title, label);
        }
    }
}