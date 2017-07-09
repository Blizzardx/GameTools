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

            if (!string.IsNullOrEmpty(m_strErrorMsg))
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
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,0+1, values[0 + tmpIndexOffset],0,100,"int","id");
	            return null;
            }

			if (!VaildUtil.TryConvert(values[1 + tmpIndexOffset], out configLineElement.name,null,null))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,1+1, values[1 + tmpIndexOffset],null,null,"string","name");
	            return null;
            }

			if (!VaildUtil.TryConvert(values[2 + tmpIndexOffset], out configLineElement.costId,int.MinValue,int.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,2+1, values[2 + tmpIndexOffset],int.MinValue,int.MaxValue,"int","constId");
	            return null;
            }

			configLineElement.position = new testTableConfig.positionClass();
			if (!VaildUtil.TryConvert(values[3 + tmpIndexOffset], out configLineElement.position.x,double.MinValue,double.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,3+1, values[3 + tmpIndexOffset],double.MinValue,double.MaxValue,"double","x");
	            return null;
            }
			if (!VaildUtil.TryConvert(values[4 + tmpIndexOffset], out configLineElement.position.y,double.MinValue,double.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,4+1, values[4 + tmpIndexOffset],double.MinValue,double.MaxValue,"double","y");
	            return null;
            }
			if (!VaildUtil.TryConvert(values[5 + tmpIndexOffset], out configLineElement.position.z,double.MinValue,double.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,5+1, values[5 + tmpIndexOffset],double.MinValue,double.MaxValue,"double","z");
	            return null;
            }

			configLineElement.nameMessageId = new testTableConfig.nameMessageIdClass();
			if (!VaildUtil.TryConvert(values[6 + tmpIndexOffset], out configLineElement.nameMessageId.id,int.MinValue,int.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,6+1, values[6 + tmpIndexOffset],int.MinValue,int.MaxValue,"int","id");
	            return null;
            }

			List<string> textureNameSourceList = null;
			if (!VaildUtil.TryConvert(values,7 + tmpIndexOffset, 0, out textureNameSourceList , out skipCount))
            {
                m_strErrorMsg = string.Format("{3} {0}.xlsx [{1},{2}]数组解析读取出现错误", "testTableConfig", lineIndex,7+1,"{desc}");
	            return null;
            }

			var textureNameTmpList = new List<string>();

			for(int i=0;i<textureNameSourceList.Count;++i)
			{		
				string subElement;	

				if (!VaildUtil.TryConvert(textureNameSourceList[i], out subElement,null,null))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]数组解析读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,i+1, textureNameSourceList[i],null,null,"string","textureName");
					return null;
				}


				textureNameTmpList.Add(subElement);
			}
			configLineElement.textureName = textureNameTmpList.ToArray();
			tmpIndexOffset += skipCount;

			List<string> vector4SourceList = null;
			if (!VaildUtil.TryConvert(values,8 + tmpIndexOffset, 1,out vector4SourceList ,out skipCount))
            {
                m_strErrorMsg = string.Format("{3} {0}.xlsx [{1},{2}]数组解析读取出现错误", "testTableConfig", lineIndex,8+1,"{desc}");
	            return null;
            }

			var vector4TmpList = new List<testTableConfig.vector4Class>();
			
			for(int i=0;i<vector4SourceList.Count;i += 4)
			{
				int startIndex = i;

				var vector4Element = new testTableConfig.vector4Class();	
				vector4TmpList.Add(vector4Element);

				if (!VaildUtil.TryConvert(vector4SourceList[0 + startIndex], out vector4Element.x,double.MinValue,double.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,0+1, vector4SourceList[0 + startIndex],double.MinValue,double.MaxValue,"double","x");
					return null;
				}
				if (!VaildUtil.TryConvert(vector4SourceList[1 + startIndex], out vector4Element.y,double.MinValue,double.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,1+1, vector4SourceList[1 + startIndex],double.MinValue,double.MaxValue,"double","y");
					return null;
				}
				if (!VaildUtil.TryConvert(vector4SourceList[2 + startIndex], out vector4Element.z,double.MinValue,double.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,2+1, vector4SourceList[2 + startIndex],double.MinValue,double.MaxValue,"double","z");
					return null;
				}
				if (!VaildUtil.TryConvert(vector4SourceList[3 + startIndex], out vector4Element.w,double.MinValue,double.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,3+1, vector4SourceList[3 + startIndex],double.MinValue,double.MaxValue,"double","w");
					return null;
				}

			}
			configLineElement.vector4 = vector4TmpList.ToArray();
			tmpIndexOffset += skipCount;

			List<string> packageSourceList = null;
			if (!VaildUtil.TryConvert(values,9 + tmpIndexOffset, 0,out packageSourceList ,out skipCount))
            {
                m_strErrorMsg = string.Format("{3} {0}.xlsx [{1},{2}]数组解析读取出现错误", "testTableConfig", lineIndex,9+1,"{desc}");
	            return null;
            }

			var packageTmpList = new List<testTableConfig.packageClass>();
			
			for(int i=0;i<packageSourceList.Count;i += 1)
			{
				int startIndex = i;

				var packageElement = new testTableConfig.packageClass();	
				packageTmpList.Add(packageElement);

				if (!VaildUtil.TryConvert(packageSourceList[0 + startIndex], out packageElement.packageName,null,null))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testTableConfig", lineIndex,0+1, packageSourceList[0 + startIndex],null,null,"string","packageName");
					return null;
				}

			}
			configLineElement.package = packageTmpList.ToArray();
			tmpIndexOffset += skipCount;



		return configLineElement;
    }
}