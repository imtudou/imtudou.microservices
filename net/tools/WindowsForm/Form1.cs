using MaticsoftGenerateEntityUI.WindowsForm;

using SqlSugar;

using System;
using System.Collections;
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

        private string serviceFolder = "Service";
        private string entityFolder = "Entitys";
        private string iserverFolder = "IServices";
        private string modelFolder = "Models";
        private string repositoryFolder = "Repositorys";
        private string apiFolder = "BusinessAPI";



        /// <summary>
        /// 生成实体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

                if (string.IsNullOrWhiteSpace(this.txt_SelectPath.Text))
                {
                    this.txt_Show.Text = "选择文件存放路径！";
                    MessageBox.Show("选择文件存放路径！");
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
                string path = this.txt_SelectPath.Text + this.txt_TableName.Text +".cs";
                db.DbFirst.Where(this.txt_TableName.Text)
                          .CreateClassFile(this.txt_SelectPath.Text)
                          ;



                //foreach (var item in db.DbMaintenance.GetColumnInfosByTableName(this.txt_TableName.Text))
                //{
                //    //string entityName = item.Name.ToUpper();/*Format class name*/
                //    //db.MappingTables.Add(entityName, item.Name);
                //    //foreach (var col in db.DbMaintenance.GetColumnInfosByTableName(item.Name))
                //    //{
                //    //    db.MappingColumns.Add(col.DbColumnName.ToUpper() /*Format class property name*/, col.DbColumnName, entityName);
                //    //}
                //}
                //db.DbFirst.IsCreateAttribute().CreateClassFile("c:\\Demo\\8", "Models");


                string orginStr = System.IO.File.ReadAllText(path);
                StringBuilder sb = Form1.RemoveMark(orginStr);
                this.txt_Show.Text = sb.ToString();
                MessageBox.Show("生成完成！，文件保存路径："+ path);
            }
            catch (Exception ex)
            {
                this.txt_Show.Text = ex.Message;
                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 生成模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "请选择文件保存路径";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txt_SelectPath.Text = dialog.SelectedPath;
            }
        }


        private static string CreateEntity(List<DbColumnInfo> dbColumnInfos)
        {
            var str = new StringPlus();
            str.AppendLine("using System;");
            str.AppendLine("using System.Linq;");
            str.AppendLine("using System.Text;");
            str.AppendLine();
            str.AppendLine("namespace Models");
            str.AppendLine("{");
            foreach (var item in dbColumnInfos)
            {
                str.AppendLine("{");
            }
            str.AppendLine("}");
            return str.Value;
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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.txt_SelectPath.Text = @"D:\";
        }
    }
}
