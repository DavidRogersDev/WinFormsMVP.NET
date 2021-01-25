using System;
using System.Linq;
using AutofacDemo.Services;
using AutofacDemo.Views;
using WinFormsMVP.NET;
using WinFormsMVP.NET.Binder;

namespace AutofacDemo.Presenters
{
    public class MainPresenter : Presenter<IMainView>, IDisposable
    {
        private readonly IOrdersService _ordersService;
        private bool _disposed = false;

        public MainPresenter(IMainView view, IOrdersService ordersService) 
            : base(view)
        {
            View.Load += View_Load;
            View.OrderFiltered += View_OrderFiltered;
            View.ClearFilter += View_ClearFilter;
            View.CleanUp += View_CleanUp;

            _ordersService = ordersService;
        }

        public void View_Load(object sender, EventArgs e)
        {
            var orders = _ordersService.GetOrders();

            View.PopulateList(orders);
            View.PopulateOrdersFilter(orders.Select(o => o.OrderId).Distinct());
        }

        public void View_OrderFiltered(object sender, int orderId)
        {
            var filteredOrders = _ordersService.GetOrdersById(orderId);

            View.FilterList(filteredOrders);
        }

        private void View_ClearFilter(object sender, EventArgs e)
        {
            var orders = _ordersService.GetOrders();

            View.ClearSelectedFilter();
            View.PopulateList(orders);
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
