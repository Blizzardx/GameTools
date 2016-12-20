using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelImproter.Framework.Reader;

namespace ExcelImproter.Framework.Handler
{
    class ParserExcelDataToPackData
    {
        private ExcelReaderBase m_Reader;

        private ExcelReaderBase GetReader()
        {
            if (null == m_Reader)
            {
                m_Reader = new ExcelReader_ER();
            }
            return m_Reader;
        }
        public ExcelData DoParser(string path)
        {
            var content = GetReader().ReadExcel(path);

            return content;
        }
    }
}
