/// <summary>
/// Referencias.
// https://www.cloudamqp.com/docs/dotnet.html 
// https://github.com/cloudamqp/DotNetAmqpExample
/// </summary>
namespace RabbitMq.Domain
{
    using RabbitMQ.Client;
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class HostUrlDirect
    {
        public string PutMessage()
        {
            #region Variables
            string ContentType = "text/plain";
            string ExchangeName = "Exchange";
            string RoutingKey = "RoutingKey";
            string messageIn = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
            string queueName = "queue";
            #endregion

            try
            {
                // Create a ConnectionFactory and set the Uri to the CloudAMQP url
                ConnectionFactory factory = new ConnectionFactory();
                factory.ContinuationTimeout = new TimeSpan(10, 0, 0, 0);
                factory.Uri = new Uri("amqps://dev:nnnnnnnnnnn@test.rmq.cloudamqp.com:5671/dev");

                // Create a connection and open a channel, dispose them when done
                using (IConnection connection = factory.CreateConnection())
                {
                    Console.WriteLine(string.Concat("Connection open: ", connection.IsOpen));
                    using (var channel = connection.CreateModel())
                    {
                        try
                        {
                            // The message we want to put on the queue
                            string message = messageIn;
                            // the data put on the queue must be a byte array
                            var body = Encoding.UTF8.GetBytes(message);

                            // Begin.Create queue
                            channel.ExchangeDeclare(exchange: ExchangeName, type: ExchangeType.Direct, durable: true, autoDelete: false, arguments: null);
                            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                            channel.QueueBind(queue: queueName, exchange: ExchangeName, routingKey: RoutingKey, arguments: null);
                            channel.BasicPublish(exchange: ExchangeName, routingKey: RoutingKey, basicProperties: null, body: body);
                            // End.Create queue

                            // Begin. Sent message
                            IBasicProperties properties = channel.CreateBasicProperties();
                            properties.Persistent = true;
                            properties.ContentType = ContentType;
                            PublicationAddress address = new PublicationAddress(exchangeType: ExchangeType.Direct, exchangeName: ExchangeName, routingKey: RoutingKey);
                            Console.WriteLine("Inicia el envio del mensaje");
                            channel.BasicPublish(addr: address, basicProperties: properties, body: body);
                            Console.WriteLine("Termina el envio del mensaje");
                            // End. Sent message
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            throw e;
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }


            return string.Empty;
        }
    }
}
