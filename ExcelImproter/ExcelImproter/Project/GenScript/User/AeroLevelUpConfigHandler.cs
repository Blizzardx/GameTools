using System.Collections.Generic;
using ExcelImproter.Framework.Handler;
using ExcelImproter.Framework.Importer;
using ExcelImproter.Framework.Reader;
using GameConfigTools.Util;
using System;

namespace ExcelImproter.Project
{
    public partial class AeroLevelUpConfigHandler : ExcelConfigHandlerBase
    {
        protected override void OnAutoParasBegin()
        {

        }
        protected override void OnAutoParasLine(int sheetIndex, int row, List<string> line, ref string errMsg)
        {
            
        }
        protected override void ImporteExcel(ExcelData data, out ImporterPkg outPkg, ref string errMsg)
        {
            outPkg = null;
        }
        protected override string GetConfigPath()
        {
            return "aeroLevelUpConfig.xlsx";
        }
    }
}
