using System.Windows.Forms;
using WinFormsMVP.NET.Binder;

namespace WinFormsMVP.NET.Forms
{
    public class MvpUserControl : UserControl, IView
    {
        protected PresenterBinder PresenterBinder = new PresenterBinder();

        public MvpUserControl()
        {
            ThrowExceptionIfNoPresenterBound = true;

            PresenterBinder.PerformBinding(this);
        }

        public bool ThrowExceptionIfNoPresenterBound { get; private set; }
    }
}
