﻿namespace BuildingMaterials
{
    partial class EditForm
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
            this.edit_b = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.name_t = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // edit_b
            // 
            this.edit_b.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.edit_b.Location = new System.Drawing.Point(13, 67);
            this.edit_b.Name = "edit_b";
            this.edit_b.Size = new System.Drawing.Size(283, 44);
            this.edit_b.TabIndex = 8;
            this.edit_b.Text = "Изменить";
            this.edit_b.UseVisualStyleBackColor = true;
            this.edit_b.Click += new System.EventHandler(this.edit_b_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(11, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(285, 20);
            this.label1.TabIndex = 7;
            this.label1.Text = "Название стройматериала";
            // 
            // name_t
            // 
            this.name_t.Font = new System.Drawing.Font("Lucida Console", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.name_t.Location = new System.Drawing.Point(13, 34);
            this.name_t.Name = "name_t";
            this.name_t.Size = new System.Drawing.Size(283, 27);
            this.name_t.TabIndex = 6;
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(307, 121);
            this.Controls.Add(this.edit_b);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.name_t);
            this.MaximumSize = new System.Drawing.Size(325, 168);
            this.MinimumSize = new System.Drawing.Size(325, 168);
            this.Name = "EditForm";
            this.Text = "EditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button edit_b;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox name_t;
    }
}