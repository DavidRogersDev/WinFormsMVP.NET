
namespace AutofacDemo.Forms
{
    partial class AddProductForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ProductNameTextBox = new System.Windows.Forms.TextBox();
            this.StockLevelNumeric = new System.Windows.Forms.NumericUpDown();
            this.ReorderLevelNumeric = new System.Windows.Forms.NumericUpDown();
            this.OnOrderNumeric = new System.Windows.Forms.NumericUpDown();
            this.DiscontinuedCheckBox = new System.Windows.Forms.CheckBox();
            this.AddProductButton = new System.Windows.Forms.Button();
            this.UnitPriceNumeric = new System.Windows.Forms.NumericUpDown();
            this.LabelCurrencySymbol = new System.Windows.Forms.Label();
            this.LabelReOrderLevel = new System.Windows.Forms.Label();
            this.LabelOnHandLevel = new System.Windows.Forms.Label();
            this.LabelUnitPrice = new System.Windows.Forms.Label();
            this.LabelStockLevel = new System.Windows.Forms.Label();
            this.LabelProductName = new System.Windows.Forms.Label();
            this.LabelOpResult = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.StockLevelNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReorderLevelNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnOrderNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnitPriceNumeric)).BeginInit();
            this.SuspendLayout();
            // 
            // ProductNameTextBox
            // 
            this.ProductNameTextBox.Location = new System.Drawing.Point(85, 39);
            this.ProductNameTextBox.Name = "ProductNameTextBox";
            this.ProductNameTextBox.Size = new System.Drawing.Size(202, 23);
            this.ProductNameTextBox.TabIndex = 0;
            // 
            // StockLevelNumeric
            // 
            this.StockLevelNumeric.Location = new System.Drawing.Point(85, 145);
            this.StockLevelNumeric.Name = "StockLevelNumeric";
            this.StockLevelNumeric.Size = new System.Drawing.Size(59, 23);
            this.StockLevelNumeric.TabIndex = 1;
            // 
            // ReorderLevelNumeric
            // 
            this.ReorderLevelNumeric.Location = new System.Drawing.Point(228, 92);
            this.ReorderLevelNumeric.Name = "ReorderLevelNumeric";
            this.ReorderLevelNumeric.Size = new System.Drawing.Size(59, 23);
            this.ReorderLevelNumeric.TabIndex = 2;
            // 
            // OnOrderNumeric
            // 
            this.OnOrderNumeric.Location = new System.Drawing.Point(228, 145);
            this.OnOrderNumeric.Name = "OnOrderNumeric";
            this.OnOrderNumeric.Size = new System.Drawing.Size(59, 23);
            this.OnOrderNumeric.TabIndex = 3;
            // 
            // DiscontinuedCheckBox
            // 
            this.DiscontinuedCheckBox.AutoSize = true;
            this.DiscontinuedCheckBox.Location = new System.Drawing.Point(85, 210);
            this.DiscontinuedCheckBox.Name = "DiscontinuedCheckBox";
            this.DiscontinuedCheckBox.Size = new System.Drawing.Size(96, 19);
            this.DiscontinuedCheckBox.TabIndex = 4;
            this.DiscontinuedCheckBox.Text = "Discontinued";
            this.DiscontinuedCheckBox.UseVisualStyleBackColor = true;
            // 
            // AddProductButton
            // 
            this.AddProductButton.Location = new System.Drawing.Point(85, 254);
            this.AddProductButton.Name = "AddProductButton";
            this.AddProductButton.Size = new System.Drawing.Size(229, 23);
            this.AddProductButton.TabIndex = 5;
            this.AddProductButton.Text = "Add Product";
            this.AddProductButton.UseVisualStyleBackColor = true;
            this.AddProductButton.Click += new System.EventHandler(this.AddProductButton_Click);
            // 
            // UnitPriceNumeric
            // 
            this.UnitPriceNumeric.DecimalPlaces = 2;
            this.UnitPriceNumeric.Location = new System.Drawing.Point(85, 92);
            this.UnitPriceNumeric.Name = "UnitPriceNumeric";
            this.UnitPriceNumeric.Size = new System.Drawing.Size(59, 23);
            this.UnitPriceNumeric.TabIndex = 6;
            this.UnitPriceNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UnitPriceNumeric.ThousandsSeparator = true;
            // 
            // LabelCurrencySymbol
            // 
            this.LabelCurrencySymbol.AutoSize = true;
            this.LabelCurrencySymbol.Location = new System.Drawing.Point(69, 96);
            this.LabelCurrencySymbol.Name = "LabelCurrencySymbol";
            this.LabelCurrencySymbol.Size = new System.Drawing.Size(13, 15);
            this.LabelCurrencySymbol.TabIndex = 7;
            this.LabelCurrencySymbol.Text = "$";
            this.LabelCurrencySymbol.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // LabelReOrderLevel
            // 
            this.LabelReOrderLevel.AutoSize = true;
            this.LabelReOrderLevel.Location = new System.Drawing.Point(303, 96);
            this.LabelReOrderLevel.Name = "LabelReOrderLevel";
            this.LabelReOrderLevel.Size = new System.Drawing.Size(83, 15);
            this.LabelReOrderLevel.TabIndex = 8;
            this.LabelReOrderLevel.Text = "Re-order Level";
            // 
            // LabelOnHandLevel
            // 
            this.LabelOnHandLevel.AutoSize = true;
            this.LabelOnHandLevel.Location = new System.Drawing.Point(303, 149);
            this.LabelOnHandLevel.Name = "LabelOnHandLevel";
            this.LabelOnHandLevel.Size = new System.Drawing.Size(86, 15);
            this.LabelOnHandLevel.TabIndex = 9;
            this.LabelOnHandLevel.Text = "On Order Level";
            // 
            // LabelUnitPrice
            // 
            this.LabelUnitPrice.AutoSize = true;
            this.LabelUnitPrice.Location = new System.Drawing.Point(150, 96);
            this.LabelUnitPrice.Name = "LabelUnitPrice";
            this.LabelUnitPrice.Size = new System.Drawing.Size(58, 15);
            this.LabelUnitPrice.TabIndex = 10;
            this.LabelUnitPrice.Text = "Unit Price";
            // 
            // LabelStockLevel
            // 
            this.LabelStockLevel.AutoSize = true;
            this.LabelStockLevel.Location = new System.Drawing.Point(150, 149);
            this.LabelStockLevel.Name = "LabelStockLevel";
            this.LabelStockLevel.Size = new System.Drawing.Size(66, 15);
            this.LabelStockLevel.TabIndex = 11;
            this.LabelStockLevel.Text = "Stock Level";
            // 
            // LabelProductName
            // 
            this.LabelProductName.AutoSize = true;
            this.LabelProductName.Location = new System.Drawing.Point(303, 42);
            this.LabelProductName.Name = "LabelProductName";
            this.LabelProductName.Size = new System.Drawing.Size(87, 15);
            this.LabelProductName.TabIndex = 12;
            this.LabelProductName.Text = "Product Name ";
            // 
            // LabelOpResult
            // 
            this.LabelOpResult.AutoSize = true;
            this.LabelOpResult.Location = new System.Drawing.Point(333, 258);
            this.LabelOpResult.Name = "LabelOpResult";
            this.LabelOpResult.Size = new System.Drawing.Size(83, 15);
            this.LabelOpResult.TabIndex = 13;
            this.LabelOpResult.Text = "LabelOpResult";
            this.LabelOpResult.Visible = false;
            // 
            // AddProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(527, 298);
            this.Controls.Add(this.LabelOpResult);
            this.Controls.Add(this.LabelProductName);
            this.Controls.Add(this.LabelStockLevel);
            this.Controls.Add(this.LabelUnitPrice);
            this.Controls.Add(this.LabelOnHandLevel);
            this.Controls.Add(this.LabelReOrderLevel);
            this.Controls.Add(this.LabelCurrencySymbol);
            this.Controls.Add(this.UnitPriceNumeric);
            this.Controls.Add(this.AddProductButton);
            this.Controls.Add(this.DiscontinuedCheckBox);
            this.Controls.Add(this.OnOrderNumeric);
            this.Controls.Add(this.ReorderLevelNumeric);
            this.Controls.Add(this.StockLevelNumeric);
            this.Controls.Add(this.ProductNameTextBox);
            this.Name = "AddProductForm";
            this.Text = "Form to Add a Product";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddProductForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.StockLevelNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReorderLevelNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OnOrderNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UnitPriceNumeric)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox ProductNameTextBox;
        private System.Windows.Forms.NumericUpDown StockLevelNumeric;
        private System.Windows.Forms.NumericUpDown ReorderLevelNumeric;
        private System.Windows.Forms.NumericUpDown OnOrderNumeric;
        private System.Windows.Forms.CheckBox DiscontinuedCheckBox;
        private System.Windows.Forms.Button AddProductButton;
        private System.Windows.Forms.NumericUpDown UnitPriceNumeric;
        private System.Windows.Forms.Label LabelCurrencySymbol;
        private System.Windows.Forms.Label LabelReOrderLevel;
        private System.Windows.Forms.Label LabelOnHandLevel;
        private System.Windows.Forms.Label LabelUnitPrice;
        private System.Windows.Forms.Label LabelStockLevel;
        private System.Windows.Forms.Label LabelProductName;
        private System.Windows.Forms.Label LabelOpResult;

    }
}