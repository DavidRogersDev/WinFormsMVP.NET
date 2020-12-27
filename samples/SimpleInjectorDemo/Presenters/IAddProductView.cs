using System;
using SimpleInjectorDemo.Services.DataTransferObjects;
using WinFormsMVP.NET;

namespace SimpleInjectorDemo.Presenters
{
    public interface IAddProductView : IView
    {
        event EventHandler<AddProductDto> AddProductEvent;
    }
}