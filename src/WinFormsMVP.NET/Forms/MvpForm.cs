using System.Windows.Forms;
using WinFormsMVP.NET.Binder;

namespace WinFormsMVP.NET.Forms
{
    public class MvpForm : Form, IView
    {
        private readonly PresenterBinder _presenterBinder = new PresenterBinder();

        public MvpForm()
        {
            ThrowExceptionIfNoPresenterBound = true;
            
            _presenterBinder.PerformBinding(this);
        }

        public bool ThrowExceptionIfNoPresenterBound { get; }
    }
}
