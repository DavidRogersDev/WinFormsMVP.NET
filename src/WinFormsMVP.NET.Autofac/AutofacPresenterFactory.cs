using System;
using Autofac;
using Autofac.Core;
using WinFormsMVP.NET.Binder;

namespace WinFormsMVP.NET.Autofac
{
    /// <summary>
    /// A custom WinForms MVP presenter factory which uses Autofac to do type resolution
    /// </summary>
    /// <remarks>
    /// <example>
    /// <code>
    /// var builder = new ContainerBuilder();
    /// builder.RegisterPresenters(Assembly.GetExecutingAssembly());
    /// WebFormsMvp.Binder.PresenterBinder.Factory = new AutofacPresenterFactory(new ContainerProvider(builder.Build()));
    /// </code>
    /// </example>
    /// </remarks>
    public class AutofacPresenterFactory : IPresenterFactory
    {
        private readonly IContainer _container;
        private readonly IPresenterIdentificationStrategy _discoveryStrategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacPresenterFactory"/> class using the <see cref="DefaultPresenterIdentificationStrategy"/>
        /// </summary>
        /// <param name="containerProvider">The container provider.</param>
        public AutofacPresenterFactory(IContainer container)
            : this(container, new DefaultPresenterIdentificationStrategy())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacPresenterFactory"/> class.
        /// </summary>
        /// <param name="containerProvider">The container provider.</param>
        /// <param name="discoveryStrategy">The discovery strategy.</param>
        public AutofacPresenterFactory(IContainer container, IPresenterIdentificationStrategy discoveryStrategy)
        {
            _container = container ?? throw new ArgumentNullException(nameof(container));
            _discoveryStrategy = discoveryStrategy;
        }

        public IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
        {
            var service = _discoveryStrategy.ServiceForPresenterType(presenterType);

            if (_container.TryResolveService(
                service,
                new[] { new LooselyTypedParameter(viewType, viewInstance) },
                out var presenter)
            )
                return (IPresenter)presenter;

            throw new Exception(
                "Unable to resolve type " + presenterType.FullName + " using service " + service.Description
                );
        }

        internal class LooselyTypedParameter : ConstantParameter
        {
            public LooselyTypedParameter(Type type, object value)
                : base(value, pi => pi.ParameterType.IsAssignableFrom(type))
            {
                if (type == null)
                {
                    throw new ArgumentNullException(nameof(type), @"Type must be provided");
                }

                Type = type;
            }

            public Type Type { get; }
        }

        /// <summary>
        /// Releases the specified presenter from any of its lifestyle demands.
        /// </summary>
        /// <param name="presenter">The presenter to release.</param>
        public virtual void Release(IPresenter presenter)
        {
            if (presenter is IDisposable)
            {
                ((IDisposable)presenter).Dispose();
            }
        }
    }
}
