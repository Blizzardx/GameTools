using System.Collections.Generic;
using System.IO;
using Common.Tool;

namespace ExcelImproter.Framework.Handler
{
    class AutoHandlerManager : Singleton<AutoHandlerManager>
    {
        private AutoHandler m_Handler;

        public AutoHandlerManager()
        {
            m_Handler = new AutoHandler();
        }
        public void Handler(string path)
        {
            m_Handler.Clear();
            m_Handler.Handler(path);
        }
    }
}
