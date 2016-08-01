using ExcelImproter.Framework.Handler;
using ExcelImproter.Framework.Importer;
using ExcelImproter.Framework.Reader;

namespace ExcelImproter.Project.mmAdv
{
    class DiyConfigImproter:ExcelConfigHandlerBase
    {
        protected override void ImporterExcel(ExcelData data, out ImporterPkg outPkg, ref string errMsg)
        {
            outPkg = null;
        }

        protected override string GetConfigPath()
        {
            return "diyCharConfig.xlsx";
        }
    }
}
