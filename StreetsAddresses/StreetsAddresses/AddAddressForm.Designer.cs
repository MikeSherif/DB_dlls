namespace StreetsAddresses
{
    partial class AddAddressForm
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
            this.add_b = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.streetBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.num_t = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // add_b
            // 
            this.add_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.add_b.Location = new System.Drawing.Point(12, 120);
            this.add_b.Name = "add_b";
            this.add_b.Size = new System.Drawing.Size(283, 44);
            this.add_b.TabIndex = 8;
            this.add_b.Text = "Добавить";
            this.add_b.UseVisualStyleBackColor = true;
            this.add_b.Click += new System.EventHandler(this.add_b_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(70, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(177, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Название улицы";
            // 
            // streetBox
            // 
            this.streetBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.streetBox.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.streetBox.FormattingEnabled = true;
            this.streetBox.Location = new System.Drawing.Point(12, 33);
            this.streetBox.Name = "streetBox";
            this.streetBox.Size = new System.Drawing.Size(283, 28);
            this.streetBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(85, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "Номер улицы";
            // 
            // num_t
            // 
            this.num_t.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.num_t.Location = new System.Drawing.Point(12, 87);
            this.num_t.Name = "num_t";
            this.num_t.Size = new System.Drawing.Size(283, 27);
            this.num_t.TabIndex = 11;
            // 
            // AddAddressForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(308, 172);
            this.Controls.Add(this.num_t);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.streetBox);
            this.Controls.Add(this.add_b);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(326, 219);
            this.MinimumSize = new System.Drawing.Size(326, 219);
            this.Name = "AddAddressForm";
            this.Text = "Добавление";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button add_b;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox streetBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox num_t;
    }
}