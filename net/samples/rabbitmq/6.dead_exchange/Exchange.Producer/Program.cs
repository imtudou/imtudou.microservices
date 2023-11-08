using rabbitmq.common;

using RabbitMQ.Client;

using System.Text;

namespace Exchange.Producer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入要发送的消息：");
            var channel = RabbitmqUntils.GetChannel();
            channel.ExchangeDeclare(RabbitmqUntils.test_exchange, "direct", false, false, null);
            for (int i = 0; i < 10; i++)
            {
                string message = $"消息{i}";
                var body = Encoding.UTF8.GetBytes(message);
                /*
                * 发送一个消息
                * 1.发送到那个交换机
                * 2.路由的 key 是哪个
                * 3.其他的参数信息
                * 4.发送消息的消息体
                */

                // 设置 ttl
                //var props = channel.CreateBasicProperties();
                //props.Expiration = "1000";

                //channel.BasicPublish(exchange: RabbitmqUntils.test_exchange, RabbitmqUntils.test_routingkey, false, props, body); //开始传递
                channel.BasicPublish(exchange: RabbitmqUntils.test_exchange, RabbitmqUntils.test_routingkey, false, null, body); //开始传递
                Console.WriteLine("已发送： {0}", message);
            }

        }
    }
}