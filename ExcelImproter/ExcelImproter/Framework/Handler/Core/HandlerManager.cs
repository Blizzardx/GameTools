using System;
using System.Collections.Generic;
using Common.Tool;
using ExcelImproter.Framework.Exporter;
using ExcelImproter.Framework.Importer;

namespace ExcelImproter.Framework.Handler
{
    public class HandlerManager:Singleton<HandlerManager>
    {
        private Dictionary<string, IHandler> m_HandlerFactory;

        public HandlerManager()
        {
            // check init
            AutoRegister();
        }
        public void HandleConfig(string name,string path)
        {
            IHandler handler = null;
            m_HandlerFactory.TryGetValue(name, out handler);
            if (null == handler)
            {
                LogQueue.Instance.Enqueue("can't load handler by name "+ name);
                return;
            }
            try
            {
                var importer = handler.GetImporter();

                ImporterPkg pkg = null;
                importer.Import(path, out pkg);

                var exporter = handler.GetExporter();
                exporter.Export(pkg);

            }
            catch (Exception e)
            {
                LogQueue.Instance.Enqueue("Exception on handle config " + name + " " + e.Message);
                LogQueue.Instance.Enqueue(e.StackTrace);
            }
        }
        private void AutoRegister()
        {
            if (null != m_HandlerFactory)
            {
                return;
            }
            m_HandlerFactory = new Dictionary<string, IHandler>();

            var list = ReflectionManager.Instance.GetTypeByBase(typeof (IHandler));
            for (int i = 0; i < list.Count; ++i)
            {
                var handler = Activator.CreateInstance(list[i]) as IHandler;
                if (m_HandlerFactory.ContainsKey(handler.GetImporter().GetPath()))
                {
                    LogQueue.Instance.Enqueue("already exist config path " + handler.GetImporter().GetPath() + " at importer " + list[i].ToString());
                    continue;
                }
                
                m_HandlerFactory.Add(handler.GetImporter().GetPath(), handler);
            }
        }
    }
}
