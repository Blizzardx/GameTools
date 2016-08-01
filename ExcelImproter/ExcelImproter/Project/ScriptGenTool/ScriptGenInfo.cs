using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelImporter.Importer
{
    public class ScriptGenInfo
    {
        public string className;
        public List<ScriptGenElementInfo> elements;
    }
    public class ScriptGenElementInfo
    {
        public int index;
        public string classTypeName;
        public string rangeMin;
        public string rangeMax;
        public string desc;
        public string memberName;
        public bool isList;
        public bool isNullable;
    }
}
