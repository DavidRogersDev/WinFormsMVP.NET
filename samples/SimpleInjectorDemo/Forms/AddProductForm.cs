using System;
using SimpleInjectorDemo.Presenters;
using SimpleInjectorDemo.Services.DataTransferObjects;
using WinFormsMVP.NET.Forms;

namespace SimpleInjectorDemo.Forms
{
    public partial class AddProductForm : MvpForm, IAddProductView
    {
        public AddProductForm()
        {
            InitializeComponent();
        }

        public event EventHandler<AddProductDto> AddProductEvent;

        private void AddProductButton_Click(object sender, EventArgs e)
        {
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
    }
}
