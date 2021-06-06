using System;
using System.IO;
using System.Net;

using HttpStorage.ServiceMethod;

namespace HttpStorage
{
    class HttpStorageServerService
    {
        HttpServiceMethod[] methods =
        {
            new HttpMethodPut(),
            new HttpMethodGet(),
            new HttpMethodHead(),
            new HttpMethodDelete(),
        };

        public HttpStorageServerService()
        {

        }

        public void Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            foreach (var method in methods)
            {
                if (method.Method.CompareTo(request.HttpMethod) == 0)
                {
                    method.Handle(request, response);
                    break;
                }
            }
        }
    }
}