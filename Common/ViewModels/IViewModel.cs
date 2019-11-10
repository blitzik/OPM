using Common.Overlay;
using System;

namespace Common.ViewModels
{
    public interface IViewModel
    {
        public event Action OnOverlayExitClick;
        public void CloseOverlay();
    }
}
