using System.Windows.Forms;
using WinFormsMVP.NET.Binder;

namespace WinFormsMVP.NET.Forms
{
    public class MvpForm : Form, IView
    {
        protected PresenterBinder PresenterBinder = new PresenterBinder();

        public MvpForm()
        {
            ThrowExceptionIfNoPresenterBound = true;
            
            PresenterBinder.PerformBinding(this);
        }

        public bool ThrowExceptionIfNoPresenterBound { get; }
    }
}
