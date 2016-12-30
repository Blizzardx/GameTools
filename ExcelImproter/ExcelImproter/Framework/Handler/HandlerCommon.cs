using System.Collections.Generic;
using System.IO;
using Common.Tool;

namespace ExcelImproter.Framework.Handler
{
    class HandlerCommon : Singleton<HandlerCommon>
    {
        public class FileDataInfo
        {
            public FileInfo m_Info;
            public string m_strName;
            public string m_strSubPath;
            public string m_strFullPath;

            public FileDataInfo(FileInfo info)
            {
                m_Info = info;
                m_strFullPath = m_Info.FullName;
                m_strFullPath = m_strFullPath.Replace('\\', '/');
                HandleConfig(m_strFullPath, ref m_strName, ref m_strSubPath);
            }
            public static void HandleConfig(string fullPath, ref string name, ref string subPath)
            {
                fullPath = fullPath.Replace('\\', '/');
                int index = fullPath.LastIndexOf('/');
                name = fullPath.Substring(index + 1);
                subPath = fullPath.Substring(0, index + 1);
            }
        }
        private string m_strTargetFolderPath;
        private List<FileDataInfo> m_ConfigElemInfoList;
         
        public HandlerCommon()
        {
            m_ConfigElemInfoList = new List<FileDataInfo>();
        }
        public void SetConfigFolderPath(string path)
        {
            m_strTargetFolderPath = path;
        }
        public void Refresh()
        {
            m_ConfigElemInfoList.Clear();
            var list = GetAllReadyConfigPath();
            if (null == list || list.Count == 0)
            {
                return;
            }
            for (int i = 0; i < list.Count; ++i)
            {
                m_ConfigElemInfoList.Add(new FileDataInfo(list[i]));
            }
        }
        public List<FileDataInfo> GetAllReadyConfig()
        {
            return m_ConfigElemInfoList;
        }
        public List<FileInfo> GetAllReadyConfigPath()
        {
            if (null == m_strTargetFolderPath || !Directory.Exists(m_strTargetFolderPath))
            {
                return null;
            }
            DirectoryInfo dir = new DirectoryInfo(m_strTargetFolderPath);
            List<FileInfo> res = new List<FileInfo>();
            res.AddRange(dir.GetFiles());
            return res;
        }
    }
}
