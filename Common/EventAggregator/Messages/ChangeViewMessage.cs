using Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Common.Factories;
using Common.ViewModelResolver;

namespace Common.EventAggregator.Messages
{
    public class ChangeViewMessage<T, Tvm> : IChangeViewMessage<IViewModelFactory, Tvm>
        where T : IViewModelFactory
    {
        public string Channel { get; }


        private readonly Func<T, Tvm> _builder;
        private readonly Action<Tvm> _action;

        private readonly Type _viewModelFactoryType;
        private Tvm _viewModel;
        
        public ChangeViewMessage(Func<T, Tvm> builder, Action<Tvm> actionBeforeActivation = null, string channel = "all")
        {
            _viewModelFactoryType = typeof(T);
            Channel = channel;
            _builder = builder;
            _action = actionBeforeActivation;
        }
        
        
        public Tvm GetViewModel(IViewModelFactoryResolver resolver)
        {
            T f = resolver.Resolve<T>(_viewModelFactoryType);
            return _viewModel = _builder.Invoke(f);
        }


        public void Apply()
        {
            _action?.Invoke(_viewModel);
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
