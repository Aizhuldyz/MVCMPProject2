
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using WebServerCommon;
using WebServerCommon.Properties;
using MimeTypes;

namespace WebServer
{
    public class WebServer
    {
        private readonly HttpListener _listener;
        private readonly string _root;
        private readonly int _port;
        private readonly bool _isVerbose;

        public WebServer(string root, int port, bool isVerbose)
        {
            _listener = new HttpListener();
            _root = root;
            _port = port;
            _isVerbose = isVerbose;
        }

        public void Run()
        {
            _listener.Prefixes.Add("http://*:" + _port.ToString() + "/");
            _listener.Start();

            while (true)
            {
                try
                {
                    var listenerContext = _listener.GetContext();
                    Process(listenerContext);
                }
                catch
                {
                    //do nothing
                }
            }
        }

        private void Process(HttpListenerContext listenerContext)
        {
            string fileName = listenerContext.Request.Url.AbsolutePath;
            if(_isVerbose)
                Logger.Log(string.Format(Resources.RenderingFileName, fileName));
            Debugger.Launch();
            fileName = fileName.Substring(1);

            if (string.IsNullOrEmpty(fileName))
            {

                foreach (var defaultFile in Constants.IndexFiles)
                {
                    if (File.Exists(Path.Combine(_root, defaultFile)))
                    {
                        fileName = defaultFile;
                        break;
                    }
                }
            }

            fileName = Path.Combine(_root, fileName);
            if (File.Exists(fileName))
            {
                try
                {
                    using (Stream input = new FileStream(fileName, FileMode.Open))
                    {
                        listenerContext.Response.ContentType = MimeTypeMap.GetMimeType(Path.GetExtension(fileName));

                        byte[] buffer = new byte[1024];
                        int bytesReadFromFile;
                        while ((bytesReadFromFile = input.Read(buffer, 0, buffer.Length)) > 0)
                            listenerContext.Response.OutputStream.Write(buffer, 0, bytesReadFromFile);
                        input.Close();

                        listenerContext.Response.StatusCode = (int) HttpStatusCode.OK;
                        listenerContext.Response.OutputStream.Flush();
                    }
                }
                catch (Exception)
                {
                    listenerContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                }
            }
            else
            {
                listenerContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            listenerContext.Response.OutputStream.Close();
        }

        public void Stop()
        {
        }
    }
}
