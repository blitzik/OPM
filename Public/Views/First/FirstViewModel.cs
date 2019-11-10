using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public.Views
{
    public class FirstViewModel : BaseScreen
    {
        private SecondViewModel _secondVieWModel;

        public FirstViewModel()
        {
            _secondVieWModel = new SecondViewModel();
        }


        public void ProcessClick()
        {
            LocalOverlay.DisplayOverlay(_secondVieWModel);
        }
    }
}
