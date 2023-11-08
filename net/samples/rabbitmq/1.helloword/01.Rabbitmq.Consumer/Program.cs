
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

namespace _01.Rabbitmq.Consumer
{
    public class Program
    {
        private static string queueName = "hello";

        static void Main(string[] args)
        {

            // 创建一个连接工厂
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };


            // publisher／consumer和broker之间的TCP连接
            using var connection = factory.CreateConnection();
            // Channel作为轻量级的Connection极大减少了操作系统建立TCPconnection的开销
            using var channel = connection.CreateModel();

            var arguments = new Dictionary<string, object>();
            arguments.Add("x-max-priority",9);
            arguments.Add("x-queue-mode", "lazy");
            // 创建队列/交换机（如队列/交换机已存在的情况可不用再次创建/此创建为：确保先开启消费者，生产者未创建队列/交换机而引发报错）
            channel.QueueDeclare("hello", true, false, false, arguments);

            //channel.QueueDeclare("hello", false, false, false, null);
            // 事件对象
            var consumer = new EventingBasicConsumer(channel);

            /*
            * 消费者消费消息
            * 1.消费哪个队列
            * 2.消费成功之后是否要自动应答 true 代表自动应答 false 手动应答
            * 3.消费者未成功消费的回调
            */
            channel.BasicConsume("hello", true, consumer);

            // 接收消息回调
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("已接收： {0}", message);

            };
            Console.ReadKey();
        }
    }

}