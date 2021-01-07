using System;
using LamarDemo.Services;
using LamarDemo.Services.DataTransferObjects;
using LamarDemo.Views;
using WinFormsMVP.NET;
using WinFormsMVP.NET.Binder;

namespace LamarDemo.Presenters
{
    public class AddProductPresenter : Presenter<IAddProductView>, IDisposable
    {
        private readonly IOrdersService _ordersService;

        public AddProductPresenter(IAddProductView view, IOrdersService ordersService) 
            : base(view)
        {
            _ordersService = ordersService;

            View.AddProductEvent += View_AddProductEvent;
            View.CleanUp += View_CleanUp;
        }

        private void View_AddProductEvent(object sender, AddProductDto dto)
        {
            var opResult = _ordersService.AddOrder(dto);

            View.NotifyOpResult(opResult);
        }

        private void View_CleanUp(object sender, System.EventArgs e)
        {
            PresenterBinder.Factory.Release(this);
        }

        public void Dispose()
        {
            if (_ordersService is IDisposable)
            {
                ((IDisposable)_ordersService).Dispose();
            }

        }
    }
}
