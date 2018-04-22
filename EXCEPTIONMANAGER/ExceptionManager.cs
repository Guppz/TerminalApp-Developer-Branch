using DATAACCESS.CRUD;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;
using System.IO;

namespace Exceptions
{
    public class ExceptionManager
    {
        public string PATH = @"C:\_temp\logs\";

        private static ExceptionManager instance;

        private static Dictionary<int, AppMessage> messages = new Dictionary<int, AppMessage>();

        private ExceptionManager()
        {
            LoadMessages();
        }

        public static ExceptionManager GetInstance()
        {
            if (instance == null)
                instance = new ExceptionManager();

            return instance;
        }

        public void Process(Exception ex)
        {

            var bex = new BusinessException();

            if (ex.GetType() == typeof(BusinessException))
            {
                bex = (BusinessException)ex;
            }
            else
            {
                bex = new BusinessException(0, ex);
            }

            ProcessBussinesException(bex);
        }

        private void ProcessBussinesException(BusinessException bex)
        {
            var today = DateTime.Now.ToString("YYYYMMdd");
            var logName = PATH + today + "_" + "log.txt";

            var message = bex.Message + "\n" + bex.StackTrace + "\n";

            if (bex.InnerException != null)
                message += bex.InnerException.Message + "\n" + bex.InnerException.StackTrace;
            FileInfo file = new FileInfo(PATH);
            file.Directory.Create(); // If the directory already exists, this method does nothing.
            

            using (StreamWriter w = File.AppendText(logName))
            {
                Log(bex.Message, w);
            }

            bex.AppMessage = GetMessage(bex);

            throw bex;
        }

        public AppMessage GetMessage(BusinessException bex)
        {
            var appMessage = new AppMessage
            {
            };

            if (messages.ContainsKey(bex.ExceptionId))
                appMessage = messages[bex.ExceptionId];

            return appMessage;
        }

        private void LoadMessages()
        {
            var crudMessages = new ExceptionMessageCrud();

            var lstMessages = crudMessages.RetrieveAll<AppMessage>();

            foreach (var appMessage in lstMessages)
            {
                messages.Add(appMessage.Id, appMessage);
            }
        }

        private void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

    }
}
