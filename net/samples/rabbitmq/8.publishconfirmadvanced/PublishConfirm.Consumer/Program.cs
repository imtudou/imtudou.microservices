using rabbitmq.common;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

namespace PublishConfirm.Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args[0] == "client1")
            {
                ConfirmConsumer();
            } else if (args[0] == "client2")
            {
                BackupConsumer();
            }
            else if (args[0] == "client3")
            {
                WarningConsumer();
            }
            Console.ReadKey();
        }


        public static void ConfirmConsumer()
        {
            Console.WriteLine("ConfirmConsumer开始接受消息：");
            var channel = RabbitmqUntils.GetConfirmAdvancedQueue();
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(RabbitmqUntils.Confirm_Queue, false, consumer);
            consumer.Received += ((sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"ConfirmConsumer{DateTime.Now} 接收消息：{message}");
                channel.BasicAck(e.DeliveryTag, false);
            });
        }

        public static void BackupConsumer()
        {
            Console.WriteLine("BackupConsumer开始接受消息：");
            var channel = RabbitmqUntils.GetConfirmAdvancedQueue();
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(RabbitmqUntils.Back_Queue, false, consumer);
            consumer.Received += ((sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"BackupConsumer{DateTime.Now} 接收消息：{message}");
                channel.BasicAck(e.DeliveryTag, false);
            });
        }
        public static void WarningConsumer()
        {
            Console.WriteLine("WarningConsumer开始接受消息：");
            var channel = RabbitmqUntils.GetConfirmAdvancedQueue();
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(RabbitmqUntils.Warning_Queue, false, consumer);
            consumer.Received += ((sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"WarningConsumer{DateTime.Now} 接收消息：{message}");
                channel.BasicAck(e.DeliveryTag, false);
            });
        }




    }
}