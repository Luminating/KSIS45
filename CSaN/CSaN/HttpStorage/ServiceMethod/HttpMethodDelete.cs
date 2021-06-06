using System;
using System.IO;
using System.Net;
using System.Text;

namespace HttpStorage.ServiceMethod
{
    class HttpMethodDelete : HttpServiceMethod
    {
        public HttpMethodDelete() : base("DELETE")
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
                    Directory.Delete(relativePath, true);
                }
                else
                {
                    File.Delete(relativePath);
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
    }
}