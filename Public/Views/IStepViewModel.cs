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

        bool CanContinue();
        bool CanGoBack();

        void PrepareBeforeActivation(TSettings settings);
        
        Task<TSettings> ModifySettings(TSettings settings);
        Task DoBackgroundWork(CancellationToken cancellationToken);
    }
}