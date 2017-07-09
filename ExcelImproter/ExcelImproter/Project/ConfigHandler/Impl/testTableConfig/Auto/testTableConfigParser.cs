using Util;
using System.Collections.Generic;

public class testTableConfigParser
{
    private string m_strErrorMsg;

    public string GetErrorMsg()
    {
        return m_strErrorMsg;
    }
    public List<testTableConfig> ParserConfig(string[][] content)
    {
        List<testTableConfig> resultConfigTable = new List<testTableConfig>();
        for (int i = 0; i < content.Length; ++i)
        {
            resultConfigTable.Add(ParserLine(i, content[i]));

            if (string.IsNullOrEmpty(m_strErrorMsg))
            {
                return null;
            }
        }
        return resultConfigTable;
    }
    private testTableConfig ParserLine(int lineIndex, string[] values)
    {
		int tmpIndexOffset = 0;
		int skipCount = 0;

		testTableConfig configLineElement = new testTableConfig();

			if (!VaildUtil.TryConvert(values[0 + tmpIndexOffset], out configLineElement.id,0,100))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,0+1, configLineElement.id,0,100,"int","id");
	            return null;
            }

			if (!VaildUtil.TryConvert(values[1 + tmpIndexOffset], out configLineElement.name,null,null))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,1+1, configLineElement.name,null,null,"string","name");
	            return null;
            }

			if (!VaildUtil.TryConvert(values[2 + tmpIndexOffset], out configLineElement.costId,int.MinValue,int.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,2+1, configLineElement.costId,int.MinValue,int.MaxValue,"int","constId");
	            return null;
            }

			configLineElement.position = new testTableConfig.positionClass();
			if (!VaildUtil.TryConvert(values[3 + tmpIndexOffset], out configLineElement.position.x,double.MinValue,double.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,3+1, configLineElement.position.x,double.MinValue,double.MaxValue,"double","x");
	            return null;
            }
			if (!VaildUtil.TryConvert(values[4 + tmpIndexOffset], out configLineElement.position.y,double.MinValue,double.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,4+1, configLineElement.position.y,double.MinValue,double.MaxValue,"double","y");
	            return null;
            }
			if (!VaildUtil.TryConvert(values[5 + tmpIndexOffset], out configLineElement.position.z,double.MinValue,double.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,5+1, configLineElement.position.z,double.MinValue,double.MaxValue,"double","z");
	            return null;
            }

			configLineElement.nameMessageId = new testTableConfig.nameMessageIdClass();
			if (!VaildUtil.TryConvert(values[6 + tmpIndexOffset], out configLineElement.nameMessageId.id,int.MinValue,int.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,6+1, configLineElement.nameMessageId.id,int.MinValue,int.MaxValue,"int","id");
	            return null;
            }

			List<string> textureNameSourceList = null;
			if (!VaildUtil.TryConvert(values[7 + tmpIndexOffset], 0, out textureNameSourceList , out skipCount))
            {
                m_strErrorMsg = string.Format("{3} {0}.xlsx [{1},{2}]数组解析读取出现错误", "testTableConfig", lineIndex,7+1,"{desc}");
	            return null;
            }

			var textureNameList = new string[textureNameSourceList.Count];

			for(int i=0;i<textureNameSourceList.Count;++i)
			{		
				string subElement;	

				if (!VaildUtil.TryConvert(textureNameSourceList[i], out subElement,null,null))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]数组解析读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,i+1, subElement,null,null,"string","textureName");
					return null;
				}


				textureNameList[i] = subElement;
			}

			tmpIndexOffset += skipCount;

			List<string> vector4SourceList = null;
			if (!VaildUtil.TryConvert(values[8 + tmpIndexOffset], 0,out vector4SourceList ,out skipCount))
            {
                m_strErrorMsg = string.Format("{3} {0}.xlsx [{1},{2}]数组解析读取出现错误", "testTableConfig", lineIndex,8+1,"{desc}");
	            return null;
            }

			var vector4List = new testTableConfig.vector4Class[vector4SourceList.Count];
			
			for(int i=0;i<4;++i)
			{
				int startIndex = i * 4;

				var vector4Element = new testTableConfig.vector4Class();	
				vector4List[i] = vector4Element;				

				if (!VaildUtil.TryConvert(values[0 + startIndex], out vector4Element.x,double.MinValue,double.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,0+1, vector4Element.x,double.MinValue,double.MaxValue,"double","x");
					return null;
				}
				if (!VaildUtil.TryConvert(values[1 + startIndex], out vector4Element.t,int.MinValue,int.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,1+1, vector4Element.t,int.MinValue,int.MaxValue,"int","t");
					return null;
				}
				if (!VaildUtil.TryConvert(values[2 + startIndex], out vector4Element.z,double.MinValue,double.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,2+1, vector4Element.z,double.MinValue,double.MaxValue,"double","z");
					return null;
				}
				if (!VaildUtil.TryConvert(values[3 + startIndex], out vector4Element.w,int.MinValue,int.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,3+1, vector4Element.w,int.MinValue,int.MaxValue,"int","w");
					return null;
				}

			}
			tmpIndexOffset += skipCount;

			List<string> packageSourceList = null;
			if (!VaildUtil.TryConvert(values[9 + tmpIndexOffset], 0,out packageSourceList ,out skipCount))
            {
                m_strErrorMsg = string.Format("{3} {0}.xlsx [{1},{2}]数组解析读取出现错误", "testTableConfig", lineIndex,9+1,"{desc}");
	            return null;
            }

			var packageList = new testTableConfig.packageClass[packageSourceList.Count];
			
			for(int i=0;i<1;++i)
			{
				int startIndex = i * 1;

				var packageElement = new testTableConfig.packageClass();	
				packageList[i] = packageElement;				

				if (!VaildUtil.TryConvert(values[0 + startIndex], out packageElement.packageName,null,null))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,0+1, packageElement.packageName,null,null,"string","packageName");
					return null;
				}

			}
			tmpIndexOffset += skipCount;



		return configLineElement;
    }
}