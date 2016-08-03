using System.Collections.Generic;
using ExcelImproter.Framework.Handler;
using ExcelImproter.Framework.Importer;
using ExcelImproter.Framework.Reader;
using GameConfigTools.Util;
using System;
using ExcelImproter.Framework.Exporter;

namespace ExcelImproter.Project
{
    public partial class AeroLevelUpConfigHandler : ExcelConfigHandlerBase
    {
        protected override void ImporterExcel(ExcelData data, out ImporterPkg outPkg, ref string errMsg)
        {
            outPkg = null;
        }

        protected override string GetConfigPath()
        {
            return "AeroLevelUpConfig.xlsx";
        }

        protected override void OnAutoParasLine(List<string> line, ref string errMsg)
        {
            
        }

        protected override void OnAutoParasBegin()
        {
        }
    }
}
