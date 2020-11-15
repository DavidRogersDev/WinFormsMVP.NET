using System;

namespace WinFormsMVP.NET.Binder
{
    /// <summary>
    /// Provides data for the <see cref="WebFormsMvp.Binder.PresenterBinder.PresenterCreated"/> event.
    /// </summary>
    public class PresenterCreatedEventArgs : EventArgs
    {
        /// <summary />
        /// <param name="presenter">The presenter that was just created.</param>
        public PresenterCreatedEventArgs(IPresenter presenter)
        {
            Presenter = presenter;
        }

        /// <summary>
        /// Gets the presenter that was just created.
        /// </summary>
        public IPresenter Presenter { get; }
    }
}