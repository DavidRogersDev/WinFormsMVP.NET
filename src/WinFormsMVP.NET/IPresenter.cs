namespace WinFormsMVP.NET
{
    public interface IPresenter
    {
        IKeyValueState PresenterState { get; }
    }
}
