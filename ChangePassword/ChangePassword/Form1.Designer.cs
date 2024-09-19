namespace ChangePassword
{
    partial class Form1
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
            this.old_pass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.new_pass = new System.Windows.Forms.TextBox();
            this.change_b = new System.Windows.Forms.Button();
            this.status_timer = new System.Windows.Forms.Timer(this.components);
            this.rus_label = new System.Windows.Forms.Label();
            this.eng_label = new System.Windows.Forms.Label();
            this.caps_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // old_pass
            // 
            this.old_pass.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.old_pass.Location = new System.Drawing.Point(26, 32);
            this.old_pass.Name = "old_pass";
            this.old_pass.PasswordChar = '*';
            this.old_pass.Size = new System.Drawing.Size(236, 27);
            this.old_pass.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 10.8F);
            this.label1.Location = new System.Drawing.Point(23, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(239, 18);
            this.label1.TabIndex = 2;
            this.label1.Text = "Введите старый пароль";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Console", 10.8F);
            this.label2.Location = new System.Drawing.Point(30, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(228, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Введите новый пароль";
            // 
            // new_pass
            // 
            this.new_pass.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.new_pass.Location = new System.Drawing.Point(26, 94);
            this.new_pass.Name = "new_pass";
            this.new_pass.PasswordChar = '*';
            this.new_pass.Size = new System.Drawing.Size(236, 27);
            this.new_pass.TabIndex = 4;
            // 
            // change_b
            // 
            this.change_b.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.change_b.Location = new System.Drawing.Point(26, 148);
            this.change_b.Name = "change_b";
            this.change_b.Size = new System.Drawing.Size(236, 47);
            this.change_b.TabIndex = 5;
            this.change_b.Text = "Сменить пароль";
            this.change_b.UseVisualStyleBackColor = true;
            this.change_b.Click += new System.EventHandler(this.change_b_Click);
            // 
            // status_timer
            // 
            this.status_timer.Interval = 200;
            this.status_timer.Tick += new System.EventHandler(this.status_timer_Tick);
            // 
            // rus_label
            // 
            this.rus_label.AutoSize = true;
            this.rus_label.Location = new System.Drawing.Point(178, 209);
            this.rus_label.Name = "rus_label";
            this.rus_label.Size = new System.Drawing.Size(101, 16);
            this.rus_label.TabIndex = 13;
            this.rus_label.Text = "Язык: Русский";
            // 
            // eng_label
            // 
            this.eng_label.AutoSize = true;
            this.eng_label.Location = new System.Drawing.Point(155, 209);
            this.eng_label.Name = "eng_label";
            this.eng_label.Size = new System.Drawing.Size(124, 16);
            this.eng_label.TabIndex = 14;
            this.eng_label.Text = "Язык: Английский";
            // 
            // caps_label
            // 
            this.caps_label.AutoSize = true;
            this.caps_label.ForeColor = System.Drawing.Color.Red;
            this.caps_label.Location = new System.Drawing.Point(12, 209);
            this.caps_label.Name = "caps_label";
            this.caps_label.Size = new System.Drawing.Size(83, 16);
            this.caps_label.TabIndex = 15;
            this.caps_label.Text = "CAPS LOCK!";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(291, 234);
            this.Controls.Add(this.caps_label);
            this.Controls.Add(this.eng_label);
            this.Controls.Add(this.rus_label);
            this.Controls.Add(this.change_b);
            this.Controls.Add(this.new_pass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.old_pass);
            this.MaximumSize = new System.Drawing.Size(309, 281);
            this.MinimumSize = new System.Drawing.Size(309, 281);
            this.Name = "Form1";
            this.Text = "Смена пароля";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox old_pass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox new_pass;
        private System.Windows.Forms.Button change_b;
        private System.Windows.Forms.Timer status_timer;
        private System.Windows.Forms.Label rus_label;
        private System.Windows.Forms.Label eng_label;
        private System.Windows.Forms.Label caps_label;
    }
}