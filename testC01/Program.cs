using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using System;
using testC01.FileServices;
using testC01.FileServices.Implements;

namespace testC01
{
    class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IMain, Main>();
                    services.AddSingleton<IFileHandle, FileHandle>();
                    services.AddSingleton<IUtilHandle, UtilHandle>();
                    services.AddSingleton<ILogError, LogError>();
                    services.AddLogging(logging =>
                    {
                        // xoa logger
                        logging.ClearProviders();
                        //set level nho nhat cho log
                        logging.SetMinimumLevel(LogLevel.Trace);
                        //add NLog từ appsetting.json 
                        logging.AddNLog(context.Configuration);
                    });
                    //services.AddLogging(logging =>
                    //{
                    //    logging.ClearProviders();
                    //    logging.SetMinimumLevel(LogLevel.Error);
                    //    logging.AddNLog(context.Configuration);
                    //});        
                    NLog.LogManager.Configuration = new NLogLoggingConfiguration(context.Configuration.GetSection("NLog"));
                })
                .Build();
            var main = ActivatorUtilities.CreateInstance<Main>(host.Services);
            main.Run();
        }
    }
}
