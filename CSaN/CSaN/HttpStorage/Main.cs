using System;
using System.IO;
using System.Net;
using System.Text;

namespace HttpStorage
{
    public class MainClass
    {
        public static void Main(String[] args)
        {
            HttpStorageServer server = new HttpStorageServer("http://localhost:8888/");
            server.Start();
            //HttpListener listener = new HttpListener();

            //listener.Prefixes.Add("http://localhost:8888/connection/");
            //listener.Start();

            //Console.WriteLine("Ожидание подключения...");

            //HttpListenerContext context = listener.GetContext();
            //HttpListenerRequest request = context.Request;

            //Console.WriteLine(context.Request.HttpMethod);
            //Console.WriteLine(context.Request.Url);
            //Console.WriteLine(context.Request.RawUrl);

            //HttpListenerResponse response = context.Response;

            //string responseStr = "<html><head><meta charset='utf8'></head><body>Привет мир!</body></html>";
            //byte[] buffer = Encoding.UTF8.GetBytes(responseStr);

            //response.ContentLength64 = buffer.Length;

            //Stream output = response.OutputStream;
            //output.Write(buffer, 0, buffer.Length);

            //output.Close();

            //listener.Stop();
            //Console.WriteLine("Обработка подключений завершена.");
            //Console.ReadKey();
        }
    }
}