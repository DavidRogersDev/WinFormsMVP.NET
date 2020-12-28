using System;
using System.Collections.Generic;
using SimpleInjectorDemo.Services.DataTransferObjects;
using SimpleInjectorDemo.Views;
using WinFormsMVP.NET.Binder;
using WinFormsMVP.NET.Forms;

namespace SimpleInjectorDemo.Forms
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
            OrderFiltered(sender, (int)OrdersFilterComboBox.SelectedItem);
        }

        private void AddNewProductButton_Click(object sender, EventArgs e)
        {
            var addProductForm = new AddProductForm();            
            addProductForm.ShowDialog();
        }

        public void ClearSelectedFilter()
        {
            OrdersFilterComboBox.SelectedIndexChanged -= OrdersFilterComboBox_SelectedIndexChanged;
            
            OrdersFilterComboBox.SelectedIndex = -1;

            OrdersFilterComboBox.SelectedIndexChanged += OrdersFilterComboBox_SelectedIndexChanged;
        }

        private void ClearFilterButton_Click(object sender, EventArgs e)
        {
            ClearFilter?.Invoke(sender, EventArgs.Empty);
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
