using System;
using Lamar;
using WinFormsMVP.NET.Binder;

namespace WinFormsMVP.NET.Lamar
{
    public class LamarPresenterFactory : IPresenterFactory
    {
        private readonly IContainer _container;

        public LamarPresenterFactory(IContainer container)
        {
            _container = container;
        }

        public IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
        {
            var c = _container.GetNestedContainer();
            c.Inject(viewInstance);

            return _container.GetInstance(presenterType) as IPresenter;
        }

        public void Release(IPresenter presenter)
        {
            //not required. Let Lamar release
        }
    }
}
