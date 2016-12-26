using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Tool;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;
using GameConfigTools.Util;

namespace ExcelImproter.Framework.BehaviourTree.Editor.GenCode
{
    class GenCodeTool:Singleton<GenCodeTool>
    {
        public void GenCode(string outputPath, List<BTNodeTypeInfoData> info)
        {
            if (null == info || info.Count == 0)
            {
                return;
            }
            if (Directory.Exists(outputPath))
            {
                // do clear first
                Directory.Delete(outputPath, true);
            }
            // load template file
            for (int i = 0; i < info.Count; ++i)
            {
                GenElementCode(outputPath,info[i]);
            }
        }
        private void GenElementCode(string outputPath, BTNodeTypeInfoData data)
        {
            if (null == data || data.m_ParamList == null || data.m_ParamList.Count == 0)
            {
                // do noting
                return;
            }
            string className = data.m_strName;
            for (int i = 0; i < data.m_ParamList.Count; ++i)
            {
                var paramType = data.m_ParamList[i].m_Type;
                var paramName = data.m_ParamList[i].m_strName;
            }
        }
    }

    
}
