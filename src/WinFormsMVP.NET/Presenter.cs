namespace WinFormsMVP.NET
{
    public abstract class Presenter<TView> : IPresenter<TView>
        where TView : class, IView 
    {
        protected Presenter(TView view)
        {
            PresenterState = new KeyValueState();;
            View = view;
        }

        public TView View { get; }
        public IKeyValueState PresenterState { get; }
    }
}
