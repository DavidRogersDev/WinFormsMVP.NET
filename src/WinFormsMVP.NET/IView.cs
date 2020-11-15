using System;

namespace WinFormsMVP.NET
{
    public interface IView
    {
        bool ThrowExceptionIfNoPresenterBound { get; }

        event EventHandler Load;
    }
}
