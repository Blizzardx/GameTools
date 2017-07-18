using ExcelImproter.Configs;
using System.Text;
using System.Collections.Generic;
using System.IO;

namespace ExcelImproter.Project.GenCode
{
    public class GenImporterCode
    {
        private const string m_strAutoImporterTemplatePath = "Config/ConfigHandler_Auto.txt";
        private const string m_strProjectFolderPath = "../../Project/ConfigHandler/Impl/";

        private string m_strAutoImporterTemplate;
        private string m_strUserImporterTemplate;

        public void GenAutoImporterCode()
        {
            InitTempalte();

            DirectoryInfo dir = new DirectoryInfo(SystemConst.Config.ParserConfigPath);
            List<string> fileList = new List<string>();

            // load all cs file
            var allCSFils = dir.GetFiles("*.cs");
            for(int i=0;i<allCSFils.Length;++i)
            {
                var file = allCSFils[i];

                if(!file.Name.EndsWith("Parser.cs"))
                {
                    fileList.Add(file.Name.Substring(0,file.Name.Length - 3));
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
            var parser = configName + "Parser" + ".cs";
            var data = configName + ".cs";

            FileUtils.EnsureFolder(m_strProjectFolderPath + configName);

            string subAutoFolder = m_strProjectFolderPath + configName + "/";

            FileUtils.EnsureFolder(subAutoFolder);

            if(File.Exists(subAutoFolder + parser))
            {
                File.Delete(subAutoFolder + parser);
            }
            if (File.Exists(subAutoFolder + data))
            {
                File.Delete(subAutoFolder + data);
            }
            File.Copy(SystemConst.Config.ParserConfigPath + "/" + parser, subAutoFolder + parser);
            File.Copy(SystemConst.Config.ParserConfigPath + "/" + data, subAutoFolder + data);

            string autoImportPath = subAutoFolder + "ConfigHandler_" + configName + ".cs";

            if(!File.Exists(autoImportPath))
            {
                File.WriteAllText(autoImportPath, GenAutoImporter(configName));
            }

            RefreshProjectDirectory(subAutoFolder + parser);
            RefreshProjectDirectory(subAutoFolder + data);
            RefreshProjectDirectory(autoImportPath);
        }
        private void RefreshProjectDirectory(string userImportPath)
        {
            
        }
        private string GenAutoImporter(string configName)
        {
            StringBuilder res = new StringBuilder(m_strAutoImporterTemplate);
            res = res.Replace("{className}", configName);
            res = res.Replace("{parserClassName}", configName+"Parser");

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
        }
    }
}
