namespace StreetsAddresses
{
    partial class EditAddressForm
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
            this.num_t = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.streetBox = new System.Windows.Forms.ComboBox();
            this.edit_b = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // num_t
            // 
            this.num_t.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.num_t.Location = new System.Drawing.Point(13, 86);
            this.num_t.Name = "num_t";
            this.num_t.Size = new System.Drawing.Size(283, 27);
            this.num_t.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(86, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Номер улицы";
            // 
            // streetBox
            // 
            this.streetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.streetBox.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.streetBox.FormattingEnabled = true;
            this.streetBox.Location = new System.Drawing.Point(13, 32);
            this.streetBox.Name = "streetBox";
            this.streetBox.Size = new System.Drawing.Size(283, 28);
            this.streetBox.TabIndex = 14;
            // 
            // edit_b
            // 
            this.edit_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edit_b.Location = new System.Drawing.Point(13, 119);
            this.edit_b.Name = "edit_b";
            this.edit_b.Size = new System.Drawing.Size(283, 44);
            this.edit_b.TabIndex = 13;
            this.edit_b.Text = "Изменить";
            this.edit_b.UseVisualStyleBackColor = true;
            this.edit_b.Click += new System.EventHandler(this.edit_b_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(71, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Название улицы";
            // 
            // EditAddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(308, 172);
            this.Controls.Add(this.num_t);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.streetBox);
            this.Controls.Add(this.edit_b);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(326, 219);
            this.MinimumSize = new System.Drawing.Size(326, 219);
            this.Name = "EditAddressForm";
            this.Text = "Изменение";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox num_t;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox streetBox;
        private System.Windows.Forms.Button edit_b;
        private System.Windows.Forms.Label label1;
    }
}