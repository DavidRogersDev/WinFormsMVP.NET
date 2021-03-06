﻿using System;
using System.Collections.Generic;
using System.Linq;
using Basic.Services.DataTransferObjects;
using Basic.Views;
using WinFormsMVP.NET.Forms;

namespace Basic
{
    public partial class Main : MvpForm, IMainView
    {
        public Main()
        {
            InitializeComponent();

            OrdersGrid.DataSource = OrdersBindingSource;
        }

        public event EventHandler<int> OrderFiltered;

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

        public void FilterList(IEnumerable<OrderDto> orders)
        {
            OrdersBindingSource.DataSource = orders;
        }

        private void OrdersFilterComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        { 
            OrderFiltered(sender, (int) OrdersFilterComboBox.SelectedItem);
        }
    }
}
