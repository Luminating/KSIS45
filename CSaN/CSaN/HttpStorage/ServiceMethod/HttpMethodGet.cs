using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.Json;

namespace HttpStorage.ServiceMethod
{
    class HttpMethodGet : HttpServiceMethod
    {
        public HttpMethodGet() : base("GET")
        {

        }

        public override void Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            base.Handle(request, response);

            string relativePath = request.RawUrl.Trim('/');
            try
            {
                FileAttributes attr = File.GetAttributes(relativePath);
                if (attr.HasFlag(FileAttributes.Directory))
                {
                    ShowDirectory(relativePath, response);
                }
                else
                {
                    ShowFile(relativePath, response);
                }
            }
            catch (UnauthorizedAccessException)
            {
                response.StatusCode = 403;
            }
            catch (Exception)
            {
                response.StatusCode = 404;
            }
            response.OutputStream.Close();
        }

        private void ShowDirectory(string dirname, HttpListenerResponse response)
        {
            string[] dir = Directory.GetFiles(dirname);
            string jsondir = JsonSerializer.Serialize(dir, typeof(string[]));

            byte[] buffer = Encoding.UTF8.GetBytes(jsondir);

            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        private void ShowFile(string filename, HttpListenerResponse response)
        {
            try
            {
                using (FileStream fs = new FileStream(filename, FileMode.Open))
                {
                    byte[] buffer = new byte[20 * 1024 * 1024];
                    int bufferLength = fs.Read(buffer, 0, buffer.Length);

                    response.ContentLength64 = bufferLength;
                    response.OutputStream.Write(buffer, 0, bufferLength);
                }
            }
            catch (Exception)
            {
                response.StatusCode = 501;
            }
        }
    }
}