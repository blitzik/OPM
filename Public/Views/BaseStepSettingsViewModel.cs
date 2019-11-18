using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Public.Views
{
    public abstract class BaseStepSettingsViewModel<TSettings> : BaseConductorOneActive
    {
        private List<IStepViewModel<TSettings>> _steps;
        public List<IStepViewModel<TSettings>> Steps
        {
            get => _steps;
            set { _steps = value; }
        }


        public TSettings Settings { get; protected set; }
        
        
        public Dictionary<int, Task> BackgroundWorks { get; }
        public Dictionary<int, CancellationTokenSource> CancellationTokenSources { get; }

        public event Action<TSettings> OnFinishedSettings;
        
        private bool _isLocked;
        private int _cursor;
        public BaseStepSettingsViewModel(IStepViewModel<TSettings> viewModel)
        {
            Steps = new List<IStepViewModel<TSettings>>();
            AddStep(viewModel);
            _isLocked = false;
            _cursor = 0;
            BackgroundWorks = new Dictionary<int, Task>();
            CancellationTokenSources = new Dictionary<int, CancellationTokenSource>();
        }


        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var t = base.OnInitializeAsync(cancellationToken);

            _isLocked = true;
            CheckButtonsState();
            
            ActivateItemAsync(Steps.First(), cancellationToken);

            return t;
        }


        public void AddStep(IStepViewModel<TSettings> step)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));
            if (_isLocked) throw new InvalidOperationException("ViewModel cannot be added after object is activated.");
            Steps.Add(step);
        }
        
        
        // ----- BUTTONS


        public bool IsPreviousButtonVisible { get; set; }
        public bool IsNextButtonVisible { get; set; }
        public bool IsFinishButtonVisible { get; set; }


        private void CheckButtonsState()
        {
            IsPreviousButtonVisible = true;
            IsNextButtonVisible = true;
            IsFinishButtonVisible = false;
            
            if (_cursor == 0) {
                IsPreviousButtonVisible = false;
            }

            if (_cursor == Steps.Count - 1) {
                IsNextButtonVisible = false;
                IsFinishButtonVisible = true;
            }
            
            NotifyOfPropertyChange(nameof(IsPreviousButtonVisible));
            NotifyOfPropertyChange(nameof(IsNextButtonVisible));
            NotifyOfPropertyChange(nameof(IsFinishButtonVisible));
        }
        
        
        public bool CanLoadNext()
        {
            if (_cursor == Steps.Count - 1) return false; 
            return Steps.ElementAt(_cursor).CanContinue();
        }
        

        public async void LoadNext()
        {
            var currentVM = Steps.ElementAt(_cursor);
            // Apply changes to the settings by currently activated ViewModel
            var t = currentVM.ModifySettings(Settings);
            
            // Start its background task
            CancellationTokenSource ct = new CancellationTokenSource();
            CancellationTokenSources.Add(_cursor, ct);
            BackgroundWorks.Add(_cursor, currentVM.DoBackgroundWork(ct.Token));

            Settings = await t; // we need to wait for changed settings
            
            // move to the next step of settings
            _cursor++;
            CheckButtonsState();
            var nextVM = Steps.ElementAt(_cursor);
            nextVM.PrepareBeforeActivation(Settings);
            ActivateItemAsync(nextVM, CancellationToken.None);
        }


        public bool CanLoadPrevious()
        {
            if (_cursor == 0) return false;
            return Steps.ElementAt(_cursor).CanGoBack();
        }
        
        
        public void LoadPrevious()
        {
            // Cancel the background Task of previous Step
            var ct = CancellationTokenSources[_cursor - 1];
            CancellationTokenSources.Remove(_cursor - 1);
            BackgroundWorks.Remove(_cursor - 1);
            ct.Cancel();
            ct.Dispose();
            
            // Move to the previous Step
            _cursor--;
            CheckButtonsState();
            Steps.ElementAt(_cursor).PrepareBeforeActivation(Settings);
            ActivateItemAsync(Steps.ElementAt(_cursor), CancellationToken.None);
        }


        public void FinishSettings()
        {
            Steps.ElementAt(Steps.Count - 1).ModifySettings(Settings);
            
            // todo check for exceptions in background tasks
            Task.WaitAll(BackgroundWorks.Values.ToArray());
            
            OnFinishedSettings?.Invoke(Settings);
        }
    }
}