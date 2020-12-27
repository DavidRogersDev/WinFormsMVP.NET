using System.Linq;
using SimpleInjectorDemo.Services;
using SimpleInjectorDemo.Views;
using WinFormsMVP.NET;

namespace SimpleInjectorDemo.Presenters
{
    public class MainPresenter : Presenter<IMainView>
    {
        private readonly IOrdersService _ordersService;

        public MainPresenter(IMainView view, IOrdersService ordersService) 
            : base(view)
        {
            View.Load += View_Load;
            View.OrderFiltered += View_OrderFiltered;
            
            _ordersService = ordersService;
        }

        public void View_Load(object sender, System.EventArgs e)
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
    }
}
