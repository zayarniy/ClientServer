
namespace Starter
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
            this.tbServerPath = new System.Windows.Forms.TextBox();
            this.btnServerStart = new System.Windows.Forms.Button();
            this.btnClientStart = new System.Windows.Forms.Button();
            this.tbClientPath = new System.Windows.Forms.TextBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServerPath
            // 
            this.tbServerPath.Location = new System.Drawing.Point(106, 17);
            this.tbServerPath.Name = "tbServerPath";
            this.tbServerPath.Size = new System.Drawing.Size(473, 20);
            this.tbServerPath.TabIndex = 2;
            this.tbServerPath.Text = "C:\\";
            this.tbServerPath.DoubleClick += new System.EventHandler(this.tbServerPath_DoubleClick);
            // 
            // btnServerStart
            // 
            this.btnServerStart.Location = new System.Drawing.Point(12, 17);
            this.btnServerStart.Name = "btnServerStart";
            this.btnServerStart.Size = new System.Drawing.Size(75, 23);
            this.btnServerStart.TabIndex = 3;
            this.btnServerStart.Text = "Server";
            this.btnServerStart.UseVisualStyleBackColor = true;
            this.btnServerStart.Click += new System.EventHandler(this.btnServerStart_Click);
            // 
            // btnClientStart
            // 
            this.btnClientStart.Location = new System.Drawing.Point(12, 55);
            this.btnClientStart.Name = "btnClientStart";
            this.btnClientStart.Size = new System.Drawing.Size(75, 23);
            this.btnClientStart.TabIndex = 4;
            this.btnClientStart.Text = "Client";
            this.btnClientStart.UseVisualStyleBackColor = true;
            this.btnClientStart.Click += new System.EventHandler(this.btnClientStart_Click);
            // 
            // tbClientPath
            // 
            this.tbClientPath.Location = new System.Drawing.Point(106, 57);
            this.tbClientPath.Name = "tbClientPath";
            this.tbClientPath.Size = new System.Drawing.Size(473, 20);
            this.tbClientPath.TabIndex = 5;
            this.tbClientPath.Text = "D:\\";
            this.tbClientPath.DoubleClick += new System.EventHandler(this.tbClientPath_DoubleClick);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(12, 89);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(567, 23);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 118);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tbClientPath);
            this.Controls.Add(this.btnClientStart);
            this.Controls.Add(this.btnServerStart);
            this.Controls.Add(this.tbServerPath);
            this.Name = "Form1";
            this.Text = "Starter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbServerPath;
        private System.Windows.Forms.Button btnServerStart;
        private System.Windows.Forms.Button btnClientStart;
        private System.Windows.Forms.TextBox tbClientPath;
        private System.Windows.Forms.Button btnClose;
    }
}

