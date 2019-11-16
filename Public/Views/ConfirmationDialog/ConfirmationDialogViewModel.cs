using System;
using System.Threading.Tasks;
using Common.Overlay;

namespace Public.Views.ConfirmationDialog
{
    public class ConfirmationDialogViewModel : BaseScreen
    {
        public string Text { get; set; }
        
        public string ConfirmationButtonCaption { get; set; }
        
        public string CancelButtonCaption { get; set; }
        
        
        public ConfirmationDialogViewModel(string text)
        {
            Text = text;
            ConfirmationButtonCaption = "Ok";
            CancelButtonCaption = "Zrušit";
        }

        /// <summary>
        /// Call this method if you want to wait for user response
        /// </summary>
        /// <param name="activeOverlayToken"></param>
        /// <returns>TRUE if user confirmed, otherwise FALSE</returns>
        public async Task<bool> WaitForConfirmationAsync(IOverlayToken activeOverlayToken)
        {
            bool? result = null;
            
            activeOverlayToken.OnOverlayHide += (token) => { result = false; };

            OnCancelClicked += activeOverlayToken.HideOverlay;
            OnConfirmationClicked += () =>
            {
                activeOverlayToken.HideOverlay(); // must be called before result = true (otherwise result would be always false)
                result = true;
            };
            
            while (result == null)
            {
                await Task.Delay(300);
            }
            
            return result ?? false;
        }
        
        
        public event Action OnConfirmationClicked;
        public void ProcessActions()
        {
            OnConfirmationClicked?.Invoke();
        }


        public event Action OnCancelClicked;
        public void Cancel()
        {
            OnCancelClicked?.Invoke();
        }
    }
}