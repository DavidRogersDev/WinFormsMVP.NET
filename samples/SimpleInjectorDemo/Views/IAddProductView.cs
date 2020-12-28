using System;
using SimpleInjectorDemo.Services.DataTransferObjects;
using WinFormsMVP.NET;

namespace SimpleInjectorDemo.Views
{
    public interface IAddProductView : IView
    {
        event EventHandler CleanUp;
        event EventHandler<AddProductDto> AddProductEvent;
        void NotifyOpResult(bool succeeded);
    }
}