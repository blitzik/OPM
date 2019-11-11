using Common.Factories;

namespace Common.ViewModelResolver
{
    public interface IViewModelFactoryResolver
    {
        T Resolve<T>(System.Type factory) where T : IViewModelFactory;
    }
}