using System;
using System.IO;
using System.Net;

namespace HttpStorage
{
    class HttpStorageServer
    {
        private HttpListener _listener = new HttpListener();

        public HttpStorageServer(string url)
        {
            _listener.Prefixes.Add(url);
        }

        public void Start()
        {
            _listener.Start();

            HttpStorageServerService service = new HttpStorageServerService();

            while (true)
            {
                HttpListenerContext context = _listener.GetContext();
                service.Handle(context.Request, context.Response);
            }
        }

        public void Stop()
        {
            _listener.Stop();
        }
    }
}