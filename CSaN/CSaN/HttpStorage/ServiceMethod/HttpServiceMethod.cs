using System;
using System.IO;
using System.Net;

namespace HttpStorage.ServiceMethod
{
    class HttpServiceMethod
    {
        private string _method;

        public string Method => _method;

        public HttpServiceMethod(string method)
        {
            _method = method;
        }

        public virtual void Handle(HttpListenerRequest request, HttpListenerResponse response)
        {
            if (request.HttpMethod.CompareTo(Method) != 0)
            {
                throw new FormatException("Received request does not " + Method + " method.");
            }
        }
    }
}