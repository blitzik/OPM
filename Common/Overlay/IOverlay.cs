﻿using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Overlay
{
    public interface IOverlay
    {
        IOverlayToken Token { get; }
        bool IsActive { get; }

        IOverlayToken DisplayOverlay(IOverlayToken overlayToken);
        IOverlayToken DisplayOverlay<VM>(VM content, bool isMandatory = false, Action<IOverlayToken> onHideAction = null) where VM : IViewModel;
        
        
        /// <summary>
        /// Hides overlay and deactivates currently active Token
        /// </summary>
        void HideOverlay();
    }
}
