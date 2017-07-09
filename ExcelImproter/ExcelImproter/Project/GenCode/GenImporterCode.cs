using ExcelImproter.Configs;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace ExcelImproter.Project.GenCode
{
    public class GenImporterCode
    {
        private const string m_strAutoImporterTemplatePath = "Config/ConfigHandler_Auto.txt";
        private const string m_strUserImporterTemplatePath = "Config/ConfigHandler_User.txt";
        private const string m_strProjectFolderPath = "../../Project/ConfigHandler/Impl/";

        private string m_strAutoImporterTemplate;
        private string m_strUserImporterTemplate;

        public void GenAutoImporterCode()
        {
            DirectoryInfo dir = new DirectoryInfo(SystemConst.Config.ParserConfigPath);
            List<string> fileList = new List<string>();

            // load all cs file
            var allCSFils = dir.GetFiles("*.cs");
            for(int i=0;i<allCSFils.Length;++i)
            {
                var file = allCSFils[i];

                if(!file.Name.EndsWith("parser"))
                {
                    fileList.Add(file.Name);
                }
            }
            

            for(int i=0;i<fileList.Count;++i)
            {
                var configName = fileList[i];

                ProcessFile(configName);
            }
        }
        private void ProcessFile(string configName)
        {
            var parser = configName + "parser" + ".cs";
            var data = configName + ".cs";

            FileUtils.EnsureFolder(m_strProjectFolderPath + configName);

            string subAutoFolder = m_strProjectFolderPath + configName + "/Auto/";
            string subUserFolder = m_strProjectFolderPath + configName + "/User/";

            if (Directory.Exists(subAutoFolder))
            {
                Directory.Delete(subAutoFolder, true);
            }
            FileUtils.EnsureFolder(subAutoFolder);

            File.Move(SystemConst.Config.ParserConfigPath + "/" + parser, subAutoFolder + parser);
            File.Move(SystemConst.Config.ParserConfigPath + "/" + data, subAutoFolder + data);

            string autoImportPath = subAutoFolder + "ConfigHandler_" + configName + ".cs";
            string userImportPath = subUserFolder + "ConfigHandler_" + configName + ".cs";

            File.WriteAllText(autoImportPath, GenAutoImporter(configName));
            if (!Directory.Exists(subUserFolder))
            {
                FileUtils.EnsureFolder(subUserFolder);
                File.WriteAllText(autoImportPath, GenUserImporter(configName));
            }

            RefreshProjectDirectory(subAutoFolder + parser);
            RefreshProjectDirectory(subAutoFolder + data);
            RefreshProjectDirectory(autoImportPath);
            RefreshProjectDirectory(userImportPath);
        }
        private void RefreshProjectDirectory(string userImportPath)
        {
            
        }
        private string GenAutoImporter(string configName)
        {
            StringBuilder res = new StringBuilder(m_strAutoImporterTemplate);
            res = res.Replace("{className}", configName);
            res = res.Replace("{parserClassName}", configName+"parser");

            return res.ToString();  
        }
        private string GenUserImporter(string configName)
        {
            StringBuilder res = new StringBuilder(m_strUserImporterTemplate);
            res = res.Replace("{className}", configName);

            return res.ToString();
        }
        private void InitTempalte()
        {
            m_strAutoImporterTemplate = File.ReadAllText(m_strAutoImporterTemplatePath);
            m_strUserImporterTemplate = File.ReadAllText(m_strUserImporterTemplatePath);
        }
    }
}
