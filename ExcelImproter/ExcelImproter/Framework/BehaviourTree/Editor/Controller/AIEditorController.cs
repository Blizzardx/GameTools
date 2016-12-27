using System.Collections.Generic;
using System.Windows.Forms;

namespace ExcelImproter.Framework.BehaviourTree.Editor.Controller
{
    public class CustomViewNode : TreeNode
    {
        private BTNodeData m_Data;

        public void SetData(BTNodeData data)
        {
            m_Data = data;
        }
        public BTNodeData GetData()
        {
            return m_Data;
        }
    }

    public class AIEditorController
    {
        public List<CustomViewNode> ConvertDateNodeListToViewNodeList(BehaviourTreePlanData planList)
        {
            if (null == planList || null == planList.m_PlanList)
            {
                return null;
            }
            List<CustomViewNode> list = new List<CustomViewNode>(planList.m_PlanList.Count);
            for (int i = 0; i < planList.m_PlanList.Count; ++i)
            {
                list.Add(ConverDataNodeToViewNode(planList.m_PlanList[i]));
            }
            return list;
        }
        private CustomViewNode ConverDataNodeToViewNode(BTNodeData dataNode)
        {
            CustomViewNode node = new CustomViewNode();
            node.SetData(dataNode);
            node.Text = dataNode.m_strType + ":" + dataNode.m_strName;

            for (int i = 0; dataNode.m_ChildList != null && i < dataNode.m_ChildList.Count; ++i)
            {
                node.Nodes.Add(ConverDataNodeToViewNode(dataNode.m_ChildList[i]));
            }
            return node;
        }
        public BehaviourTreePlanData ConvertViewNodeListToDataNodeList(TreeNodeCollection nodeList)
        {
            BehaviourTreePlanData plan = new BehaviourTreePlanData();
            plan.m_PlanList = new List<BTNodeData>(nodeList.Count);
            foreach (var node in nodeList)
            {
                CustomViewNode realNode = node as CustomViewNode;
                plan.m_PlanList.Add(realNode.GetData());
            }
            return plan;
        }
    }
}
