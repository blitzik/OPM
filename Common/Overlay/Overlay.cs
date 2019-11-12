using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;
using Common.Commands;
using Common.ViewModels;

namespace Common.Overlay
{
    public enum State
    {
        VISIBLE,
        HIDDEN
    }


    public class Overlay : PropertyChangedBase, IOverlay
    {
        private IOverlayToken _token;
        public IOverlayToken Token
        {
            get { return _token; }
        }


        public bool IsActive
        {
            get { return _token != null && _token.IsActive == true; }
        }


        private DelegateCommand<object> _hideOverlayCommand;
        public DelegateCommand<object> HideOverlayCommand
        {
            get
            {
                if (_hideOverlayCommand == null) {
                    _hideOverlayCommand = new DelegateCommand<object>(p => {
                        if (!Token.IsMandatory) {
                            HideOverlay();
                        }
                    });
                }
                return _hideOverlayCommand;
            }
        }


        public IOverlayToken DisplayOverlay(IOverlayToken token)
        {
            if (token == null) throw new ArgumentNullException();

            PrepareToken(token);

            return token;
        }


        public IOverlayToken DisplayOverlay<VM>(VM content, bool isMandatory = false, Action<IOverlayToken> onHideAction = null) where VM : IViewModel
        {
            if (content == null) throw new ArgumentNullException();

            IOverlayToken token = new OverlayToken(content, isMandatory);
            if (onHideAction != null) {
                token.OnOverlayHide += onHideAction;
            }
            
            PrepareToken(token);

            return token;
        }


        private void PrepareToken(IOverlayToken token)
        {
            token.OnOverlayHide += (t) => {
                if (t != Token) { // only current active token can disable overlay
                    return;
                }
                
                NotifyOfPropertyChange(() => Token);
                NotifyOfPropertyChange(() => IsActive);
            };

            _token = token;
            _token.Content.OnOverlayExitClick += () => {
                _token.HideOverlay();
            };
            NotifyOfPropertyChange(() => Token);
            NotifyOfPropertyChange(() => IsActive);

            if (token.Content != null) {
                ScreenExtensions.TryActivateAsync(token.Content);
            }
        }


        public void HideOverlay()
        {
            if (Token != null) Token.HideOverlay();
        }
    }
}
