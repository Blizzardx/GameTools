using Common.Config;

public class SystemConst
{
    public const string settingConfigPath = "Config/ExcelImporterSystemConfig.xml";
    public static PathConfig Config;
}
public class PathConfig : XmlConfigBase
{
    public string XmlConfigPath;
    public string ParserConfigPath;
    public string ExcelConfigPath;
}