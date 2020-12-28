using System;
using System.Drawing;
using SimpleInjectorDemo.Presenters;
using SimpleInjectorDemo.Services.DataTransferObjects;
using SimpleInjectorDemo.Views;
using WinFormsMVP.NET.Forms;

namespace SimpleInjectorDemo.Forms
{
    public partial class AddProductForm : MvpForm, IAddProductView
    {
        public AddProductForm()
        {
            InitializeComponent();
        }

        public event EventHandler CleanUp;
        public event EventHandler<AddProductDto> AddProductEvent;

        private void AddProductButton_Click(object sender, EventArgs e)
        {
            LabelOpResult.Visible = false;

            var addProduct = new AddProductDto
            {
                Discontinued = DiscontinuedCheckBox.Checked,
                Name = ProductNameTextBox.Text.Trim(),
                ReOrderLevel = (int)ReorderLevelNumeric.Value,
                UnitPrice = UnitPriceNumeric.Value,
                UnitsInStock = (int)StockLevelNumeric.Value,
                UnitsOnOrder = (int)OnOrderNumeric.Value,
            };

            AddProductEvent?.Invoke(sender, addProduct);
        }

        public void NotifyOpResult(bool succeeded)
        {
            LabelOpResult.Visible = true;

            LabelOpResult.Text = succeeded ? "Operation Succeeded" : "Operation Failed";
            LabelOpResult.ForeColor = succeeded ? Color.Green : Color.Red;
        }

        private void AddProductForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            CleanUp?.Invoke(sender, EventArgs.Empty);
        }
    }
}
