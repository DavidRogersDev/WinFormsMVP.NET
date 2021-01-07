using System;
using LamarDemo.Services.DataTransferObjects;
using WinFormsMVP.NET;

namespace LamarDemo.Views
{
    public interface IAddProductView : IView
    {
        event EventHandler CleanUp;
        event EventHandler<AddProductDto> AddProductEvent;
        void NotifyOpResult(bool succeeded);
    }
}