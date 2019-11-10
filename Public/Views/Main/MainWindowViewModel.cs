﻿using Caliburn.Micro;
using Common.FlashMessages;
using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Public.Views
{
    public class MainWindowViewModel : BaseScreen
    {
        private readonly IFlashMessagesManager _flashMessagesManager;

        public IFlashMessagesManager FlashMessagesManager
        {
            get { return _flashMessagesManager; }
        }

        public MainWindowViewModel(IFlashMessagesManager flashMessagesManager)
        {
            _flashMessagesManager = flashMessagesManager;
        }


        protected override Task OnInitializeAsync(CancellationToken cancellationToken)
        {
            var t = base.OnInitializeAsync(cancellationToken);
            

            return t;
        }

        public void ProcessClick()
        {
            LocalOverlay.DisplayOverlay(new TestViewModel());
        }
    }
}