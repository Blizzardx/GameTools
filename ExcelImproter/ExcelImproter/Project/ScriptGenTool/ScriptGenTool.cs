using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Config;
using GameConfigTools.Util;

namespace ExcelImporter.Importer
{
    public class ScriptGenTool
    {
        public void GenScript(ScriptGenInfo info)
        {
            string genScriptFilename = info.className + "Importer.cs";
            string genScriptUserFilename = genScriptFilename;

            string parasFileOutputPath = "../../Import/GenScript/Auto/" + genScriptUserFilename;
            string userFileOutputPath = "../../Import/GenScript/User/" + genScriptUserFilename;

            string resourcePath = "../../ScriptGenTool/";
            string autoParasTemplate = File.ReadAllText(resourcePath+"ExcelImporterAutoParasTemplate.txt");
            string autoParasUserTemplate = File.ReadAllText(resourcePath + "ExcelImporterUserTemplate.txt");
            string memberTemplate = File.ReadAllText(resourcePath + "ExcelImporterMemberTemplate.txt");
            string memberParasTempalte = File.ReadAllText(resourcePath + "ExcelImporterParasTemplate.txt");
            string memberListParasTempalte = File.ReadAllText(resourcePath + "ExcelImporterListParasTemplate.txt");
            string memberDatetimeParasTempalte = File.ReadAllText(resourcePath + "ExcelImporterDatetimeParasTemplate.txt");

            StringBuilder parasClass = new StringBuilder(autoParasTemplate);
            StringBuilder userClass = new StringBuilder(autoParasUserTemplate);
            userClass = userClass.Replace("{0}", info.className);
            parasClass = parasClass.Replace("{0}", info.className);

            StringBuilder memberContent = new StringBuilder();
            StringBuilder parasContent = new StringBuilder();
            for (int i = 0; i < info.elements.Count; ++i)
            {
                ScriptGenElementInfo elem = info.elements[i];

                StringBuilder member = new StringBuilder(memberTemplate);
                if (elem.isList)
                {
                    member = member.Replace("{classname}", "List<" + elem.classTypeName+">");
                }
                else
                {
                    if (elem.classTypeName.EndsWith("DateTime"))
                    {
                        member = member.Replace("{classname}", elem.classTypeName+"?");
                    }
                    else
                    {
                        member = member.Replace("{classname}", elem.classTypeName);
                    }
                }
                member = member.Replace("{membername}", elem.memberName);

                // add member
                memberContent.Append('\t');
                memberContent.Append('\t');
                memberContent.Append(member);

                StringBuilder paras = null;
                if (elem.isList)
                {
                    paras = new StringBuilder(memberListParasTempalte);
                    paras = paras.Replace("{index}", elem.index.ToString());
                    paras = paras.Replace("{classname}", elem.classTypeName);
                    paras = paras.Replace("{desc}", elem.desc);
                }
                else if (elem.classTypeName.EndsWith("DateTime"))
                {
                    paras = new StringBuilder(memberDatetimeParasTempalte);
                    paras = paras.Replace("{index}", elem.index.ToString());
                    paras = paras.Replace("{membername}", elem.memberName);
                    paras = paras.Replace("{nullable}", elem.isNullable.ToString().ToLower());
                    paras = paras.Replace("{classname}", elem.classTypeName);
                    paras = paras.Replace("{name}", elem.memberName);
                    paras = paras.Replace("{desc}", elem.desc);
                }
                else
                {
                    paras = new StringBuilder(memberParasTempalte);
                    paras = paras.Replace("{index}", elem.index.ToString());
                    paras = paras.Replace("{membername}", elem.memberName);
                    paras = paras.Replace("{min}", elem.rangeMin);
                    paras = paras.Replace("{max}", elem.rangeMax);
                    paras = paras.Replace("{classname}", elem.classTypeName);
                    paras = paras.Replace("{name}", elem.memberName);
                    paras = paras.Replace("{desc}", elem.desc);
                }
                

                // add paras
                parasContent.Append(paras);

            }

            parasClass = parasClass.Replace("{1}", memberContent.ToString());
            parasClass = parasClass.Replace("{2}", parasContent.ToString());

            // output paras script
            File.WriteAllText(parasFileOutputPath,parasClass.ToString());

            if (!File.Exists(userFileOutputPath))
            {
                // output user script
                File.WriteAllText(userFileOutputPath, userClass.ToString());
            }

            FixProjectFile(info.className);

        }
        private void FixProjectFile(string className)
        {
            string projectFilePath = "../../GameConfigTools.csproj";
            // check project file
            string projectFile = File.ReadAllText(projectFilePath);
            string autoFileDesc = File.ReadAllText("../../ScriptGenTool/ExcelImporterProjfixAuto.txt");
            string userFileDesc = File.ReadAllText("../../ScriptGenTool/ExcelImporterProjfixUser.txt");
            string testTemplate = File.ReadAllText("../../ScriptGenTool/testTemplate.txt");
            bool needUpdate = false;
            StringBuilder autoFile = new StringBuilder(autoFileDesc);
            autoFile = autoFile.Replace("{0}", className);
            if (projectFile.IndexOf(autoFile.ToString()) < 0)
            {
                // fix project file
                projectFile = projectFile.Replace(testTemplate, testTemplate + "\n" + autoFile.ToString());
                needUpdate = true;
            }

            StringBuilder userFile = new StringBuilder(userFileDesc);
            userFile = userFile.Replace("{0}", className);

            if (projectFile.IndexOf(userFile.ToString()) < 0)
            {
                // fix project file
                projectFile = projectFile.Replace(testTemplate, testTemplate + "\n" + userFile.ToString());
                needUpdate = true;
            }
            if (needUpdate)
            {
                // write file
                File.WriteAllText(projectFilePath, projectFile);
            }
        }
        public static void GenAllScript()
        {
            // load config
            ScriptGenTool tool = new ScriptGenTool();

            string configPath = "../../ScriptGenTool/ConfigXml.xml";
            string configContent = File.ReadAllText(configPath);

            var config = XmlConfigBase.DeSerialize<GenScriptXmlConfig>(configContent);

            if (null == config)
            {
                return;
            }

            for (int i = 0; i < config.classConfigList.Count; ++i)
            {
                var elemConfig = config.classConfigList[i];
                ScriptGenInfo info = new ScriptGenInfo();
                info.className = elemConfig.className;
                info.elements = new List<ScriptGenElementInfo>(elemConfig.lineConfigList.Count);
                foreach (var line in elemConfig.lineConfigList)
                {
                    ScriptGenElementInfo infoLine = new ScriptGenElementInfo();
                    infoLine.index = line.index-1;
                    infoLine.classTypeName = line.classTypeName;
                    infoLine.desc = line.desc;
                    infoLine.isList = line.isList;
                    infoLine.memberName = line.memberName;
                    infoLine.rangeMax = line.rangeMax;
                    infoLine.rangeMin = line.rangeMin;
                    infoLine.isNullable = line.isNullable;
                    if (infoLine.classTypeName.EndsWith("string"))
                    {
                        infoLine.rangeMax = "string.Empty";
                        infoLine.rangeMin = "string.Empty";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(infoLine.rangeMax))
                        {
                            infoLine.rangeMax = infoLine.classTypeName + ".MaxValue";
                        }
                        if (string.IsNullOrEmpty(infoLine.rangeMin))
                        {
                            infoLine.rangeMin = infoLine.classTypeName + ".MinValue";
                        }
                    }
                    info.elements.Add(infoLine);
                }
                // gen script
                tool.GenScript(info);
            }
        }
        // test 
        public static void Test()
        {
            ScriptGenTool tool = new ScriptGenTool();
            ScriptGenInfo info = new ScriptGenInfo();
            info.className = "DiyCharConfig";
            info.elements = new List<ScriptGenElementInfo>();

            ScriptGenElementInfo id = new ScriptGenElementInfo();
            id.index = 0;
            id.classTypeName = "int";
            id.desc = "模型id";
            id.memberName = "id";
            id.rangeMax = int.MaxValue.ToString();
            id.rangeMin = int.MinValue.ToString();
            id.isList = false;
            info.elements.Add(id);

            ScriptGenElementInfo positionId = new ScriptGenElementInfo();
            positionId.index = 2;
            positionId.classTypeName = "int";
            positionId.desc = "position id";
            positionId.memberName = "positionId";
            positionId.rangeMax = int.MaxValue.ToString();
            positionId.rangeMin = int.MinValue.ToString();
            positionId.isList = false;
            info.elements.Add(positionId);

            ScriptGenElementInfo vertexid = new ScriptGenElementInfo();
            vertexid.index = 3;
            vertexid.classTypeName = "int";
            vertexid.desc = "vertex id";
            vertexid.memberName = "vertexid";
            vertexid.rangeMax = int.MaxValue.ToString();
            vertexid.rangeMin = int.MinValue.ToString();
            vertexid.isList = false;
            info.elements.Add(vertexid);

            ScriptGenElementInfo radius = new ScriptGenElementInfo();
            radius.index = 3;
            radius.classTypeName = "int";
            radius.desc = "radius ";
            radius.memberName = "radius";
            radius.rangeMax = int.MaxValue.ToString();
            radius.rangeMin = int.MinValue.ToString();
            radius.isList = false;
            info.elements.Add(radius);

            ScriptGenElementInfo min = new ScriptGenElementInfo();
            min.index = 3;
            min.classTypeName = "int";
            min.desc = "min ";
            min.memberName = "min";
            min.rangeMax = int.MaxValue.ToString();
            min.rangeMin = int.MinValue.ToString();
            min.isList = false;
            info.elements.Add(min);

            ScriptGenElementInfo max = new ScriptGenElementInfo();
            max.index = 3;
            max.classTypeName = "int";
            max.desc = "max ";
            max.memberName = "max";
            max.rangeMax = int.MaxValue.ToString();
            max.rangeMin = int.MinValue.ToString();
            max.isList = false;
            info.elements.Add(max);

            ScriptGenElementInfo dirx = new ScriptGenElementInfo();
            dirx.index = 3;
            dirx.classTypeName = "int";
            dirx.desc = "dirx ";
            dirx.memberName = "dirx";
            dirx.rangeMax = int.MaxValue.ToString();
            dirx.rangeMin = int.MinValue.ToString();
            dirx.isList = false;
            info.elements.Add(dirx);

            ScriptGenElementInfo diry = new ScriptGenElementInfo();
            diry.index = 3;
            diry.classTypeName = "int";
            diry.desc = "diry ";
            diry.memberName = "diry";
            diry.rangeMax = int.MaxValue.ToString();
            diry.rangeMin = int.MinValue.ToString();
            diry.isList = false;
            info.elements.Add(diry);

            ScriptGenElementInfo dirz = new ScriptGenElementInfo();
            dirz.index = 3;
            dirz.classTypeName = "int";
            dirz.desc = "dirz ";
            dirz.memberName = "dirz";
            dirz.rangeMax = int.MaxValue.ToString();
            dirz.rangeMin = int.MinValue.ToString();
            dirz.isList = false;
            info.elements.Add(dirz);


            tool.GenScript(info);
        }
        public static void Test1()
        {
            GenScriptXmlConfig config = new GenScriptXmlConfig();
            config.classConfigList = new List<GenScriptClassXmlConfig>();

            GenScriptClassXmlConfig elem = new GenScriptClassXmlConfig();
            elem.className = "diyConfig";
            elem.lineConfigList = new List<GenScriptLineXmlConfig>();
            GenScriptLineXmlConfig elem1Line1 = new GenScriptLineXmlConfig();
            elem1Line1.classTypeName = "int";
            elem1Line1.desc = "id";
            elem1Line1.index = 0;
            elem1Line1.isList = false;
            elem1Line1.isNullable = false;
            elem1Line1.memberName = "id";
            elem1Line1.rangeMax = "1";
            elem1Line1.rangeMin = "2";
            elem.lineConfigList.Add(elem1Line1);

            GenScriptLineXmlConfig elem1Line2 = new GenScriptLineXmlConfig();
            elem1Line1.classTypeName = "int";
            elem1Line1.desc = "vertexid";
            elem1Line1.index = 0;
            elem1Line1.isList = false;
            elem1Line1.isNullable = false;
            elem1Line1.memberName = "vertexid";
            elem1Line1.rangeMax = "1";
            elem1Line1.rangeMin = "2";
            elem.lineConfigList.Add(elem1Line2);
            config.classConfigList.Add(elem);

            var content = XmlConfigBase.Serialize(config);
            File.WriteAllText("test.xml", content);

        }
    }
}
