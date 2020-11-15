namespace WinFormsMVP.NET
{
    public interface IView<TModel> : IView
    {
        TModel Model { get; set; }
    }
}
