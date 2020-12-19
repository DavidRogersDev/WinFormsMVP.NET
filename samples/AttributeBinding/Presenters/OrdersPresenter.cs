using System.Linq;
using AttributeBinding.Models;
using AttributeBinding.Services;
using AttributeBinding.Views;
using WinFormsMVP.NET;

namespace AttributeBinding.Presenters
{
    public class OrdersPresenter : Presenter<IMainView>
    {
        private readonly OrdersService _ordersService;

        public OrdersPresenter(IMainView view)
            : base(view)
        {
            View.Load += View_Load;
            View.OrderFiltered += View_OrderFiltered;
            View.ClearFilter += View_ClearFilter;

            _ordersService = new OrdersService();
        }

        private void View_ClearFilter(object sender, System.EventArgs e)
        {
            View.PopulateList(View.Model.Orders);
        }

        public void View_OrderFiltered(object sender, int orderId)
        {
            View.FilterList(orderId);
        }

        public void View_Load(object sender, System.EventArgs e)
        {
            var model = new MainModel
            {
                Orders = _ordersService.GetOrders()
            };

            View.Model = model;

            View.PopulateList(model.Orders);
            View.PopulateOrdersFilter(model.Orders.Select(o => o.OrderId).Distinct());
        }
    }
}
