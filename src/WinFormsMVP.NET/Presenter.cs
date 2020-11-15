namespace WinFormsMVP.NET
{
    public abstract class Presenter<TView> : IPresenter<TView>
        where TView : class, IView 
    {
        protected Presenter(TView view)
        {
            View = view;
        }

        public TView View { get; }
    }
}
