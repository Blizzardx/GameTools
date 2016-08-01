using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Excel;

namespace ExcelImproter.Framework.Reader
{
    public class ExcelReader_ER : ExcelReaderBase
    {
        public override ExcelData ReadExcel(string path)
        {
            var time = DateTime.Now;
            ExcelData res = new ExcelData();
            res.DataList = new List<ExcelTable>();
            string filePath = path;
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = excelReader.AsDataSet();

            var classes = new List<List<string>>();
            for (int i = 0; i < result.Tables.Count; ++i)
            {
                int rowCount = result.Tables[i].Rows.Count;
                int colCount = result.Tables[i].Columns.Count;
                ExcelTable table = new ExcelTable();
                
                for (int row = 0; row < rowCount; ++row)
                {
                    var properties = new List<string>();
                    for (int col = 0; col < colCount; ++col)
                    {
                        var elem = result.Tables[i].Rows[row][col].ToString();
                        properties.Add(elem);
                    }
                    classes.Add(properties);
                }
                table.Data = classes;
                res.DataList.Add(table);
            }

            //6. Free resources (IExcelDataReader is IDisposable)
            excelReader.Close();
            stream.Close();
            var space = DateTime.Now - time;
            Console.WriteLine("cost time " + space.TotalMilliseconds);
            LogQueue.instance.Add("ER reader cost time " + space.TotalMilliseconds);
            return res;
        }
    }
}
