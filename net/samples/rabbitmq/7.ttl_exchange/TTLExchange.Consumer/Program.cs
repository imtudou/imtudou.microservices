using rabbitmq.common;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

namespace TTLExchange.Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine($"开始接受");
            using var channel = RabbitmqUntils.GetTTLQueue();
            //事件对象
            var consumer = new EventingBasicConsumer(channel);
            // autoAck：false 手动应答
            channel.BasicConsume(queue: RabbitmqUntils.QD_Queue, false, consumer);
            // 接收消息回调
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                System.Console.WriteLine($"{DateTime.Now} 接收消息：{message}");
                channel.BasicAck(e.DeliveryTag, false);
            };
            System.Console.ReadKey();

        }

        public static void ReciveDelayedMsg()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory { UserName = "guest", Password = "guest", HostName = "127.0.0.1" };
            //创建连接
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
            consumer.Received += (obj, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                Console.WriteLine(message); channel.BasicAck(ea.DeliveryTag, false);
            };
            channel.BasicConsume("plug.delay.queue", false, consumer);
            Console.ReadKey();
            channel.Dispose();
            connection.Close();

        }

    }
}