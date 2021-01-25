using System;
using Autofac.Core;

namespace WinFormsMVP.NET.Autofac
{
    /// <summary>
    /// A Presenter Identification Strategy which uses the postfix of Presenter to build the type service name
    /// </summary>
    /// <remarks>The NamedService created uses "presenter." + TypeName as the service name</remarks>
    public class DefaultPresenterIdentificationStrategy : IPresenterIdentificationStrategy
    {
        private const string Prefix = "presenter.";
        private const string TypeNameSuffix = "Presenter";

        #region IPresenterIdentificationStrategy Members

        /// <summary>
        /// Gets a service based on the name of the presenter
        /// </summary>
        /// <param name="presenterName">Name of the presenter.</param>
        /// <returns></returns>
        public Service ServiceForPresenterName(string presenterName)
        {
            if (string.IsNullOrEmpty(presenterName))
                throw new ArgumentException("PresenterName must be suppled.", "presenterName");
            
            return new KeyedService(Prefix + presenterName.ToLowerInvariant(), typeof(IPresenter));
        }

        /// <summary>
        /// Get a service based on the type of the presenter
        /// </summary>
        /// <param name="presenterType">Type of the presenter.</param>
        /// <returns></returns>
        public Service ServiceForPresenterType(Type presenterType)
        {
            if (presenterType == null)
                throw new ArgumentNullException("presenterType", "Type of presenter must be suppled.");

            return ServiceForPresenterName(presenterType.Name.Replace(TypeNameSuffix, ""));
        }

        #endregion
    }
}