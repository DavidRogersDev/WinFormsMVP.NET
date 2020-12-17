using System.Linq;
using Basic.Services;
using Basic.Views;
using WinFormsMVP.NET;

namespace Basic.Presenters
{
    public class MainPresenter : Presenter<IMainView>
    {
        private readonly OrdersService _ordersService;

        public MainPresenter(IMainView view) 
            : base(view)
        {
            View.Load += View_Load;
            View.OrderFiltered += View_OrderFiltered;
            
            _ordersService = new OrdersService();
        }

        public void View_OrderFiltered(object sender, int orderId)
        {
            View.FilterList(orderId);
        }

        public void View_Load(object sender, System.EventArgs e)
        {
            var orders = _ordersService.GetOrders();

            View.PopulateList(orders);
            View.PopulateOrdersFilter(orders.Select(o => o.OrderId).Distinct());
        }
    }
}
