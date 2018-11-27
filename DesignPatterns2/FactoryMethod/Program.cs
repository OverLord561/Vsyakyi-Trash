using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryMethod
{
    class Program
    {
        //Визначити інтерфейс для створення об’єкта, але надати підкласам
        //вирішувати який клас інстанціювати.Фабричний метод відделеговує інстанцювання своїм підкласам
        static void Main(string[] args)
        {
            var providerType = GetTypeOfLoggingProviderFromConfigFile();
            ILogger logger = LoggerProviderFactory.GetLoggingProvider(providerType);
            logger.LogMessage("Hello Factory Method Design Pattern.");
            // Вивід: [Log4Net: Hello Factory Method Design Pattern]
            Console.ReadLine();
        }

        private static LoggingProviders GetTypeOfLoggingProviderFromConfigFile()
        {
            // Це такий собі хадркод, щоб не ускладнювати прикладу
            return LoggingProviders.Log4Net;
        }
    }

    interface ILogger
    {
        void LogMessage(string message);
        void LogError(string message);
        void LogVerboseInformation(string message);
    }
    class Log4NetLogger : ILogger
    {
        public void LogError(string message)
        {
            throw new NotImplementedException();
        }

        public void LogMessage(string message)
        {
            Console.WriteLine();
        }

        public void LogVerboseInformation(string message)
        {
            throw new NotImplementedException();
        }
    }

    class EnterpriseLogger : ILogger
    {
        public void LogError(string message)
        {
            throw new NotImplementedException();
        }

        public void LogMessage(string message)
        {
            throw new NotImplementedException();
        }

        public void LogVerboseInformation(string message)
        {
            throw new NotImplementedException();
        }
    }

    class LoggerProviderFactory
    {
        public static ILogger GetLoggingProvider(LoggingProviders logProviders)
        {
            switch (logProviders)
            {
                case LoggingProviders.Enterprise:
                    return new EnterpriseLogger();
                case LoggingProviders.Log4Net:
                    return new Log4NetLogger();
                default:
                    return new EnterpriseLogger();
            }
        }
    }

    public enum LoggingProviders
    {
        Enterprise,
        Log4Net
    }

   
}
