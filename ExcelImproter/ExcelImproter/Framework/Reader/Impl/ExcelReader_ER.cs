using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Excel;

namespace ExcelImproter.Framework.Reader
{
    public class ExcelReader_ER : IExcelReader
    {
        public ExcelData ReadExcel(string path)
        {
            var time = DateTime.Now;
            ExcelData res = new ExcelData();
            res.DataList = new List<ExcelTable>();
            string filePath = path;
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);

            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            var result = excelReader.AsDataSet();

            for (int i = 0; i < result.Tables.Count; ++i)
            {
                int rowCount = result.Tables[i].Rows.Count;
                int colCount = result.Tables[i].Columns.Count;
                ExcelTable table = ReadSheet(rowCount, colCount, result.Tables[i]);
                res.DataList.Add(table);
            }
            
            excelReader.Close();
            stream.Close();
            var space = DateTime.Now - time;
            Console.WriteLine("cost time " + space.TotalMilliseconds);
            LogQueue.Instance.Enqueue("ER reader cost time " + space.TotalMilliseconds);
            return res;
        }
        private ExcelTable ReadSheet(int rowCount, int colCount, DataTable dataTable)
        {
            ExcelTable table = new ExcelTable();
            table.Data = new List<List<string>>();

            for (int row = 0; row < rowCount; ++row)
            {
                var properties = new List<string>();
                for (int col = 0; col < colCount; ++col)
                {
                    var elem = dataTable.Rows[row][col].ToString();
                    if (elem.Equals("##"))
                    {
                        return null;
                    }
                    if (UseAnnotation() && dataTable.Rows[row][0].ToString().StartsWith("#"))
                    {
                        //properties.Add(null);
                        break;
                    }
                    else
                    {
                        properties.Add(elem);
                    }

                }
                if (properties.Count > 0)
                {
                    table.Data.Add(properties);
                }
            }

            return table;
        }
        protected virtual bool UseAnnotation()
        {
            return true;
        }
    }
}
