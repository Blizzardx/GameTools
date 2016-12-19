using System;
using System.Collections.Generic;
using Common.Tool;
using ExcelImproter.Framework.Exporter;
using ExcelImproter.Framework.Importer;

namespace ExcelImproter.Framework.Handler
{
    public class ManualHandlerManager:Singleton<ManualHandlerManager>
    {
        private Dictionary<string, IManualHandler> m_HandlerFactory;

        public ManualHandlerManager()
        {
            // check init
            AutoRegister();
        }
        
        public void HandleConfig(string name,string path)
        {
            IManualHandler handler = null;
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
                LogQueue.Instance.Enqueue("begin importer config " + name);
                importer.Import(path, out pkg);
                //LogQueue.Instance.Enqueue("end importer config " + name);

                var exporter = handler.GetExporter();
                //LogQueue.Instance.Enqueue("begin export config " + name);
                exporter.Export(pkg);
                LogQueue.Instance.Enqueue("end export config " + name);

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
            m_HandlerFactory = new Dictionary<string, IManualHandler>();

            var list = ReflectionManager.Instance.GetTypeByBase(typeof (IManualHandler));
            for (int i = 0; i < list.Count; ++i)
            {
                var handler = Activator.CreateInstance(list[i]) as IManualHandler;
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
