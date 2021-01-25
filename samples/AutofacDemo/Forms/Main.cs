using System;
using System.Collections.Generic;
using AutofacDemo.Services.DataTransferObjects;
using AutofacDemo.Views;
using WinFormsMVP.NET.Forms;

namespace AutofacDemo.Forms
{
    public partial class Main : MvpForm, IMainView
    {
        public Main()
        {
            InitializeComponent();
            
            OrdersGrid.DataSource = OrdersBindingSource;
        }

        public event EventHandler CleanUp;
        public event EventHandler ClearFilter;
        public event EventHandler<int> OrderFiltered;
        public void ClearSelectedFilter()
        {
            OrdersFilterComboBox.SelectedIndexChanged -= OrdersFilterComboBox_SelectedIndexChanged;

            OrdersFilterComboBox.SelectedIndex = -1;

            OrdersFilterComboBox.SelectedIndexChanged += OrdersFilterComboBox_SelectedIndexChanged;
        }

        public void FilterList(IEnumerable<OrderDto> orders)
        {
            OrdersBindingSource.DataSource = orders;
        }

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

        private void ClearFilterButton_Click(object sender, EventArgs e)
        {
            ClearFilter?.Invoke(sender, EventArgs.Empty);
        }

        private void OrdersFilterComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            OrderFiltered(sender, (int)OrdersFilterComboBox.SelectedItem);
        }

        private void AddNewProductButton_Click(object sender, EventArgs e)
        {
            var addProductForm = new AddProductForm();
            addProductForm.ShowDialog();
        }


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Main_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            CleanUp?.Invoke(sender, EventArgs.Empty);
        }

    }
}
