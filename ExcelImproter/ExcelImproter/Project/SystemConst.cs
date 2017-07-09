using Common.Config;

public class SystemConst
{
    public static PathConfig Config;
}
public class PathConfig : XmlConfigBase
{
    public string XmlConfigPath;
    public string ParserConfigPath;
    public string ExcelConfigPath;
}