/// <summary>
/// 
/// 
/// Prueba de utilizacion de RabbitMQ
/// </summary>

namespace ConsoleAppSent
{
    using System;
    using RabbitMQ.Client;
    using System.Text;
    using Newtonsoft.Json;
    using RabbitMq.Domain;

    class Program
    {
        static void Main(string[] args)
        {

            //Host testHost = new Host();
            //var data = testHost.PutMessage();

            //HostUrl testHostUri = new HostUrl();
            //testHostUri.PutMessage();

            var data = new HostExchangeTopic().PutMessage();

            // var datafanout = new HostExchangeFanout().PutMessage();


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
