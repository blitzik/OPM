using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Public.Views
{
    public class TestViewModel : BaseScreen
    {
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }


        private FirstViewModel _firstViewModel;


        public TestViewModel(string text)
        {
            Text = text;
            _firstViewModel = new FirstViewModel();
        }


        public void ProcessClick()
        {
            LocalOverlay.DisplayOverlay(_firstViewModel);
        }
    }
}
