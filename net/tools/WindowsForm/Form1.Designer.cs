namespace WindowsForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_ConnectionString = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txt_SelectPath = new System.Windows.Forms.TextBox();
            this.txt_TableName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_Show = new System.Windows.Forms.TextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(683, 106);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(105, 48);
            this.button3.TabIndex = 10;
            this.button3.Text = "生成模板";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.txt_ConnectionString);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.txt_SelectPath);
            this.panel1.Controls.Add(this.txt_TableName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(847, 201);
            this.panel1.TabIndex = 8;
            // 
            // txt_ConnectionString
            // 
            this.txt_ConnectionString.Location = new System.Drawing.Point(147, 41);
            this.txt_ConnectionString.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_ConnectionString.Name = "txt_ConnectionString";
            this.txt_ConnectionString.Size = new System.Drawing.Size(513, 25);
            this.txt_ConnectionString.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(87, 132);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "路径：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(49, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "连接字符串：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(683, 41);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 46);
            this.button1.TabIndex = 0;
            this.button1.Text = "生成实体";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(556, 128);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(105, 26);
            this.button2.TabIndex = 10;
            this.button2.Text = "浏览...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txt_SelectPath
            // 
            this.txt_SelectPath.Location = new System.Drawing.Point(147, 128);
            this.txt_SelectPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_SelectPath.Name = "txt_SelectPath";
            this.txt_SelectPath.Size = new System.Drawing.Size(377, 25);
            this.txt_SelectPath.TabIndex = 8;
            // 
            // txt_TableName
            // 
            this.txt_TableName.Location = new System.Drawing.Point(147, 75);
            this.txt_TableName.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_TableName.Name = "txt_TableName";
            this.txt_TableName.Size = new System.Drawing.Size(513, 25);
            this.txt_TableName.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(97, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "表名：";
            // 
            // txt_Show
            // 
            this.txt_Show.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txt_Show.Location = new System.Drawing.Point(0, 201);
            this.txt_Show.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txt_Show.Multiline = true;
            this.txt_Show.Name = "txt_Show";
            this.txt_Show.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_Show.Size = new System.Drawing.Size(847, 294);
            this.txt_Show.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 495);
            this.Controls.Add(this.txt_Show);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txt_SelectPath;
        private System.Windows.Forms.TextBox txt_TableName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_Show;
        private System.Windows.Forms.TextBox txt_ConnectionString;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}

