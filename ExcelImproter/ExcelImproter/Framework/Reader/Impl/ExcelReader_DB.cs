using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;

namespace ExcelImproter.Framework.Reader
{

    public class ExcelReader_DB: IExcelReader
    {
        public ExcelData ReadExcel(string path)
        {
            var time = DateTime.Now;
            ExcelData res = new ExcelData();
            res.DataList = new List<ExcelTable>();
            string strConn 
                = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" 
                + path 
                + ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";
            
            var sheets = GetExcelSheetName(strConn);
            foreach (var sheetName in sheets)
            {
                ExcelTable tmpTable = new ExcelTable();
                tmpTable.Data = ReadSheet(strConn, sheetName);
                res.DataList.Add(tmpTable);
            }
            var space = DateTime.Now - time;
            Console.WriteLine("cost time " + space.TotalMilliseconds);
            LogQueue.Instance.Enqueue("DB reader cost time " + space.TotalMilliseconds);
            return res;
        }
        private List<List<string>> ReadSheet(string strConn, string sheetName)
        {
            string strExcel = "select * from [" + sheetName + "]";
            OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);
            System.Data.DataTable dt = new System.Data.DataTable();
            myCommand.Fill(dt);
            List<List<string>> content = new List<List<string>>();

            foreach (DataRow dataRow in dt.AsEnumerable())
            {
                int tempColumnCount = dataRow.ItemArray.Length;
                List<string> line = new List<string>(tempColumnCount);
                for (int i = 0; i < tempColumnCount; i++)
                {
                    string elem = dataRow[i].ToString();
                    // check 
                    //if (dataRow[0].ToString().Equals("##"))
                    //{
                    //    return null;
                    //}
                    //if (UseAnnotation() && dataRow[0].ToString().StartsWith("#"))
                    //{
                    //    line.Add(null);
                    //}
                    //else
                    {
                        line.Add(elem);
                    }
                }
                content.Add(line);
            }
            return content;
        }
        private List<string> GetExcelSheetName(string strConn)
        {
            OleDbConnection Conn = new OleDbConnection(strConn);
            Conn.Open();
            List<string> result = new List<string>();
            System.Data.DataTable sheetNames = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            foreach (DataRow dr in sheetNames.Rows)
            {
                result.Add(dr[2].ToString());
            }
            return result;
        }
        protected virtual bool UseAnnotation()
        {
            return true;
        }
    }
}