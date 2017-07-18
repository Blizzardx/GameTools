﻿using Common.Tool;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace ExcelImproter.Project.DynamicCompile
{
    public class DynamicCompiler : Singleton<DynamicCompiler>
    {
        public void LoadClassAtFolder(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            List<string> fileList = new List<string>();

            // load all cs file
            var allCSFils = dir.GetFiles("*.cs");
            List<string> paths = new List<string>();

            for (int i = 0; i < allCSFils.Length; ++i)
            {
                var file = allCSFils[i];
                paths.Add(file.FullName);
            }
            LoadClass(paths.ToArray());
        }
        private void LoadClass(string[] paths)
        {
            Assembly assem = Assembly.GetAssembly(typeof(ReflectionManager));

            string[] sourcecodes = new string[paths.Length];
            for(int i=0;i<paths.Length;++i)
            {
                sourcecodes[i] = File.ReadAllText(paths[i]);
            }

            // 1.Create a new CSharpCodePrivoder instance  
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();

            // 2.Sets the runtime compiling parameters by crating a new CompilerParameters instance  
            CompilerParameters objCompilerParameters = new CompilerParameters();
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            
            objCompilerParameters.GenerateInMemory = true;

            // 3.CompilerResults: Complile the code snippet by calling a method from the provider  
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, sourcecodes);

            if (cr.Errors.HasErrors)
            {
                string strErrorMsg = cr.Errors.Count.ToString() + " Errors:";

                for (int x = 0; x < cr.Errors.Count; x++)
                {
                    strErrorMsg = strErrorMsg + "/r/nLine: " +
                                 cr.Errors[x].Line.ToString() + " - " +
                                 cr.Errors[x].ErrorText;
                }

                LogQueue.Instance.Enqueue("There were build erros, please modify your code." + strErrorMsg);
                return;
            }

            // 4. Invoke the method by using Reflection  
            Assembly objAssembly = cr.CompiledAssembly;
            object objClass = objAssembly.CreateInstance("Dynamicly.HelloWorld");

            if (objClass == null)
            {
                LogQueue.Instance.Enqueue("Error:  Couldn't load class ");
                return;
            }

            //object[] objCodeParms = new object[1];
            //objCodeParms[0] = "Allan.";

            //string strResult = (string)objClass.GetType().InvokeMember(
            //           "GetTime", BindingFlags.InvokeMethod, null, objClass, objCodeParms);
            //this.txtResult.Text = strResult;
        }
    }
}
