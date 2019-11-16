using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Common.Factories;
using Common.ViewModelResolver;

namespace Common.EventAggregator.Messages
{
    public class ChangeViewMessage<T, Tvm> : IChangeViewMessage<IViewModelFactory, Tvm>
        where Tvm : IViewModel
        where T : IViewModelFactory
    {
        public bool IsViewModelUnique { get; }
        public Type ViewModel { get; }
        public string Channel { get; }
        

        private readonly Func<T, Tvm> _builder;
        private readonly Type _viewModelFactoryType;

        public ChangeViewMessage(Func<T, Tvm> builder, bool isUnique = false, string channel = "all")
        {
            _viewModelFactoryType = typeof(T);
            ViewModel = typeof(Tvm);
            IsViewModelUnique = isUnique;
            Channel = channel;
            _builder = builder;
        }
        
        
        public Tvm GetViewModel(IViewModelFactoryResolver resolver)
        {
            T f = resolver.Resolve<T>();
            return _builder.Invoke(f);
        }
    }
    
    
    /*public class ChangeViewMessage<T> : IChangeViewMessage<IViewModel> where T : IViewModel
    {
        private Type _type;
        public Type Type
        {
            get { return _type; }
        }
        
        
        protected IViewModel _viewModel;
        public IViewModel ViewModel
        {
            get { return _viewModel; }
        }


        private Action<T> _action;
        
        
        public ChangeViewMessage(Action<T> actionBeforeActivation = null)
        {
            _type = typeof(T);
            _action = actionBeforeActivation;
        }

        public ChangeViewMessage(T viewModel, Action<T> actionBeforeActivation = null)
        {
            _viewModel = viewModel;
            _action = actionBeforeActivation;
        }


        public void Apply(IViewModel viewModel)
        {
            _action?.Invoke((T)viewModel);
        }
    }*/
}
