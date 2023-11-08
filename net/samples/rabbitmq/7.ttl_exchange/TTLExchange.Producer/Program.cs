using rabbitmq.common;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;
using System.Threading.Channels;

namespace TTLExchange.Producer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("请输入要发送的消息：");
            string message = System.Console.ReadLine();

            if (string.IsNullOrEmpty(message))
            {
                while (true)
                {
                    Console.WriteLine("请输入要发送的消息： {0}", message);
                }
            }
            else
            {
                using var channel = RabbitmqUntils.GetTTLQueue();
                while (true)
                {
                    SendMsg(message, channel);


                    Console.WriteLine("请输入要发送的消息：");
                    message = Console.ReadLine();
                }
            }
            Console.ReadKey();

        }


        private static void SendExpirationMsg(string message, IModel channel)
        {
            var msg = $"消息来自ttl 为 10s QA队列{message}";
            var body = Encoding.UTF8.GetBytes(msg);
            channel.BasicPublish(RabbitmqUntils.X_Exchange, "XC", false, null, body);

            Console.WriteLine($"{DateTime.Now} 已发送：{msg}");
        }


        private static void SendMsg(string message, IModel channel)
        {
            var qaMsg = $"消息来自ttl 为 10s QA队列{message}";
            var qbMsg = $"消息来自ttl 为 40s QB队列{message}";
            var qaBody = Encoding.UTF8.GetBytes(qaMsg);
            var qbBody = Encoding.UTF8.GetBytes(qbMsg);
            channel.BasicPublish(RabbitmqUntils.X_Exchange, "XA", false, null, qaBody);
            channel.BasicPublish(RabbitmqUntils.X_Exchange, "XB", false, null, qbBody);

            Console.WriteLine($"{DateTime.Now} 已发送：{qaMsg}");
            Console.WriteLine($"{DateTime.Now} 已发送：{qbMsg}");
        }

        private static void SendDelayedMsg()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory() { UserName = "guest", Password = "guest", HostName = "127.0.0.1" };
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            Dictionary<string, object> exchangeArgs = new Dictionary<string, object>(){
                {"x-delayed-type","direct" }
            }; //指定x-delayed-message 类型的交换机，并且添加x-delayed-type属性
            channel.ExchangeDeclare("plug.delay.exchange", "x-delayed-message", true, false, exchangeArgs);
            channel.QueueDeclare("plug.delay.queue", true, false, false, null);
            channel.QueueBind("plug.delay.queue", "plug.delay.exchange", "plugdelay");
            var properties = channel.CreateBasicProperties();
            Console.WriteLine("生产者开始发送消息");
            Dictionary<string, object> headers = new Dictionary<string, object>() { { "x-delay", "5000" } };
            properties.Persistent = true;
            properties.Headers = headers;
            while (true)
            {
                string message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("plug.delay.exchange", "plugdelay", properties, body);
            }
        }
    }
}