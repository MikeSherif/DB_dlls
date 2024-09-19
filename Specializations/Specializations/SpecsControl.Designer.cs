﻿namespace Specializations
{
    partial class SpecsControl
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.save_b = new System.Windows.Forms.Button();
            this.delete_b = new System.Windows.Forms.Button();
            this.edit_b = new System.Windows.Forms.Button();
            this.add_b = new System.Windows.Forms.Button();
            this.itemsGrid = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.itemsGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // save_b
            // 
            this.save_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.save_b.Location = new System.Drawing.Point(1002, 470);
            this.save_b.Name = "save_b";
            this.save_b.Size = new System.Drawing.Size(183, 94);
            this.save_b.TabIndex = 9;
            this.save_b.Text = "Сохранить";
            this.save_b.UseVisualStyleBackColor = true;
            this.save_b.Click += new System.EventHandler(this.save_b_Click);
            // 
            // delete_b
            // 
            this.delete_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.delete_b.Location = new System.Drawing.Point(1002, 257);
            this.delete_b.Name = "delete_b";
            this.delete_b.Size = new System.Drawing.Size(183, 109);
            this.delete_b.TabIndex = 8;
            this.delete_b.Text = "Удалить";
            this.delete_b.UseVisualStyleBackColor = true;
            this.delete_b.Click += new System.EventHandler(this.delete_b_Click);
            // 
            // edit_b
            // 
            this.edit_b.Font = new System.Drawing.Font("Lucida Console", 12F);
            this.edit_b.Location = new System.Drawing.Point(1002, 142);
            this.edit_b.Name = "edit_b";
            this.edit_b.Size = new System.Drawing.Size(183, 109);
            this.edit_b.TabIndex = 7;
            this.edit_b.Text = "Изменить";
            this.edit_b.UseVisualStyleBackColor = true;
            this.edit_b.Click += new System.EventHandler(this.edit_b_Click);
            // 
            // add_b
            // 
            this.add_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_b.Location = new System.Drawing.Point(1002, 27);
            this.add_b.Name = "add_b";
            this.add_b.Size = new System.Drawing.Size(183, 109);
            this.add_b.TabIndex = 6;
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
            this.Column2});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.itemsGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.itemsGrid.Location = new System.Drawing.Point(3, 3);
            this.itemsGrid.MultiSelect = false;
            this.itemsGrid.Name = "itemsGrid";
            this.itemsGrid.ReadOnly = true;
            this.itemsGrid.RowHeadersVisible = false;
            this.itemsGrid.RowHeadersWidth = 51;
            this.itemsGrid.RowTemplate.Height = 24;
            this.itemsGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemsGrid.Size = new System.Drawing.Size(993, 561);
            this.itemsGrid.TabIndex = 5;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Column1.Width = 125;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Специализация";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // SpecsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.Controls.Add(this.save_b);
            this.Controls.Add(this.delete_b);
            this.Controls.Add(this.edit_b);
            this.Controls.Add(this.add_b);
            this.Controls.Add(this.itemsGrid);
            this.Name = "SpecsControl";
            this.Size = new System.Drawing.Size(1188, 567);
            ((System.ComponentModel.ISupportInitialize)(this.itemsGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button save_b;
        private System.Windows.Forms.Button delete_b;
        private System.Windows.Forms.Button edit_b;
        private System.Windows.Forms.Button add_b;
        private System.Windows.Forms.DataGridView itemsGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}