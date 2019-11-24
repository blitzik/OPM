using System;
using System.Threading;
using System.Threading.Tasks;

namespace Public.Views
{
    public interface IStepViewModel<TSettings> : IViewModel
    {
        public string Title { get; }
        public string Label { get; }
        public bool IsActive { get; }

        bool CanProceed();

        event Action OnRaiseCanExecuteChanged;
        
        void PrepareBeforeActivation(TSettings settings);
        
        TSettings ModifySettings(TSettings settings);
    }
}