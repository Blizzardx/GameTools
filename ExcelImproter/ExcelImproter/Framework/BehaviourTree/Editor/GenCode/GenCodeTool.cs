using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common.Tool;
using ExcelImproter.Configs;
using ExcelImproter.Framework.BehaviourTree.Editor.Controller;
using GameConfigTools.Util;

namespace ExcelImproter.Framework.BehaviourTree.Editor.GenCode
{
    class GenCodeTool:Singleton<GenCodeTool>
    {
        private string m_strClassTemplate;
        private string m_strParamterTemplate;
        private string m_strParserParamterTemplate;

        public void GenCode(List<BTNodeTypeInfoData> info)
        {
            m_strClassTemplate = FileUtils.ReadStringFile(BTConfigSetting.BTNodeTypeCodeGenClassTemplatePath);
            m_strParamterTemplate = FileUtils.ReadStringFile(BTConfigSetting.BTNodeTypeCodeGenParamterTemplatePath);
            m_strParserParamterTemplate = FileUtils.ReadStringFile(BTConfigSetting.BTNodeTypeCodeGenParserTemplatePath);

            if (null == info || info.Count == 0)
            {
                return;
            }
            if (Directory.Exists(BTConfigSetting.BTNodeTypeCodeGenOutputPath))
            {
                // do clear first
                Directory.Delete(BTConfigSetting.BTNodeTypeCodeGenOutputPath, true);
            }
            // load template file
            for (int i = 0; i < info.Count; ++i)
            {
                GenElementCode(BTConfigSetting.BTNodeTypeCodeGenOutputPath, info[i]);
            }
        }
        private void GenElementCode(string outputPath, BTNodeTypeInfoData data)
        {
            if (null == data || data.m_ParamList == null || data.m_ParamList.Count == 0)
            {
                // do noting
                return;
            }
            outputPath += data.m_strName + ".cs";
            StringBuilder res = new StringBuilder();
            res.Append(m_strClassTemplate);

            // {2} - class name
            string className = data.m_strName;
            res.Replace("{2}", className);

            StringBuilder paramterDefine = new StringBuilder();
            StringBuilder paramterParser = new StringBuilder();

            for (int i = 0; i < data.m_ParamList.Count; ++i)
            {
                // {0} paramter name 
                // {1} paramter type
                var paramType = ConvertTypeToCsharpType(data.m_ParamList[i].m_Type);
                var paramName = data.m_ParamList[i].m_strName;

                StringBuilder paramterTemplate = new StringBuilder();
                paramterTemplate.Append(m_strParamterTemplate);
                paramterTemplate.Replace("{0}", paramName);
                paramterTemplate.Replace("{1}", paramType);

                paramterDefine.Append('\n');
                paramterDefine.Append(paramterTemplate);


                StringBuilder paramterParserTemplate = new StringBuilder();
                paramterParserTemplate.Append(m_strParserParamterTemplate);
                paramterParserTemplate.Replace("{0}", paramName);

                paramterParser.Append('\n');
                paramterParser.Append(paramterParserTemplate);
            }
            //{3} paramter define
            res.Replace("{3}", paramterDefine.ToString());

            //{4} paramter parser
            res.Replace("{4}", paramterParser.ToString());

            // save to file
            FileUtils.WriteStringFile(outputPath,res.ToString());
        }
        private string ConvertTypeToCsharpType(BTNodeParamDataType type)
        {
            switch (type)
            {
                case BTNodeParamDataType.Bool:
                    return "bool";
                case BTNodeParamDataType.Byte:
                    return "byte";
                case BTNodeParamDataType.Double:
                    return "double";
                case BTNodeParamDataType.I16:
                    return "short";
                case BTNodeParamDataType.I32:
                    return "int";
                case BTNodeParamDataType.I64:
                    return "long";
                case BTNodeParamDataType.String:
                    return "string";
            }
            return "";
        }
    }

    
}
