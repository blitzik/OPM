using System;
using Caliburn.Micro;
using Common.Factories;
using Common.ViewModels;

namespace Common.ViewModelResolver
{
    public class ViewModelFactoryResolver : IViewModelFactoryResolver
    {
        private readonly SimpleContainer _container;
        
        public ViewModelFactoryResolver(SimpleContainer container)
        {
            _container = container;
        }
        
        
        public T Resolve<T>() where T : IViewModelFactory
        {
            return (T)_container.GetInstance(typeof(T), null);
        }
    }
}