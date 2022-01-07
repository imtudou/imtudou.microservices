using SqlSugar;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(this.txt_ConnectionString.Text))
                {
                    this.txt_Show.Text = "连接字符串不能为空！";
                    MessageBox.Show("连接字符串不能为空！");
                    return;
                }

                if (string.IsNullOrWhiteSpace(this.txt_TableName.Text))
                {
                    this.txt_Show.Text = "表名不能为空！";
                    MessageBox.Show("表名不能为空！");
                    return;
                }

                //配置数据库连接
                SqlSugarClient db = new SqlSugarClient(
                                    new ConnectionConfig()
                                    {
                                        ConnectionString = this.txt_ConnectionString.Text,
                                        DbType = SqlSugar.DbType.SqlServer,//设置数据库类型
                                    IsAutoCloseConnection = true,//自动释放数据务，如果存在事务，在事务结束后释放
                                    InitKeyType = InitKeyType.Attribute //从实体特性中读取主键自增列信息
                                });
                string directoryPath = @"D:\";
                string path = @"D:\" + this.txt_TableName.Text + ".cs";
                //db.DbFirst.Where("tbMedicalRecord").CreateClassFile(@"D:\", "tbMedicalRecordEntity");

                db.DbFirst.Where(this.txt_TableName.Text).CreateClassFile(directoryPath);
                string orginStr = System.IO.File.ReadAllText(path);
                StringBuilder sb = Form1.RemoveMark(orginStr);
                this.txt_Show.Text = sb.ToString();//System.IO.File.ReadAllText(sb.ToString());
                MessageBox.Show("生成完成！，文件保存路径："+ path);
            }
            catch (Exception ex)
            {
                this.txt_Show.Text = ex.Message;
                MessageBox.Show(ex.Message);
            }

        }

        private static StringBuilder RemoveMark(string path)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var stringReader = new StringReader(path);
            var currentString = stringReader.ReadLine();
            while (currentString != null)
            {
                if (currentString.Contains("Default:") || currentString.Contains("Nullable:"))
                {
                    currentString = stringReader.ReadLine();
                    continue;
                }

                if (currentString.Contains("Desc:"))
                {
                    currentString = currentString.Replace("Desc:", string.Empty);
                }

                currentString = currentString.TrimEnd();
                stringBuilder.AppendLine(currentString);
                currentString = stringReader.ReadLine();
            }

            return stringBuilder;
        }
    }
}
