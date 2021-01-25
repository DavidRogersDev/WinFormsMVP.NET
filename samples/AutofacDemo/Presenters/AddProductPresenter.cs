using System;
using AutofacDemo.Services;
using AutofacDemo.Views;
using WinFormsMVP.NET;
using WinFormsMVP.NET.Binder;

namespace AutofacDemo.Presenters
{
    public class AddProductPresenter : Presenter<IAddProductView>, IDisposable
    {
        private readonly IOrdersService _ordersService;
        private bool _disposed = false;

        public AddProductPresenter(IAddProductView view, IOrdersService ordersService) 
            : base(view)
        {
            _ordersService = ordersService;

            View.AddProductEvent += View_AddProductEvent;
            View.CleanUp += View_CleanUp;
        }

        private void View_AddProductEvent(object sender, Services.DataTransferObjects.AddProductDto dto)
        {
            var opResult = _ordersService.AddOrder(dto);

            View.NotifyOpResult(opResult);
        }

        private void View_CleanUp(object sender, EventArgs e)
        {
            PresenterBinder.Factory.Release(this);
        }

        public void Dispose()
        {
            if (!_disposed && _ordersService is IDisposable)
            {
                ((IDisposable)_ordersService).Dispose();
                _disposed = true;
            }

        }
    }
}
