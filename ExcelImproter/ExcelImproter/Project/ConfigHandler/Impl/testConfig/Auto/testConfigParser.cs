using Util;
using System.Collections.Generic;
using System;

public class testConfigParser
{
    private string m_strErrorMsg;

    public string GetErrorMsg()
    {
        return m_strErrorMsg;
    }
    public List<testConfig> ParserConfig(string[][] content)
    {
        List<testConfig> resultConfigTable = new List<testConfig>();
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
    private testConfig ParserLine(int lineIndex, string[] values)
    {
		int tmpIndexOffset = 0;
		int skipCount = 0;

		testConfig configLineElement = new testConfig();

			if (!VaildUtil.TryConvert(values[0 + tmpIndexOffset], out configLineElement.id,0,50))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,0+1, values[0 + tmpIndexOffset],0,50,"int","id");
	            return null;
            }			
			if (!VaildUtil.CheckRefrenceConfig("aiconfig", 5 ,values[0 + tmpIndexOffset]))
			{
					m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,0+1, values[0 + tmpIndexOffset],"aiconfig","id");
					return null;
			}

			if (!VaildUtil.TryConvert(values[1 + tmpIndexOffset], out configLineElement.nameMessageId,int.MinValue,int.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,1+1, values[1 + tmpIndexOffset],int.MinValue,int.MaxValue,"int","nameMessageId");
	            return null;
            }			
			if (!VaildUtil.CheckRefrenceConfig("messageConfig", 0 ,values[1 + tmpIndexOffset]))
			{
					m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,1+1, values[1 + tmpIndexOffset],"messageConfig","nameMessageId");
					return null;
			}

			configLineElement.position = new testConfig.positionClass();
			if (!VaildUtil.TryConvert(values[2 + tmpIndexOffset], out configLineElement.position.x,double.MinValue,double.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,2+1, values[2 + tmpIndexOffset],double.MinValue,double.MaxValue,"double","x");
	            return null;
            }			
			if (!VaildUtil.CheckRefrenceConfig("", 0 ,values[2 + tmpIndexOffset]))
			{
					m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,2+1, values[2 + tmpIndexOffset],"","x");
					return null;
			}
			if (!VaildUtil.TryConvert(values[3 + tmpIndexOffset], out configLineElement.position.y,double.MinValue,double.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,3+1, values[3 + tmpIndexOffset],double.MinValue,double.MaxValue,"double","y");
	            return null;
            }			
			if (!VaildUtil.CheckRefrenceConfig("", 0 ,values[3 + tmpIndexOffset]))
			{
					m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,3+1, values[3 + tmpIndexOffset],"","y");
					return null;
			}
			if (!VaildUtil.TryConvert(values[4 + tmpIndexOffset], out configLineElement.position.z,double.MinValue,double.MaxValue))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,4+1, values[4 + tmpIndexOffset],double.MinValue,double.MaxValue,"double","z");
	            return null;
            }			
			if (!VaildUtil.CheckRefrenceConfig("", 0 ,values[4 + tmpIndexOffset]))
			{
					m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,4+1, values[4 + tmpIndexOffset],"","z");
					return null;
			}

			configLineElement.resource = new testConfig.resourceClass();
			if (!VaildUtil.TryConvert(values[5 + tmpIndexOffset], out configLineElement.resource.textureName,null,null))
            {
                m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,5+1, values[5 + tmpIndexOffset],null,null,"string","textureName");
	            return null;
            }			
			if (!VaildUtil.CheckRefrenceConfig("", 0 ,values[5 + tmpIndexOffset]))
			{
					m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,5+1, values[5 + tmpIndexOffset],"","textureName");
					return null;
			}

			List<string> rotationSourceList = null;
			if (!VaildUtil.TryConvert(values,6 + tmpIndexOffset, 0,out rotationSourceList ,out skipCount))
            {
                m_strErrorMsg = string.Format("{3} {0}.xlsx [{1},{2}]数组解析读取出现错误", "testConfig", lineIndex,6+1,"{desc}");
	            return null;
            }

			var rotationTmpList = new List<testConfig.rotationClass>();
			
			for(int i=0;i<rotationSourceList.Count;i += 3)
			{
				int startIndex = i;

				var rotationElement = new testConfig.rotationClass();	
				rotationTmpList.Add(rotationElement);

				if (!VaildUtil.TryConvert(rotationSourceList[0 + startIndex], out rotationElement.x,double.MinValue,double.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,0+1, rotationSourceList[0 + startIndex],double.MinValue,double.MaxValue,"double","x");
					return null;
				}
							
				if (!VaildUtil.CheckRefrenceConfig("", 0 , rotationSourceList[0 + startIndex]))
				{
						m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,0+1, rotationSourceList[0 + startIndex],"","x");
						return null;
				}
				if (!VaildUtil.TryConvert(rotationSourceList[1 + startIndex], out rotationElement.y,double.MinValue,double.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,1+1, rotationSourceList[1 + startIndex],double.MinValue,double.MaxValue,"double","y");
					return null;
				}
							
				if (!VaildUtil.CheckRefrenceConfig("", 0 , rotationSourceList[1 + startIndex]))
				{
						m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,1+1, rotationSourceList[1 + startIndex],"","y");
						return null;
				}
				if (!VaildUtil.TryConvert(rotationSourceList[2 + startIndex], out rotationElement.z,double.MinValue,double.MaxValue))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,2+1, rotationSourceList[2 + startIndex],double.MinValue,double.MaxValue,"double","z");
					return null;
				}
							
				if (!VaildUtil.CheckRefrenceConfig("", 0 , rotationSourceList[2 + startIndex]))
				{
						m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,2+1, rotationSourceList[2 + startIndex],"","z");
						return null;
				}

			}
			configLineElement.rotation = rotationTmpList.ToArray();
			tmpIndexOffset += skipCount;

			List<string> attachNamePositionSourceList = null;
			if (!VaildUtil.TryConvert(values,7 + tmpIndexOffset, 0, out attachNamePositionSourceList , out skipCount))
            {
                m_strErrorMsg = string.Format("{3} {0}.xlsx [{1},{2}]数组解析读取出现错误", "testConfig", lineIndex,7+1,"{desc}");
	            return null;
            }

			var attachNamePositionTmpList = new List<string>();

			for(int i=0;i<attachNamePositionSourceList.Count;++i)
			{		
				string subElement;	

				if (!VaildUtil.TryConvert(attachNamePositionSourceList[i], out subElement,null,null))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]数组解析读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,i+1, attachNamePositionSourceList[i],null,null,"string","attachNamePosition");
					return null;
				}			
				if (!VaildUtil.CheckRefrenceConfig("", 0 , attachNamePositionSourceList[i]))
				{
						m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,i+1, attachNamePositionSourceList[i],"","attachNamePosition");
						return null;
				}


				attachNamePositionTmpList.Add(subElement);
			}
			configLineElement.attachNamePosition = attachNamePositionTmpList.ToArray();
			tmpIndexOffset += skipCount;

			List<string> audioSourceSourceList = null;
			if (!VaildUtil.TryConvert(values,8 + tmpIndexOffset, 0,out audioSourceSourceList ,out skipCount))
            {
                m_strErrorMsg = string.Format("{3} {0}.xlsx [{1},{2}]数组解析读取出现错误", "testConfig", lineIndex,8+1,"{desc}");
	            return null;
            }

			var audioSourceTmpList = new List<testConfig.audioSourceClass>();
			
			for(int i=0;i<audioSourceSourceList.Count;i += 1)
			{
				int startIndex = i;

				var audioSourceElement = new testConfig.audioSourceClass();	
				audioSourceTmpList.Add(audioSourceElement);

				if (!VaildUtil.TryConvert(audioSourceSourceList[0 + startIndex], out audioSourceElement.audioSourceName,null,null))
				{
					m_strErrorMsg = string.Format("{7} {0}.xlsx [{1},{2}]读取出现错误，{3}必须为{4} - {5} {6}型", "testConfig", lineIndex,0+1, audioSourceSourceList[0 + startIndex],null,null,"string","auduioSourceName");
					return null;
				}
							
				if (!VaildUtil.CheckRefrenceConfig("", 0 , audioSourceSourceList[0 + startIndex]))
				{
						m_strErrorMsg = string.Format("{5} {0}.xlsx [{1},{2}]读取出现错误，{3}在{4}中没找到", "testConfig", lineIndex,0+1, audioSourceSourceList[0 + startIndex],"","auduioSourceName");
						return null;
				}

			}
			configLineElement.audioSource = audioSourceTmpList.ToArray();
			tmpIndexOffset += skipCount;



		return configLineElement;
    }
	public bool CheckIsConfigExistKey(string[][] content, int index, string keyValue)
    {
        for (int i = 0; i < content.Length; ++i)
        {
            if (CheckIsConfigExistKey(content[i], index, keyValue))
            {
                return true;
            }
        }
        return false;
    }
    public bool CheckIsConfigExistKey(string[] values, int index, string keyValue)
    {
        int tmpIndexOffset = 0;
        int skipCount = 0;
        bool tmpMark = false;

		if (index == 0)
		{
			return values[0 + tmpIndexOffset] == keyValue;
		}

		if (index == 1)
		{
			return values[1 + tmpIndexOffset] == keyValue;
		}

		if (index == 2)
		{
			return values[2 + tmpIndexOffset] == keyValue;
		}
		if (index == 3)
		{
			return values[3 + tmpIndexOffset] == keyValue;
		}
		if (index == 4)
		{
			return values[4 + tmpIndexOffset] == keyValue;
		}

		if (index == 5)
		{
			return values[5 + tmpIndexOffset] == keyValue;
		}

        List<string> rotationSourceList = null;
        if (!VaildUtil.TryConvert(values, 6 + tmpIndexOffset, 0, out rotationSourceList, out skipCount))
        {
            throw new Exception("Error on check config is exist");
        }
        for (int i = 0; i < rotationSourceList.Count; i += 3)
        {
			if (index == 6)
            {
                tmpMark = true;
                if (rotationSourceList[0 + i] == keyValue)
                {
                    return true;
                }
            }
			if (index == 7)
            {
                tmpMark = true;
                if (rotationSourceList[1 + i] == keyValue)
                {
                    return true;
                }
            }
			if (index == 8)
            {
                tmpMark = true;
                if (rotationSourceList[2 + i] == keyValue)
                {
                    return true;
                }
            }
            
        }
        if (tmpMark)
        {
            return false;
        }
        tmpIndexOffset += skipCount;

        List<string> attachNamePositionSourceList = null;
        if (!VaildUtil.TryConvert(values[7 + tmpIndexOffset], 0, out attachNamePositionSourceList, out skipCount))
        {
            throw new Exception("Error on check config is exist");
        }
        if (index == 9)
        {
            for (int i = 0; i < attachNamePositionSourceList.Count; ++i)
            {
                if (attachNamePositionSourceList[i] == keyValue)
                {
                    return true;
                }
            }
            return false;
        }
        tmpIndexOffset += skipCount;

        List<string> audioSourceSourceList = null;
        if (!VaildUtil.TryConvert(values, 8 + tmpIndexOffset, 0, out audioSourceSourceList, out skipCount))
        {
            throw new Exception("Error on check config is exist");
        }
        for (int i = 0; i < audioSourceSourceList.Count; i += 1)
        {
			if (index == 10)
            {
                tmpMark = true;
                if (audioSourceSourceList[0 + i] == keyValue)
                {
                    return true;
                }
            }
            
        }
        if (tmpMark)
        {
            return false;
        }
        tmpIndexOffset += skipCount;



        return false;
    }
}