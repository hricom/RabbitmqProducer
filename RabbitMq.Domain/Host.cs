

namespace RabbitMq.Domain
{
    using System;
    using System.Text;
    using RabbitMQ.Client;
    public class Host
    {
        public string PutMessage()
        {
            #region Variables            
            string messageIn = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            string RoutingKey = "RoutingKey";            
            string queueName = "queue";
            #endregion

            var factory = new ConnectionFactory()
            {                
                // HostName = "l296dtv03",
                Port = 5671,
                // Port = Protocols.DefaultProtocol.DefaultPort,
                UserName = "qa",
                Password = "mmmmmmmmmmmmmmmmmmmmmmmm",
                VirtualHost = "qa",
                ContinuationTimeout = new TimeSpan(10, 0, 0, 0)


            };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    string message = messageIn;
  ;

                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: RoutingKey, basicProperties: null, body: body);

                    Console.WriteLine("[x] Sent {0}", message);
                }
            }
            return "OK";
        }
    }
}
