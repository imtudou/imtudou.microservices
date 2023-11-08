using rabbitmq.common;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

namespace Exchange.Consumer2
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C2 开始接消息");
            using var channel = RabbitmqUntils.GetChannel();

            /*
               申明一个死信交换机
               绑定交换机与对列
            */
            channel.ExchangeDeclare(RabbitmqUntils.dead_exchange, "direct", false, false, null);
            channel.QueueDeclare("dead_queue", false, false, false, null);
            channel.QueueBind(RabbitmqUntils.dead_queue, RabbitmqUntils.dead_exchange, RabbitmqUntils.dead_routingkey, null);

            //事件对象
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "dead_queue", true, consumer);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("C2 接收消息： {0}", message);
            };

            Console.ReadKey();
        }
    }
}