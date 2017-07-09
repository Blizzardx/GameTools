namespace ExcelImproter.Framework.Reader
{
    public interface IExcelReader 
    {
        ExcelData ReadExcel(string path);
    }
}
