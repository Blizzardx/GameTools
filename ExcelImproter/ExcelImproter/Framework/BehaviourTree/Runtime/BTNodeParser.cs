using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Config;
using Common.Tool;
using ExcelImproter.Configs;

namespace ExcelImproter.Framework.BehaviourTree
{
    class BTNodeParser:Singleton<BTNodeParser>
    {
        private BehaviourTreePlanData m_PlanList;

        public BehaviourTreePlanData LoadBTPlan(string path)
        {
            string xmlContent = FileUtils.ReadStringFile(path);
            if (string.IsNullOrEmpty(xmlContent))
            {
                return null;
            }
            m_PlanList = XmlConfigBase.DeSerialize<BehaviourTreePlanData>(xmlContent);
            return m_PlanList;
        }
        public BehaviourTreePlanData GetPlanList()
        {
            return m_PlanList;
        }
        public void SavePlan(string path, BehaviourTreePlanData planList)
        {
            m_PlanList = planList;
            BehaviourTreePlanData content = planList;
            var strcontent = XmlConfigBase.Serialize(content);
            FileUtils.WriteStringFile(path, strcontent);
        }
        public void Save(string path)
        {
            BehaviourTreePlanData content = new BehaviourTreePlanData();
            content.m_PlanList = new List<BTNodeData>();
            BTNodeData data = new BTNodeData();
            data.m_strName = "Action";
            data.m_strType = "BTAction";
            data.m_ChildList = new List<BTNodeData>();
            data.m_ParamList = new List<BTNodeParamData>();
            BTNodeParamData param = new BTNodeParamData();
            param.m_strName = "id";
            param.m_Type = BTNodeParamDataType.I32;
            param.m_Value = "100";
            data.m_ParamList.Add(param);
            content.m_PlanList.Add(data);
            var strcontent = XmlConfigBase.Serialize(content);
            FileUtils.WriteStringFile(path, strcontent);
        }
    }
}
