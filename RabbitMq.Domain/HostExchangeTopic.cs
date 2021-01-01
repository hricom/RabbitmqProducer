using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMq.Domain
{
    public class HostExchangeTopic
    {
        public string PutMessage()
        {
            #region Variables
            string ContentType = "text/plain";
            string ExchangeName = "Exchange";
            string RoutingKey = "RoutingKey";
            string messageIn = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";
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
                        Console.WriteLine(string.Concat("channel open: ", channel.IsOpen));
                        try
                        {
                            // The message we want to put on the queue
                            string message = messageIn;
                            // the data put on the queue must be a byte array
                            var body = Encoding.UTF8.GetBytes(message);

                            // Begin. Sent message
                            IBasicProperties properties = channel.CreateBasicProperties();
                            properties.Persistent = true;
                            properties.ContentType = ContentType;
                            PublicationAddress address = new PublicationAddress(exchangeType: ExchangeType.Topic, exchangeName: ExchangeName, routingKey: RoutingKey);
                            Console.WriteLine("Inicia el envio del mensaje");
                            channel.BasicPublish(addr: address, basicProperties: properties, body: body);
                            Console.WriteLine("Termina el envio del mensaje");
                            // End. Sent message
                        }
                        catch (Exception EX)
                        {
                            Console.WriteLine(EX.Message);
                        }


                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return string.Empty;
        }
    }
}
