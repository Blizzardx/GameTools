using System.Collections.Generic;

namespace ExcelImproter.Framework.Reader
{
    public class ExcelData 
    {
        public List<ExcelTable> DataList;

        public string[][] GetMergedContent()
        {
            int tmpcount = 0;

            foreach (var sheetElem in DataList)
            {
                tmpcount += sheetElem.Data.Count;
            }

            // merge sheet 
            string[][] finalData = new string[tmpcount][];
            int index = 0;
            foreach (var table in DataList)
            {
                foreach (var elemLine in table.Data)
                {
                    finalData[index] = elemLine.ToArray();
                    ++index;
                }
            }

            return finalData;
        }
    }

    public class ExcelTable
    {
        public List<List<string>> Data;
    }
}
