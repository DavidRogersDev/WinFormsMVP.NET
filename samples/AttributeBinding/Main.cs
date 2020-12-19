using AttributeBinding.Presenters;
using AttributeBinding.Services.DataTransferObjects;
using AttributeBinding.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using AttributeBinding.Models;
using WinFormsMVP.NET;
using WinFormsMVP.NET.Forms;

namespace AttributeBinding
{
    [PresenterBinding(typeof(OrdersPresenter))]
    public partial class Main : MvpForm, IMainView
    {
        public Main()
        {
            InitializeComponent();

            OrdersGrid.DataSource = OrdersBindingSource;
        }

        public event EventHandler ClearFilter;
        public event EventHandler<int> OrderFiltered;

        public MainModel Model { get; set; }

        public void PopulateList(IEnumerable<OrderDto> orders)
        {
            OrdersBindingSource.DataSource = orders;
        }

        public void PopulateOrdersFilter(IEnumerable<int> orderIds)
        {
            OrdersFilterComboBox.DataSource = new List<int>(orderIds);

            OrdersFilterComboBox.SelectedIndex = -1;

            OrdersFilterComboBox.SelectedIndexChanged += OrdersFilterComboBox_SelectedIndexChanged;
        }

        public void FilterList(int orderId)
        {
            OrdersBindingSource.DataSource = Model.Orders.Where(o => o.OrderId == orderId);
        }

        private void OrdersFilterComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            OrderFiltered(sender, (int)OrdersFilterComboBox.SelectedItem);
        }

        private void ClearFilterButton_Click(object sender, EventArgs e)
        {
            ClearFilter(sender, EventArgs.Empty);
        }
    }
}
