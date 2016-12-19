using System.Collections.Generic;

/*
 * m_type = bool:m_Value = bool
 * m_type = byte:m_vlaue = sbyte;
 * m_type = i16:m_vlaue = short;
 * m_type = i32:m_vlaue = int;
 * m_type = i64:m_vlaue = long;
 * m_type = double:m_Value = double
 * m_type = string:m_vlaue = string;
 * m_type = struct:m_value = PackDataStruct
 * m_type = list:m_value = List<PackDataElement>
 * m_type = set:m_value = HashSet<PackDataElement>
 * m_type = map:m_value = Dictionary<PackDataElement,PackDataElement>
*/
public class PackDataElement
{
    public PackDataElementType m_Type;
    public short m_Id;
    public string m_strName;
    public object m_Value;
}
public class PackDataStruct
{
    public List<PackDataElement> m_ElemList;
}