using Microsoft.Owin.Hosting;
using System;
using System.Configuration;

namespace Reservations.API.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var baseAddress = ConfigurationManager.AppSettings["baseAddress"] ?? "http://localhost:8181";

            using (var webApp = WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine($"{typeof(Program).Namespace} listening on {baseAddress}");
                Console.Read();
            }
        }
    }
}
