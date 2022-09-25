using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace UdemyRabbitMQ.publisher
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var uri = Environment.GetEnvironmentVariable("URI", EnvironmentVariableTarget.Process);
            var factory = new ConnectionFactory();
            factory.Uri = new Uri(uri);

            using var connection = factory.CreateConnection();

            var channel = connection.CreateModel();

            channel.ExchangeDeclare("logs-fanout", durable:true, type:ExchangeType.Fanout);

            Enumerable.Range(1, 50).ToList().ForEach(x =>
            {
                string message = $"Log: {x}";
                var messageBody = Encoding.UTF8.GetBytes(message); // kuyruğa bayt olarak gönderilir

                channel.BasicPublish("logs-fanout", "", null, messageBody);

                Console.WriteLine($"Mesaj Gönderildi: {x}");
            });
  
            Console.ReadLine();
        }
    }
}
