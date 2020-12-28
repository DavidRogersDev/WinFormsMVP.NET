using System.Windows.Forms;
using WinFormsMVP.NET.Binder;

namespace WinFormsMVP.NET.Forms
{
    public class MvpUserControl : UserControl, IView
    {
        private readonly PresenterBinder _presenterBinder = new PresenterBinder();

        public MvpUserControl()
        {
            ThrowExceptionIfNoPresenterBound = true;

            _presenterBinder.PerformBinding(this);
        }

        public bool ThrowExceptionIfNoPresenterBound { get; private set; }
    }
}
