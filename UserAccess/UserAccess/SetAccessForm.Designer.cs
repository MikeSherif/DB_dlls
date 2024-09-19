namespace UserAccess
{
    partial class SetAccessForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.menuComboBox = new System.Windows.Forms.ComboBox();
            this.R_b = new System.Windows.Forms.CheckBox();
            this.W_b = new System.Windows.Forms.CheckBox();
            this.E_b = new System.Windows.Forms.CheckBox();
            this.D_b = new System.Windows.Forms.CheckBox();
            this.edit_b = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(135, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 53;
            this.label2.Text = "Пункт меню";
            // 
            // menuComboBox
            // 
            this.menuComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.menuComboBox.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.menuComboBox.FormattingEnabled = true;
            this.menuComboBox.Location = new System.Drawing.Point(12, 32);
            this.menuComboBox.Name = "menuComboBox";
            this.menuComboBox.Size = new System.Drawing.Size(361, 28);
            this.menuComboBox.TabIndex = 55;
            this.menuComboBox.SelectedIndexChanged += new System.EventHandler(this.menuComboBox_SelectedIndexChanged);
            // 
            // R_b
            // 
            this.R_b.AutoSize = true;
            this.R_b.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.R_b.Location = new System.Drawing.Point(12, 92);
            this.R_b.Name = "R_b";
            this.R_b.Size = new System.Drawing.Size(70, 21);
            this.R_b.TabIndex = 56;
            this.R_b.Text = "Read";
            this.R_b.UseVisualStyleBackColor = true;
            // 
            // W_b
            // 
            this.W_b.AutoSize = true;
            this.W_b.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.W_b.Location = new System.Drawing.Point(98, 92);
            this.W_b.Name = "W_b";
            this.W_b.Size = new System.Drawing.Size(80, 21);
            this.W_b.TabIndex = 57;
            this.W_b.Text = "Write";
            this.W_b.UseVisualStyleBackColor = true;
            // 
            // E_b
            // 
            this.E_b.AutoSize = true;
            this.E_b.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.E_b.Location = new System.Drawing.Point(194, 92);
            this.E_b.Name = "E_b";
            this.E_b.Size = new System.Drawing.Size(70, 21);
            this.E_b.TabIndex = 58;
            this.E_b.Text = "Edit";
            this.E_b.UseVisualStyleBackColor = true;
            // 
            // D_b
            // 
            this.D_b.AutoSize = true;
            this.D_b.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.D_b.Location = new System.Drawing.Point(283, 92);
            this.D_b.Name = "D_b";
            this.D_b.Size = new System.Drawing.Size(90, 21);
            this.D_b.TabIndex = 59;
            this.D_b.Text = "Delete";
            this.D_b.UseVisualStyleBackColor = true;
            // 
            // edit_b
            // 
            this.edit_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edit_b.Location = new System.Drawing.Point(12, 132);
            this.edit_b.Name = "edit_b";
            this.edit_b.Size = new System.Drawing.Size(361, 44);
            this.edit_b.TabIndex = 60;
            this.edit_b.Text = "Изменить";
            this.edit_b.UseVisualStyleBackColor = true;
            this.edit_b.Click += new System.EventHandler(this.edit_b_Click);
            // 
            // SetAccessForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(385, 188);
            this.Controls.Add(this.edit_b);
            this.Controls.Add(this.D_b);
            this.Controls.Add(this.E_b);
            this.Controls.Add(this.W_b);
            this.Controls.Add(this.R_b);
            this.Controls.Add(this.menuComboBox);
            this.Controls.Add(this.label2);
            this.MaximumSize = new System.Drawing.Size(403, 235);
            this.MinimumSize = new System.Drawing.Size(403, 235);
            this.Name = "SetAccessForm";
            this.Text = "Изменение прав";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox menuComboBox;
        private System.Windows.Forms.CheckBox R_b;
        private System.Windows.Forms.CheckBox W_b;
        private System.Windows.Forms.CheckBox E_b;
        private System.Windows.Forms.CheckBox D_b;
        private System.Windows.Forms.Button edit_b;
    }
}