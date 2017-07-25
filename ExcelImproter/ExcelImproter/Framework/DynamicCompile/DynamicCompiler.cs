using System;
using Common.Tool;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ExcelImproter.Project.DynamicCompile
{
    public class DynamicCompiler : Singleton<DynamicCompiler>
    {
        private CodeDomProvider m_Provider;
        private CompilerParameters m_CompilerParams;
        private CompilerResults m_CompilerResult;
        private Dictionary<string, Type> m_ClassFindMap;
        private List<Type> m_ClassList;

        public DynamicCompiler()
        {
            // 2.Sets the runtime compiling parameters by crating a new CompilerParameters instance  
            m_CompilerParams = new CompilerParameters();
            m_CompilerParams.ReferencedAssemblies.Add("ExcelImproter.exe");
            m_CompilerParams.GenerateInMemory = true;
            m_CompilerParams.GenerateExecutable = false;
            //m_CompilerParams.CompilerOptions = "/optimize";

            // 3.CompilerResults: Complile the code snippet by calling a method from the provider  
            m_Provider = CodeDomProvider.CreateProvider("CSharp");
        }
        public void CompileClassAtDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                return;
            }
            DirectoryInfo dir = new DirectoryInfo(path);
            List<string> fileList = new List<string>();

            // load all cs file
            var allCSFils = dir.GetFiles("*.cs", SearchOption.AllDirectories);
            List<string> paths = new List<string>();

            for (int i = 0; i < allCSFils.Length; ++i)
            {
                var file = allCSFils[i];
                paths.Add(file.FullName);
            }
            CompileClass(paths.ToArray());
        }
        private void CompileClass(string[] paths)
        {
            m_CompilerResult = m_Provider.CompileAssemblyFromFile(m_CompilerParams, paths);

            if (m_CompilerResult.Errors.HasErrors)
            {
                LogQueue.Instance.Enqueue("There were build erros, please modify your code.");

                for (int x = 0; x < m_CompilerResult.Errors.Count; x++)
                {
                   var strErrorMsg =
                                m_CompilerResult.Errors[x].FileName + " " +
                                 m_CompilerResult.Errors[x].Line + " - " +
                                 m_CompilerResult.Errors[x].ErrorText;

                    LogQueue.Instance.Enqueue(strErrorMsg);
                }

                return;
            }

            //// 4. Invoke the method by using Reflection  
            //Assembly objAssembly = cr.CompiledAssembly;
            //object objClass = objAssembly.CreateInstance("Dynamicly.HelloWorld");

            //if (objClass == null)
            //{
            //    LogQueue.Instance.Enqueue("Error:  Couldn't load class ");
            //    return;
            //}

            //object[] objCodeParms = new object[1];
            //objCodeParms[0] = "Allan.";

            //string strResult = (string)objClass.GetType().InvokeMember(
            //           "GetTime", BindingFlags.InvokeMethod, null, objClass, objCodeParms);
            //this.txtResult.Text = strResult;

            InitReflection();
        }
        public void InitReflection()
        {
            m_ClassFindMap = new Dictionary<string, Type>();
            Assembly assem = m_CompilerResult.CompiledAssembly;
            m_ClassList = new List<Type>(assem.GetTypes());

            for (int i = 0; i < m_ClassList.Count; ++i)
            {
                var elem = m_ClassList[i];
                if (!m_ClassFindMap.ContainsKey(elem.Name))
                {
                    m_ClassFindMap.Add(elem.Name, elem);
                }
            }
        }
        public List<Type> GetTypeByBase(Type baseType)
        {
            if (null == m_ClassList)
            {
                return new List<Type>();
            }
            List<Type> resList = new List<Type>();
            for (int i = 0; i < m_ClassList.Count; ++i)
            {
                var elem = m_ClassList[i];
                if (elem.BaseType == baseType)
                {
                    // add to list
                    resList.Add(elem);
                }
                else if (!elem.IsInterface && !elem.IsAbstract && baseType.IsInterface && baseType.IsAssignableFrom(elem))
                {
                    // add to list
                    resList.Add(elem);
                }
            }
            return resList;
        }
    }
}
