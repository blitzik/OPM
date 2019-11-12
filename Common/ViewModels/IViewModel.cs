using Common.Overlay;
using System;

namespace Common.ViewModels
{
    public interface IViewModel
    {
        public string Context { get; set; }
        public event Action OnOverlayExitClick;
        public void CloseOverlay();
    }
}
