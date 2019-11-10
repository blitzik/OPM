using Caliburn.Micro;
using Common.ViewModelResolver;
using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common.Validation;
using Common.FlashMessages;
using Common.Overlay;
using System.Threading;
using System.Threading.Tasks;

namespace Common.ViewModels
{
    public abstract class BaseConductorAllActive<P> : Conductor<P>.Collection.AllActive, IViewModel, INotifyDataErrorInfo where P : class, IViewModel
    {
        private IOverlay _localOverlay;
        public IOverlay LocalOverlay
        {
            get { return _localOverlay; }
        }


        private IValidationObject _validation;
        protected IValidationObject Validation
        {
            get { return _validation; }
        }


        public BaseConductorAllActive()
        {
            _localOverlay = new Overlay.Overlay();
            _validation = new ValidationObject();
            _validation.ErrorsChanged += (object sender, DataErrorsChangedEventArgs e) => {
                ErrorsChanged?.Invoke(this, e);
            };
        }


        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var t = base.OnInitializeAsync(cancellationToken);

            InitializeValidation();

            return t;
        }


        // -----


        public event System.Action OnOverlayExitClick;

        public void CloseOverlay()
        {
            OnOverlayExitClick?.Invoke();
        }


        // ----- INotifyDataErrorInfo


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected void RaiseErrorsChanged([CallerMemberName]string propertyName = "")
        {
            Validation.RaiseErrorsChanged(propertyName);
        }


        protected virtual void InitializeValidation()
        {
        }


        public bool HasErrors
        {
            get { return Validation.HasErrors; }
        }


        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName)) {
                // todo
                return new List<string>();
            }

            if (!Validation.Errors.ContainsKey(propertyName)) {
                return new List<string>();
            }

            return Validation.Errors[propertyName];
        }
    }
}
