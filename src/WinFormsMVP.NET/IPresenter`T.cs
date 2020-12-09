namespace WinFormsMVP.NET
{
    public interface IPresenter<out TView> : IPresenter
        where TView : class, IView
    {
        TView View { get; }
    }
}
