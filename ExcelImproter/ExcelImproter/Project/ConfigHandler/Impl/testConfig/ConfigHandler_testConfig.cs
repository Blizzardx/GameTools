using ExcelImproter.Framework.Reader;
using ExcelImproter.Project;
using System;
using System.Collections.Generic;

public partial class ConfigHandler_testConfig : ConfigHandlerBase
{
    public override string HandleConfig(ExcelData content)
    {
        var sourcedata = content.GetMergedContent();
        testConfigParser parser = new testConfigParser();
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
        testConfigParser parser = new testConfigParser();
        return parser.CheckIsConfigExistKey(sourcedata, id, keyValue);
    }
	private string ParserData(List<testConfig> data)
    {
        throw new NotImplementedException();
    }
}