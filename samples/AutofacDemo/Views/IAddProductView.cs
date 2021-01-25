using System;
using AutofacDemo.Services.DataTransferObjects;
using WinFormsMVP.NET;

namespace AutofacDemo.Views
{
    public interface IAddProductView : IView
    {
        event EventHandler CleanUp;
        event EventHandler<AddProductDto> AddProductEvent;
        void NotifyOpResult(bool succeeded);

    }
}
