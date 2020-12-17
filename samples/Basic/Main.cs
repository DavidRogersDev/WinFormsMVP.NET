using System;
using System.Collections.Generic;
using System.Linq;
using Basic.Services.DataTransferObjects;
using Basic.Views;
using WinFormsMVP.NET.Forms;

namespace Basic
{
    public partial class Main : MvpForm, IMainView
    {
        private IEnumerable<OrderDto> _orders;

        public Main()
        {
            InitializeComponent();

            dataGridView1.DataSource = bindingSource1;
        }

        public event EventHandler<int> OrderFiltered;

        public void PopulateList(IEnumerable<OrderDto> orders)
        {
            bindingSource1.DataSource = _orders = orders;
        }

        public void PopulateOrdersFilter(IEnumerable<int> orderIds)
        {
            OrdersFilterComboBox.DataSource = new List<int>(orderIds);
            
            OrdersFilterComboBox.SelectedIndex = -1;

            OrdersFilterComboBox.SelectedIndexChanged += OrdersFilterComboBox_SelectedIndexChanged;
        }

        public void FilterList(int orderId)
        {
            bindingSource1.DataSource = _orders.Where(o => o.OrderId == orderId);
        }

        private void OrdersFilterComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        { 
            OrderFiltered(sender, (int) OrdersFilterComboBox.SelectedItem);
        }
    }
}
