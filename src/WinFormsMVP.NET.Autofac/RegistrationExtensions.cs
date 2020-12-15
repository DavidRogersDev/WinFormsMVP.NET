using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;

namespace WinFormsMVP.NET.Autofac
{
    public static class RegistrationExtensions
    {
        /// <summary>
        /// Registers all types which inherit IPresenter.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        /// <param name="presenterAssemblies">Assemblies to scan for presenters.</param>
        /// <returns>Registration builder allowing the presenter components to be customised.</returns>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterPresenters(
                this ContainerBuilder builder,
                params Assembly[] presenterAssemblies)
        {
            return RegisterPresenters(builder, new DefaultPresenterIdentificationStrategy(), presenterAssemblies);
        }

        /// <summary>
        /// Registers all types which inherit IPresenter.
        /// </summary>
        /// <param name="builder">The container builder.</param>
        /// <param name="identificationStrategy">The identification strategy.</param>
        /// <param name="presenterAssemblies">Assemblies to scan for presenters.</param>
        /// <returns>Registration builder allowing the presenter components to be customised.</returns>
        public static IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>
            RegisterPresenters(
                this ContainerBuilder builder,
                IPresenterIdentificationStrategy identificationStrategy,
                params Assembly[] presenterAssemblies)
        {
            return builder.RegisterAssemblyTypes(presenterAssemblies)
                .Where(t => typeof(IPresenter).IsAssignableFrom(t))
                .PropertiesAutowired()
                .As(t => identificationStrategy.ServiceForPresenterType(t));
        }
    }
}
