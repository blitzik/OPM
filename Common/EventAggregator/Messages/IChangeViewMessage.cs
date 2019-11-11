using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ViewModelResolver;

namespace Common.EventAggregator.Messages
{
    public interface IChangeViewMessage<T, out Tvm>
    {
        public string Channel { get; }

        public Tvm GetViewModel(IViewModelFactoryResolver resolver);
        public void Apply();
    }
    
    
    /*public interface IChangeViewMessage<T>
    {
        Type Type { get; }
        T ViewModel { get; }

        void Apply(T viewModel);
    }*/
}
