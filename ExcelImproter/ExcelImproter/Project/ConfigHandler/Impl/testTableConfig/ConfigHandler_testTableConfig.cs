using ExcelImproter.Framework.Reader;
using ExcelImproter.Project;
using System;
using System.Collections.Generic;

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
	 public override bool CheckRefrenceConfig(ExcelData content, int id, string keyValue)
    {
        var sourcedata = content.GetMergedContent();
        testTableConfigParser parser = new testTableConfigParser();
        return parser.CheckIsConfigExistKey(sourcedata, id, keyValue);
    }
	private string ParserData(List<testTableConfig> data)
    {
        throw new NotImplementedException();
    }
}