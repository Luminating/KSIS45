using System;
using System.IO;
using System.Net;
using System.Text;

namespace HttpStorage.ServiceMethod
{
    class HttpMethodHead : HttpServiceMethod
    {
        public HttpMethodHead() : base("HEAD")
        {

        }

        public override void Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            base.Handle(request, response);

            string relativepath = request.RawUrl.Trim('/');
            if (File.Exists(relativepath)) 
            {
                FileInfo info = new FileInfo(relativepath);

                response.ContentLength64 = info.Length;
                response.Headers.Add("Content-Loaction", info.Name);
                response.Headers.Add("Content-Type", info.Extension);
                response.Headers.Add("Date", info.LastWriteTimeUtc.ToString());
            } 
            else
            {
                response.StatusCode = 404;
                response.ContentLength64 = 0;
            }
            response.OutputStream.Close();
        }
    }
}