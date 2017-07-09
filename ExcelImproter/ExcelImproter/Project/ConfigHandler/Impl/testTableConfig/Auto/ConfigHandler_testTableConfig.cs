using ExcelImproter.Framework.Reader;
using ExcelImproter.Project;

public partial class ConfigHandler_testTableConfig : ConfigHandlerBase
{
    public override string HandleConfig(ExcelData content)
    {
        var sourcedata = content.GetMergedContent();
        testTableConfigParser parser = new testTableConfigParser();
        var data = parser.ParserConfig(sourcedata);

        if (!string.IsNullOrEmpty(parser.GetErrorMsg()))
        {
            return parser.GetErrorMsg();
        }

        return ParserData(data);
    }
}