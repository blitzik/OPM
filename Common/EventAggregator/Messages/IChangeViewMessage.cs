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
        /// <summary>
        /// Tells whether a parent ViewModel should store the acquired child viewModel (and make it unique)
        /// </summary>
        public bool IsUnique { get; }
        
        public string Channel { get; }

        public Type ViewModel { get; }

        public Tvm GetViewModel(IViewModelFactoryResolver resolver);
    }
    
    
    /*public interface IChangeViewMessage<T>
    {
        Type Type { get; }
        T ViewModel { get; }

        void Apply(T viewModel);
    }*/
}
