namespace ConstructionObject
{
    partial class InfoForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.complete_b = new System.Windows.Forms.Button();
            this.delete_b = new System.Windows.Forms.Button();
            this.edit_b = new System.Windows.Forms.Button();
            this.add_b = new System.Windows.Forms.Button();
            this.itemsGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancel_b = new System.Windows.Forms.Button();
            this.info_b = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.itemsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // complete_b
            // 
            this.complete_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.complete_b.Location = new System.Drawing.Point(1005, 217);
            this.complete_b.Name = "complete_b";
            this.complete_b.Size = new System.Drawing.Size(183, 68);
            this.complete_b.TabIndex = 29;
            this.complete_b.Text = "Выполнить";
            this.complete_b.UseVisualStyleBackColor = true;
            this.complete_b.Click += new System.EventHandler(this.complete_b_Click);
            // 
            // delete_b
            // 
            this.delete_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.delete_b.Location = new System.Drawing.Point(1005, 461);
            this.delete_b.Name = "delete_b";
            this.delete_b.Size = new System.Drawing.Size(183, 109);
            this.delete_b.TabIndex = 28;
            this.delete_b.Text = "Удалить";
            this.delete_b.UseVisualStyleBackColor = true;
            this.delete_b.Click += new System.EventHandler(this.delete_b_Click);
            // 
            // edit_b
            // 
            this.edit_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.edit_b.Location = new System.Drawing.Point(1005, 126);
            this.edit_b.Name = "edit_b";
            this.edit_b.Size = new System.Drawing.Size(183, 85);
            this.edit_b.TabIndex = 27;
            this.edit_b.Text = "Изменить";
            this.edit_b.UseVisualStyleBackColor = true;
            this.edit_b.Click += new System.EventHandler(this.edit_b_Click);
            // 
            // add_b
            // 
            this.add_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_b.Location = new System.Drawing.Point(1005, 33);
            this.add_b.Name = "add_b";
            this.add_b.Size = new System.Drawing.Size(183, 87);
            this.add_b.TabIndex = 26;
            this.add_b.Text = "Добавить";
            this.add_b.UseVisualStyleBackColor = true;
            this.add_b.Click += new System.EventHandler(this.add_b_Click);
            // 
            // itemsGrid
            // 
            this.itemsGrid.AllowUserToAddRows = false;
            this.itemsGrid.AllowUserToDeleteRows = false;
            this.itemsGrid.AllowUserToResizeColumns = false;
            this.itemsGrid.AllowUserToResizeRows = false;
            this.itemsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemsGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.itemsGrid.DefaultCellStyle = dataGridViewCellStyle2;
            this.itemsGrid.Location = new System.Drawing.Point(6, 9);
            this.itemsGrid.MultiSelect = false;
            this.itemsGrid.Name = "itemsGrid";
            this.itemsGrid.ReadOnly = true;
            this.itemsGrid.RowHeadersVisible = false;
            this.itemsGrid.RowHeadersWidth = 51;
            this.itemsGrid.RowTemplate.Height = 24;
            this.itemsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemsGrid.Size = new System.Drawing.Size(993, 561);
            this.itemsGrid.TabIndex = 25;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 50;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Название";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Column3.HeaderText = "Статус";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column3.Width = 82;
            // 
            // cancel_b
            // 
            this.cancel_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancel_b.Location = new System.Drawing.Point(1005, 291);
            this.cancel_b.Name = "cancel_b";
            this.cancel_b.Size = new System.Drawing.Size(183, 68);
            this.cancel_b.TabIndex = 30;
            this.cancel_b.Text = "Отменить";
            this.cancel_b.UseVisualStyleBackColor = true;
            this.cancel_b.Click += new System.EventHandler(this.cancel_b_Click);
            // 
            // info_b
            // 
            this.info_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.info_b.Location = new System.Drawing.Point(1005, 365);
            this.info_b.Name = "info_b";
            this.info_b.Size = new System.Drawing.Size(183, 68);
            this.info_b.TabIndex = 31;
            this.info_b.Text = "Подробнее";
            this.info_b.UseVisualStyleBackColor = true;
            this.info_b.Click += new System.EventHandler(this.info_b_Click);
            // 
            // InfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(1195, 578);
            this.Controls.Add(this.info_b);
            this.Controls.Add(this.cancel_b);
            this.Controls.Add(this.complete_b);
            this.Controls.Add(this.delete_b);
            this.Controls.Add(this.edit_b);
            this.Controls.Add(this.add_b);
            this.Controls.Add(this.itemsGrid);
            this.MaximumSize = new System.Drawing.Size(1213, 625);
            this.MinimumSize = new System.Drawing.Size(1213, 625);
            this.Name = "InfoForm";
            this.Text = "Перечень работ";
            ((System.ComponentModel.ISupportInitialize)(this.itemsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button complete_b;
        private System.Windows.Forms.Button delete_b;
        private System.Windows.Forms.Button edit_b;
        private System.Windows.Forms.Button add_b;
        private System.Windows.Forms.DataGridView itemsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button cancel_b;
        private System.Windows.Forms.Button info_b;
    }
}