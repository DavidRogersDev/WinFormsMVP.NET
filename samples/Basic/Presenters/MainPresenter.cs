using Basic.Views;
using WinFormsMVP.NET;

namespace Basic.Presenters
{
    public class MainPresenter : Presenter<IMainView>
    {
        public MainPresenter(IMainView view) 
            : base(view)
        {
        }
    }
}
