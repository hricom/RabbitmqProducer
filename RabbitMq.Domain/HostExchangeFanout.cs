using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMq.Domain
{
    public class HostExchangeFanout
    {
        public string PutMessage()
        {
            #region Variables
            string ContentType = "text/plain";
            string ExchangeName = "Exchange";           
            string messageIn = "xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx";           
            #endregion

            try
            {
                // Create a ConnectionFactory and set the Uri to the CloudAMQP url
                ConnectionFactory factory = new ConnectionFactory();
                factory.ContinuationTimeout = new TimeSpan(10, 0, 0, 0);
                factory.Uri = new Uri("amqps://dev:mmmmmmmmmmmmmmm@url:5671/dev");

                // Create a connection and open a channel, dispose them when done
                using (IConnection connection = factory.CreateConnection())
                {
                    Console.WriteLine(string.Concat("Connection open: ", connection.IsOpen));
                    using (IModel channel = connection.CreateModel())
                    {
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
                            Console.WriteLine("Inicia el envio del mensaje");
                            channel.BasicPublish(exchange: ExchangeName, routingKey: "", basicProperties: properties, body: body);
                            Console.WriteLine("Termina el envio del mensaje");
                            // End. Sent message
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
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
