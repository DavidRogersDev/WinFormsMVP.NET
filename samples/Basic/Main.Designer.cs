﻿
namespace Basic
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            this.OrdersGrid = new System.Windows.Forms.DataGridView();
            this.OrdersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.OrdersFilterComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // OrdersGrid
            // 
            this.OrdersGrid.AllowUserToAddRows = false;
            this.OrdersGrid.AllowUserToDeleteRows = false;
            this.OrdersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrdersGrid.Location = new System.Drawing.Point(62, 78);
            this.OrdersGrid.Name = "OrdersGrid";
            this.OrdersGrid.ReadOnly = true;
            this.OrdersGrid.RowTemplate.Height = 25;
            this.OrdersGrid.Size = new System.Drawing.Size(875, 370);
            this.OrdersGrid.TabIndex = 0;
            // 
            // OrdersFilterComboBox
            // 
            this.OrdersFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OrdersFilterComboBox.FormattingEnabled = true;
            this.OrdersFilterComboBox.Location = new System.Drawing.Point(62, 20);
            this.OrdersFilterComboBox.Name = "OrdersFilterComboBox";
            this.OrdersFilterComboBox.Size = new System.Drawing.Size(170, 23);
            this.OrdersFilterComboBox.TabIndex = 1;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 461);
            this.Controls.Add(this.OrdersFilterComboBox);
            this.Controls.Add(this.OrdersGrid);
            this.Name = "Main";
            this.Text = "Main";
            ((System.ComponentModel.ISupportInitialize)(this.OrdersGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView OrdersGrid;
        private System.Windows.Forms.BindingSource OrdersBindingSource;
        private System.Windows.Forms.ComboBox OrdersFilterComboBox;
    }
}