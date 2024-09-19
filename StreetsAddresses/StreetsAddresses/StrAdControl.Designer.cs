namespace StreetsAddresses
{
    partial class StrAdControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.save_street_b = new System.Windows.Forms.Button();
            this.itemsGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete_street_b = new System.Windows.Forms.Button();
            this.edit_street_b = new System.Windows.Forms.Button();
            this.add_street_b = new System.Windows.Forms.Button();
            this.itemsGrid2 = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.delete_address_b = new System.Windows.Forms.Button();
            this.edit_address_b = new System.Windows.Forms.Button();
            this.add_address_b = new System.Windows.Forms.Button();
            this.save_address_b = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.itemsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsGrid2)).BeginInit();
            this.SuspendLayout();
            // 
            // save_street_b
            // 
            this.save_street_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.save_street_b.Location = new System.Drawing.Point(319, 470);
            this.save_street_b.Name = "save_street_b";
            this.save_street_b.Size = new System.Drawing.Size(183, 94);
            this.save_street_b.TabIndex = 9;
            this.save_street_b.Text = "Сохранить";
            this.save_street_b.UseVisualStyleBackColor = true;
            this.save_street_b.Click += new System.EventHandler(this.save_street_b_Click);
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
            this.Column2});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.itemsGrid.DefaultCellStyle = dataGridViewCellStyle11;
            this.itemsGrid.Location = new System.Drawing.Point(3, 3);
            this.itemsGrid.MultiSelect = false;
            this.itemsGrid.Name = "itemsGrid";
            this.itemsGrid.ReadOnly = true;
            this.itemsGrid.RowHeadersVisible = false;
            this.itemsGrid.RowHeadersWidth = 51;
            this.itemsGrid.RowTemplate.Height = 24;
            this.itemsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemsGrid.Size = new System.Drawing.Size(310, 561);
            this.itemsGrid.TabIndex = 5;
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
            this.Column2.HeaderText = "Улица";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // delete_street_b
            // 
            this.delete_street_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.delete_street_b.Location = new System.Drawing.Point(319, 258);
            this.delete_street_b.Name = "delete_street_b";
            this.delete_street_b.Size = new System.Drawing.Size(183, 109);
            this.delete_street_b.TabIndex = 12;
            this.delete_street_b.Text = "Удалить";
            this.delete_street_b.UseVisualStyleBackColor = true;
            this.delete_street_b.Click += new System.EventHandler(this.delete_street_b_Click);
            // 
            // edit_street_b
            // 
            this.edit_street_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.edit_street_b.Location = new System.Drawing.Point(319, 143);
            this.edit_street_b.Name = "edit_street_b";
            this.edit_street_b.Size = new System.Drawing.Size(183, 109);
            this.edit_street_b.TabIndex = 11;
            this.edit_street_b.Text = "Изменить";
            this.edit_street_b.UseVisualStyleBackColor = true;
            this.edit_street_b.Click += new System.EventHandler(this.edit_street_b_Click);
            // 
            // add_street_b
            // 
            this.add_street_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_street_b.Location = new System.Drawing.Point(319, 28);
            this.add_street_b.Name = "add_street_b";
            this.add_street_b.Size = new System.Drawing.Size(183, 109);
            this.add_street_b.TabIndex = 10;
            this.add_street_b.Text = "Добавить";
            this.add_street_b.UseVisualStyleBackColor = true;
            this.add_street_b.Click += new System.EventHandler(this.add_street_b_Click);
            // 
            // itemsGrid2
            // 
            this.itemsGrid2.AllowUserToAddRows = false;
            this.itemsGrid2.AllowUserToDeleteRows = false;
            this.itemsGrid2.AllowUserToResizeColumns = false;
            this.itemsGrid2.AllowUserToResizeRows = false;
            this.itemsGrid2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemsGrid2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.Column3});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.itemsGrid2.DefaultCellStyle = dataGridViewCellStyle12;
            this.itemsGrid2.Location = new System.Drawing.Point(508, 3);
            this.itemsGrid2.MultiSelect = false;
            this.itemsGrid2.Name = "itemsGrid2";
            this.itemsGrid2.ReadOnly = true;
            this.itemsGrid2.RowHeadersVisible = false;
            this.itemsGrid2.RowHeadersWidth = 51;
            this.itemsGrid2.RowTemplate.Height = 24;
            this.itemsGrid2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemsGrid2.Size = new System.Drawing.Size(488, 561);
            this.itemsGrid2.TabIndex = 13;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.Width = 50;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.HeaderText = "Улица";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "Номер";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // delete_address_b
            // 
            this.delete_address_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.delete_address_b.Location = new System.Drawing.Point(1002, 258);
            this.delete_address_b.Name = "delete_address_b";
            this.delete_address_b.Size = new System.Drawing.Size(183, 109);
            this.delete_address_b.TabIndex = 17;
            this.delete_address_b.Text = "Удалить";
            this.delete_address_b.UseVisualStyleBackColor = true;
            this.delete_address_b.Click += new System.EventHandler(this.delete_address_b_Click);
            // 
            // edit_address_b
            // 
            this.edit_address_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.edit_address_b.Location = new System.Drawing.Point(1002, 143);
            this.edit_address_b.Name = "edit_address_b";
            this.edit_address_b.Size = new System.Drawing.Size(183, 109);
            this.edit_address_b.TabIndex = 16;
            this.edit_address_b.Text = "Изменить";
            this.edit_address_b.UseVisualStyleBackColor = true;
            this.edit_address_b.Click += new System.EventHandler(this.edit_address_b_Click);
            // 
            // add_address_b
            // 
            this.add_address_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_address_b.Location = new System.Drawing.Point(1002, 28);
            this.add_address_b.Name = "add_address_b";
            this.add_address_b.Size = new System.Drawing.Size(183, 109);
            this.add_address_b.TabIndex = 15;
            this.add_address_b.Text = "Добавить";
            this.add_address_b.UseVisualStyleBackColor = true;
            this.add_address_b.Click += new System.EventHandler(this.add_address_b_Click);
            // 
            // save_address_b
            // 
            this.save_address_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.save_address_b.Location = new System.Drawing.Point(1002, 470);
            this.save_address_b.Name = "save_address_b";
            this.save_address_b.Size = new System.Drawing.Size(183, 94);
            this.save_address_b.TabIndex = 14;
            this.save_address_b.Text = "Сохранить";
            this.save_address_b.UseVisualStyleBackColor = true;
            this.save_address_b.Click += new System.EventHandler(this.save_address_b_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(371, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "Улицы";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(1058, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Адреса";
            // 
            // StrAdControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delete_address_b);
            this.Controls.Add(this.edit_address_b);
            this.Controls.Add(this.add_address_b);
            this.Controls.Add(this.save_address_b);
            this.Controls.Add(this.itemsGrid2);
            this.Controls.Add(this.delete_street_b);
            this.Controls.Add(this.edit_street_b);
            this.Controls.Add(this.add_street_b);
            this.Controls.Add(this.save_street_b);
            this.Controls.Add(this.itemsGrid);
            this.Name = "StrAdControl";
            this.Size = new System.Drawing.Size(1188, 567);
            ((System.ComponentModel.ISupportInitialize)(this.itemsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemsGrid2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button save_street_b;
        private System.Windows.Forms.DataGridView itemsGrid;
        private System.Windows.Forms.Button delete_street_b;
        private System.Windows.Forms.Button edit_street_b;
        private System.Windows.Forms.Button add_street_b;
        private System.Windows.Forms.DataGridView itemsGrid2;
        private System.Windows.Forms.Button delete_address_b;
        private System.Windows.Forms.Button edit_address_b;
        private System.Windows.Forms.Button add_address_b;
        private System.Windows.Forms.Button save_address_b;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
