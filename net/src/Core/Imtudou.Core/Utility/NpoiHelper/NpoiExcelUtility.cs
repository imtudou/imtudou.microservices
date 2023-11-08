namespace Imtudou.Core.Utility.NpoiHelper
{
    using NPOI.HSSF.UserModel;
    using NPOI.SS.Formula.Functions;
    using NPOI.SS.UserModel;
    using NPOI.XSSF.UserModel;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    /// <summary>
    /// NPOI导出
    /// </summary>
    public class NpoiExcelUtility
    {
        private XSSFWorkbook _workBook = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        public NpoiExcelUtility()
        {
            _workBook = new XSSFWorkbook();
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputStream"></param>
        /// <returns></returns>
        public DataTable UploadFileStream(Stream inputStream)
        {
            DataTable dt = new DataTable();
            var workbook = WorkbookFactory.Create(inputStream);
            if (workbook == null) { return null; }
            ISheet sheet = workbook.GetSheetAt(0);

            //表头  
            IRow header = sheet.GetRow(sheet.FirstRowNum);
            for (int i = 0; i < header.LastCellNum; i++)
            {
                object obj = GetValueType(header.GetCell(i));
                if (obj != null || !string.IsNullOrEmpty(obj.ToString()))
                    dt.Columns.Add(new DataColumn(obj.ToString()));
            }

            return dt;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputStream"></param>
        /// <param name="startTitleRow">起始标题行</param>
        /// <param name="startReadRow">起始数据行</param>
        public List<T> UploadFileStream<T>(Stream inputStream, int startTitleRow = 0, int startReadRow = 1) where T : class, new()
        {
            var workbook = WorkbookFactory.Create(inputStream);
            if (workbook == null) { return null; }
            ISheet sheet = workbook.GetSheetAt(0);

            //实例化T数组
            List<T> list = new List<T>();
            if (sheet != null)
            {
                //一行最后一个cell的编号 即总的列数
                IRow cellNum = sheet.GetRow(startTitleRow);
                int num = cellNum.LastCellNum;
                //获取泛型对象T的所有注解
                PropertyInfo[] peroperties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                //每行转换为单个T对象
                for (int i = startReadRow; i <= sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    var obj = new T();
                    for (int j = 0; j < num; j++)
                    {
                        //列名称
                        var colName = cellNum.GetCell(j) + "";
                        string propName = "";
                        //去找注解对应的对象字段名
                        foreach (PropertyInfo property in peroperties)
                        {
                            object[] objs = property.GetCustomAttributes(typeof(DescriptionAttribute), true);
                            if (objs.Length > 0 && ((DescriptionAttribute)objs[0]).Description == colName)
                            {
                                propName = property.Name;
                                //没有数据的单元格都默认是null
                                ICell cell = row.GetCell(j);
                                if (cell != null)
                                {
                                    var value = row.GetCell(j).ToString();
                                    //获取对象的属性类型
                                    //string str = property.PropertyType.FullName;                                        
                                    var propType = property.PropertyType;
                                    if (propType == typeof(string))
                                    {
                                        typeof(T).GetProperty(propName).SetValue(obj, value, null);
                                    }
                                    else if (propType == typeof(DateTime) || propType == typeof(DateTime?))
                                    {
                                        //如果等于空
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            typeof(T).GetProperty(propName).SetValue(obj, null, null);
                                        }
                                        else
                                        {
                                            if (HSSFDateUtil.IsCellDateFormatted(cell))
                                            {
                                                var pdt = cell.DateCellValue;
                                                typeof(T).GetProperty(propName).SetValue(obj, pdt, null);
                                            }
                                            else
                                            {
                                                typeof(T).GetProperty(propName).SetValue(obj, null, null);
                                            }
                                           
                                        }
                                    }
                                    else if (propType == typeof(bool))
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            typeof(T).GetProperty(propName).SetValue(obj, null, null);
                                        }
                                        else
                                        {
                                            bool pb = Convert.ToBoolean(value);
                                            typeof(T).GetProperty(propName).SetValue(obj, pb, null);
                                        }

                                    }
                                    else if (propType == typeof(Int16) || propType == typeof(Int16?))
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            typeof(T).GetProperty(propName).SetValue(obj, null, null);
                                        }
                                        else
                                        {
                                            short pi16 = Convert.ToInt16(value);
                                            typeof(T).GetProperty(propName).SetValue(obj, pi16, null);
                                        }
                                    }
                                    else if (propType == typeof(Int32) || propType == typeof(Int32?))
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            typeof(T).GetProperty(propName).SetValue(obj, null, null);
                                        }
                                        else
                                        {
                                            int pi32 = Convert.ToInt32(value);
                                            typeof(T).GetProperty(propName).SetValue(obj, pi32, null);
                                        }

                                    }
                                    else if (propType == typeof(Int64) || propType == typeof(Int64?))
                                    {
                                        if (string.IsNullOrEmpty(value))
                                        {
                                            typeof(T).GetProperty(propName).SetValue(obj, null, null);
                                        }
                                        else
                                        {
                                            long pi64 = Convert.ToInt64(value);
                                            typeof(T).GetProperty(propName).SetValue(obj, pi64, null);
                                        }
                                    }
                                    else if (propType == typeof(byte))
                                    {
                                        byte pb = Convert.ToByte(value);
                                        typeof(T).GetProperty(propName).SetValue(obj, pb, null);
                                    }
                                    else
                                    {
                                        typeof(T).GetProperty(propName).SetValue(obj, null, null);
                                    }
                                }
                            }

                            if (objs.Length > 0 && ((DescriptionAttribute)objs[0]).Description == "Row") // 返回当前行
                            {
                                typeof(T).GetProperty(property.Name).SetValue(obj, i, null);
                            }



                        }
                    }
                    list.Add(obj);
                }
            }
            return list;
        }



        /// <summary>
        /// 下载文件
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="heads"></param>
        /// <param name="datas"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] DownloadFileByte<T>(List<NpoiHeadModel> heads, List<T> datas)
            where T : class, new()
        {
            byte[] bytes = new byte[0];
            CreatExcelSheet(heads, datas);
            using (XLSXMemoryStream stream = new XLSXMemoryStream())
            {
                stream.AllowClose = false;
                _workBook.Write(stream);
                bytes = stream.GetBuffer();
                stream.Close();
            }

            return bytes;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="heads"></param>
        /// <param name="datas"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task<Stream> DownloadFileStreamAsync<T>(List<NpoiHeadModel> heads, List<T> datas)
            where T : class, new()
        {
            CreatExcelSheet(heads, datas);
            XLSXMemoryStream stream = new XLSXMemoryStream();
            {
                stream.AllowClose = false;
                _workBook.Write(stream);
                stream.Seek(0, SeekOrigin.Begin);
                await stream.FlushAsync();
                return stream;
            }
        }

        /// <summary>
        /// 创建excel，当行数大于65536时，自动分割成几个sheet，sheet名称为sheetName_i
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="heads"></param>
        /// <param name="datas"></param>
        public void CreatExcelSheet<T>(List<NpoiHeadModel> heads, List<T> datas)
            where T : class, new()
        {
            int rowMax = 65535;
            int intNum = datas.Count / rowMax;
            int remainder = datas.Count % rowMax;
            if (remainder >= 0)// 没有数据生成表头
            {
                intNum++;
            }

            for (int i = 0; i < intNum; i++)
            {
                var subDatas = datas.Skip(i * rowMax).Take(rowMax).ToList();
                string subSheet = "sheet_" + (i + 1);
                ISheet sheet = _workBook.CreateSheet(subSheet);
                this.ListToExcel(heads, subDatas, sheet);
            }
        }

        /// <summary>
        /// 将数据保存到sheet里
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="heads"></param>
        /// <param name="datas"></param>
        /// <param name="sheet"></param>
        private void ListToExcel<T>(List<NpoiHeadModel> heads, List<T> datas, ISheet sheet)
            where T : class, new()
        {
            ICellStyle style = _workBook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;
            style.VerticalAlignment = VerticalAlignment.Center;
            ICellStyle colStyle = _workBook.CreateCellStyle();
            colStyle.Alignment = HorizontalAlignment.Left;
            colStyle.VerticalAlignment = VerticalAlignment.Center;
            IFont font = _workBook.CreateFont();
            colStyle.SetFont(font);
            IRow headerRow = sheet.CreateRow(0);
            int headColIndex = 0;
            heads.ForEach(item =>
            {
                sheet.SetDefaultColumnStyle(headColIndex, style);

                ICell cell = headerRow.CreateCell(headColIndex);
                cell.SetCellValue(item.FiledDesc?.ToString());
                cell.CellStyle = colStyle;
                headColIndex++;
            });

            var props = typeof(T).GetProperties();
            for (int i = 0; i < datas.Count; i++)
            {
                IRow row = sheet.CreateRow(i + 1);
                var data = datas[i];
                if (props != null)
                {
                    heads = heads?.OrderBy(p => p.Sort)?.ToList();
                    for (int c = 0; c < heads.Count; c++)
                    {
                        ICell cell = row.CreateCell(c);
                        var fiedName = heads[c]?.FiledName;
                        var prop = props.Where(p => p.Name == fiedName).FirstOrDefault();
                        object value = prop?.GetValue(data, null);
                        cell.SetCellValue(value?.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 获取单元格类型
        /// </summary>
        /// <param name="cell">目标单元格</param>
        /// <returns></returns>
        private static object GetValueType(ICell cell)
        {
            if (cell == null)
                return null;
            switch (cell.CellType)
            {
                case CellType.Blank:
                    return null;
                case CellType.Boolean:
                    return cell.BooleanCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue;
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Error:
                    return cell.ErrorCellValue;
                case CellType.Formula:
                default:
                    return "=" + cell.CellFormula;
            }
        }
    }
}
