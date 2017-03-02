using System;
using System.IO;
using System.Resources;
using CommandLine;
using WebServer;
using WebServerCommon;
using WebServerCommon.Properties;

namespace LocalWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new Options();

            if (Parser.Default.ParseArguments(args, options))
            {
                var root = options.Root;
                if (!Directory.Exists(root))
                {
                    Logger.Log(
                        Resources.InvalidPathMessage);
                }
                else
                {
                    var port = options.Port;
                    var isVerbose = options.Verbose;
                    var webServer = new WebServer.WebServer(root, port, isVerbose);
                    webServer.Run();
                }

            }
        }
    }
}
