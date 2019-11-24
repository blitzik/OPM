using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Commands;

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


        private DelegateCommand<object> _returnBackCommand;
        public DelegateCommand<object> ReturnBackCommand
        {
            get
            {
                if (_returnBackCommand == null) {
                    _returnBackCommand = new DelegateCommand<object>(
                        p => LoadPrevious()
                    );
                }

                return _returnBackCommand;
            }
        }


        private DelegateCommand<object> _continueCommand;
        public DelegateCommand<object> ContinueCommand
        {
            get
            {
                if (_continueCommand == null) {
                    _continueCommand = new DelegateCommand<object>(
                        p => LoadNext(),
                        p => CanLoadNext()
                    );
                }

                return _continueCommand;
            }
        }


        public TSettings Settings { get; protected set; }

        public event Action<TSettings> OnFinishedSettings;
        
        private bool _isLocked;
        private int _cursor;
        public BaseStepSettingsViewModel(IStepViewModel<TSettings> viewModel)
        {
            Steps = new List<IStepViewModel<TSettings>>();
            AddStep(viewModel);
            _isLocked = false;
            _cursor = 0;
        }


        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var t = base.OnInitializeAsync(cancellationToken);

            _isLocked = true;
            CheckButtonsState();
            
            ActivateItemAsync(Steps.First(), cancellationToken);

            return t;
        }


        public BaseStepSettingsViewModel<TSettings> AddStep(IStepViewModel<TSettings> step)
        {
            if (step == null) throw new ArgumentNullException(nameof(step));
            if (_isLocked) throw new InvalidOperationException("ViewModel cannot be added after object is activated.");
            step.OnRaiseCanExecuteChanged += () =>
            {
                ContinueCommand.RaiseCanExecuteChanged();
                ReturnBackCommand.RaiseCanExecuteChanged();
            };
            Steps.Add(step);

            return this;
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


        private bool CanLoadNext()
        {
            return Steps.ElementAt(_cursor).CanProceed();
        }
        

        private async void LoadNext()
        {
            var currentVM = Steps.ElementAt(_cursor);
            // Apply changes to the settings by currently activated ViewModel
            Settings = currentVM.ModifySettings(Settings);

            // move to the next step of settings
            _cursor++;
            CheckButtonsState();
            var nextVM = Steps.ElementAt(_cursor);
            nextVM.PrepareBeforeActivation(Settings);
            ActivateItemAsync(nextVM, CancellationToken.None);
        }

        
        private void LoadPrevious()
        {
            _cursor--;
            CheckButtonsState();
            Steps.ElementAt(_cursor).PrepareBeforeActivation(Settings);
            ActivateItemAsync(Steps.ElementAt(_cursor), CancellationToken.None);
        }


        private void FinishSettings()
        {
            Steps.ElementAt(Steps.Count - 1).ModifySettings(Settings);

            OnFinishedSettings?.Invoke(Settings);
        }
    }
}