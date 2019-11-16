using Common.Factories;

namespace Common.ViewModelResolver
{
    public interface IViewModelFactoryResolver
    {
        T Resolve<T>() where T : IViewModelFactory;
    }
}