using System.Collections.Generic;
using System.Xml.Serialization;
using Common.Config;

[XmlRoot("root")]
public class GenScriptXmlConfig : XmlConfigBase
{
    [XmlArray("classConfigList")]
    public List<GenScriptClassXmlConfig> classConfigList { get; set; }
}
public class GenScriptClassXmlConfig : XmlConfigBase
{
    [XmlAttribute("className")]
    public string className { get; set; }

    [XmlArray("lineConfigList")]
    public List<GenScriptLineXmlConfig> lineConfigList { get; set; }
}

public class GenScriptLineXmlConfig : XmlConfigBase
{
    //[XmlAttribute("index")]
    //public int index { get; set; }

    [XmlAttribute("classTypeName")]
    public string classTypeName { get; set; }

    [XmlAttribute("memberName")]
    public string memberName { get; set; }

    [XmlAttribute("desc")]
    public string desc { get; set; }

    [XmlAttribute("rangeMin")]
    public string rangeMin { get; set; }

    [XmlAttribute("rangeMax")]
    public string rangeMax { get; set; }

    [XmlAttribute("isList")]
    public bool isList { get; set; }

    [XmlAttribute("isNullable")]
    public bool isNullable { get; set; }
}
