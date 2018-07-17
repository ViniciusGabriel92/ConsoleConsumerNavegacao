using System;
using RabbitMQ.Client;
using Newtonsoft.Json;
using RabbitMQ.Client.Events;
using System.Text;


namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            var objDeserialized = new object();
            Console.WriteLine("Consumer!");

            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "filaNavegacao",
                                        durable: false,
                                        exclusive: false,
                                        autoDelete: false,
                                        arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var corpo = ea.Body;
                    var mensagem = Encoding.UTF8.GetString(corpo);
                    objDeserialized = JsonConvert.DeserializeObject<InformationNavigation>(mensagem);
                    Console.WriteLine("Creating user {0} ", MetodosGenericos.BuscarParametros(objDeserialized));
                    MetodosGenericos.InsertInformationNavigation((InformationNavigation)objDeserialized);

                };

                channel.BasicConsume(queue: "filaNavegacao",
                                 autoAck: true,
                                 consumer: consumer);

                Console.WriteLine("Done.");
                Console.ReadLine();
            }

        }

    }
}
