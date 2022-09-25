using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace UdemyRabbitMQ.subscriber // consumer
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

            // subscriber down olursa bağlandığı kuyruk silinmesin isteniyorsa aşağıdaki kodlar gereklidir
            // Avantajı: subscriber tekrar bağlandığı zaman aradaki down olduğu süre boyunca gelen mesajlar da
            // eski kuyruğu kalmaya devam ettiği için oraya geleceğinden o mesajları tekrar okuyabilir
            //var randomQueueName = "log-database-save-queue";
            //channel.QueueDeclare(randomQueueName, true, false, false);
            //channel.QueueBind(randomQueueName, "logs-fanout", "", null);

            // bağlanınca random queue ismi verilsin down olunca kuyruk da silinsin isteniyorsa aşağıdaki kodlar gereklidir
            var randomQueueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(randomQueueName, "logs-fanout", "", null);


            channel.BasicQos(0, 1, false); // mesajlar kuyruktan kaçar kaçar gelecek? videodan parametreleri dinle
            var consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(randomQueueName, false, consumer);
            // ikinci parametre true verilirse rabbitmq mesaj yanlış da alınsa doğru da alınsa kuyruktan siler
            // false yaparsak ben sana mesajı doğru gönderdiğini haber edeceğim o zaman sil diyoruz

            Console.WriteLine("Loglar Dinleniyor...");
            consumer.Received += (object sender, BasicDeliverEventArgs e) =>
            {
                var message = Encoding.UTF8.GetString(e.Body.ToArray());
                Thread.Sleep(500);
                Console.WriteLine("Kuyruktan Gelen Mesaj: " + message);

                channel.BasicAck(e.DeliveryTag, false); // mesajı artık kuyruktan silebilirsin diye belirttik (yukarıyı false yaptığımız için)
            };

            Console.ReadLine();
        }

    }
}
