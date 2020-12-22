using log4net;
using log4net.Config;
using log4net.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace cy_chat.utils
{
    public class CommonUtil
    {
        public static ILoggerRepository repository { get; set; }
        public static void Init()
        {
            //指定配置文件
            repository = LogManager.CreateRepository("CyLogRepository");
            XmlConfigurator.Configure(repository, new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "Config\\Log4Net.config"));
        }
    }
}
