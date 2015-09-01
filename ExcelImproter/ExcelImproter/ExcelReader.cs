using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace ExcelImproter
{
    class ExcelReader
    {
        public bool ReadExcel(string filePath,ref List<string[][]> content)
        {
            string errMsg = string.Empty;

            int rowCount = 0;
            int columnCount = 0;
            string[] sheetNames = this.GetSheetNames(filePath, ref errMsg, ref rowCount, ref columnCount);
            if (sheetNames == null)
            {
                return false;
            }

            List<List<List<string>>> tempList = new List<List<List<string>>>();

            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;" + "data source=" + filePath +
                             ";Extended Properties='Excel 12.0; HDR=NO; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);

            try
            {
                conn.Open();
                foreach (string sheetName in sheetNames)
                {
                    //第一个sheet页
                    List<List<string>> sheetList = new List<List<string>>();
                    tempList.Add(sheetList);
                    String sqlGetColumnName = "SELECT * FROM [" + sheetName + "$]";
                    System.Data.OleDb.OleDbDataAdapter odasheet =
                        new System.Data.OleDb.OleDbDataAdapter(sqlGetColumnName, conn);
                    DataSet dsSheet = new DataSet();

                    odasheet.Fill(dsSheet, sheetName);
                    System.Data.DataTable table = dsSheet.Tables[0];

                    foreach (DataRow dataRow in table.AsEnumerable())
                    {
                        //是否被注释掉了
                        bool annotation = false;
                        object firstObj = dataRow.ItemArray.First();
                        if (firstObj != null)
                        {
                            //sheet页的第一个单元格内，是两个"##"表示整个sheet页不读
                            if (firstObj.ToString().Equals("##"))
                            {
                                goto LOOP;
                            }
                            //如果对注释生效，同时第一个字符为"#"，表示注释一行
                            if (UseAnnotation() && firstObj.ToString().StartsWith("#"))
                            {
                                annotation = true;
                            }
                        }
                        List<string> columnList = new List<string>();
                        sheetList.Add(columnList);
                        int tempColumnCount = dataRow.ItemArray.Length;
                        for (int i = 0; i < columnCount; i++)
                        {
                            //如果注释了，一整行都为空
                            if (annotation)
                            {
                                columnList.Add(null);
                            }
                            else
                            {
                                if (i < tempColumnCount)
                                {
                                    columnList.Add(dataRow[i] == null ? null : dataRow[i].ToString());
                                }
                                else
                                {
                                    columnList.Add(null);
                                }
                            }
                        }
                    }
                    LOOP:
                    continue;
                }
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }

            List<string[][]> sheetValues = new List<string[][]>();
            for (int i = 0; i < tempList.Count; i++)
            {
                List<List<string>> list = tempList[i];
                string[][] values = new string[list.Count][];
                for (int j = 0; j < list.Count; j++)
                {
                    values[j] = list[j].ToArray();
                }
                sheetValues.Add(values);
            }

            content = sheetValues;

            return true;
        }
        public string[] GetSheetNames(string filePath, ref string errMsg, ref int rowCount, ref int columnCount)
        {
            string[] sheetNames;
            Application excel = new Microsoft.Office.Interop.Excel.Application();
            //Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.ApplicationClass();
            Workbook workBook = null;
            try
            {
                //设置Excel不可见
                excel.Visible = false;
                excel.DisplayAlerts = false;

                workBook = excel.Workbooks.Open(filePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                sheetNames = new string[workBook.Sheets.Count];
                //计算出数组有多大
                for (int i = 1; i <= workBook.Sheets.Count; i++)
                {
                    Worksheet sheet = workBook.Sheets[i];
                    rowCount += sheet.UsedRange.Rows.Count;
                    if (columnCount == 0)
                    {
                        columnCount = sheet.UsedRange.Columns.Count;
                    }
                    sheetNames[i - 1] = sheet.Name;
                }
                return sheetNames;
            }
            catch (Exception e)
            {
                errMsg = e.Message;
                return null;
            }
            finally
            {
                if (workBook != null)
                {
                    workBook.Close(Missing.Value, Missing.Value, Missing.Value);
                }
                excel.Workbooks.Close();
                excel.Quit();

            }
        }
        protected virtual bool UseAnnotation()
        {
            return true;
        }
    }
}