
namespace SimpleInjectorDemo.Forms
{
    partial class AddProductForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.ProductNameTextBox.Size = new System.Drawing.Size(229, 23);
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
            this.ReorderLevelNumeric.Location = new System.Drawing.Point(272, 96);
            this.ReorderLevelNumeric.Name = "ReorderLevelNumeric";
            this.ReorderLevelNumeric.Size = new System.Drawing.Size(59, 23);
            this.ReorderLevelNumeric.TabIndex = 2;
            // 
            // OnOrderNumeric
            // 
            this.OnOrderNumeric.Location = new System.Drawing.Point(272, 145);
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
            this.AddProductButton.Location = new System.Drawing.Point(85, 258);
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
            this.UnitPriceNumeric.Location = new System.Drawing.Point(85, 96);
            this.UnitPriceNumeric.Name = "UnitPriceNumeric";
            this.UnitPriceNumeric.Size = new System.Drawing.Size(120, 23);
            this.UnitPriceNumeric.TabIndex = 6;
            this.UnitPriceNumeric.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UnitPriceNumeric.ThousandsSeparator = true;
            // 
            // AddProductForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UnitPriceNumeric);
            this.Controls.Add(this.AddProductButton);
            this.Controls.Add(this.DiscontinuedCheckBox);
            this.Controls.Add(this.OnOrderNumeric);
            this.Controls.Add(this.ReorderLevelNumeric);
            this.Controls.Add(this.StockLevelNumeric);
            this.Controls.Add(this.ProductNameTextBox);
            this.Name = "AddProductForm";
            this.Text = "Form to Add a Product";
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
    }
}