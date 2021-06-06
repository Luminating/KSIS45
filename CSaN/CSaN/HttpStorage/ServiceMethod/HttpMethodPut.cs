using System;
using System.IO;
using System.Net;

namespace HttpStorage.ServiceMethod
{
    class HttpMethodPut : HttpServiceMethod
    {
        public HttpMethodPut() : base("PUT")
        {

        }

        public override void Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            base.Handle(request, response);

            string relativeToPath = request.RawUrl.Trim('/');
            string relativeFromPath = request.Headers.Get("X-Copy-From")?.Trim('/');

            if (relativeFromPath == null)
            {
                WriteFile(relativeToPath, request, response);
            } 
            else
            {
                CopyFile(relativeFromPath, relativeToPath, response);
            }


            response.OutputStream.Close();
        }

        private void CopyFile(string fromPath, string toPath, HttpListenerResponse response)
        {
            try
            {
                int bufferLength = 0;
                byte[] buffer = new byte[20 * 1024 * 1024];

                using (FileStream fs = new FileStream(fromPath, FileMode.Open))
                {
                    bufferLength = fs.Read(buffer, 0, buffer.Length);
                }

                try
                {
                    FileInfo info = new FileInfo(toPath);
                    if (!info.Exists)
                    {
                        Directory.CreateDirectory(info.DirectoryName);
                    }

                    using (FileStream fs = new FileStream(toPath, FileMode.Create))
                    {
                        fs.Write(buffer, 0, bufferLength);
                    }
                }
                catch (Exception)
                {
                    response.StatusCode = 501;
                }
            }
            catch (Exception)
            {
                response.StatusCode = 404;
            }
        }

        private void WriteFile(string path, HttpListenerRequest request, HttpListenerResponse response)
        {
            Stream input = request.InputStream;
            byte[] content = new byte[request.ContentLength64];

            try
            {
                FileInfo info = new FileInfo(path);
                if (!info.Exists)
                {
                    Directory.CreateDirectory(info.DirectoryName);
                }

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    input.Read(content, 0, content.Length);
                    fs.Write(content, 0, content.Length);
                }
            }
            catch (Exception)
            {
                response.StatusCode = 501;
            }
        }
    }
}