using RabbitMQ.Client;

using System.Reflection;
using System.Text;
using System.Threading.Channels;

namespace _01.Rabbitmq.Producer
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

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();
            /*
                *生成一个队列
                *1.队列名称
                *2.队列里面的消息是否持久化默认消息存储在内存中
                *3.该队列是否只供一个消费者进行消费是否进行共享true可以多个消费者消费
                *4.是否自动删除最后一个消费者端开连接以后该队列是否自动删除true自动删除*
                *5.其他参数
             */

            // 设置队列的最大优先级最大可设置0~255 官网推荐设置0~9 如果设置太高会比较占用cup和内存
            var arguments = new Dictionary<string, object>();
            arguments.Add("x-max-priority", 9);
            channel.QueueDeclare("hello", true, false, false, arguments);//创建一个名称为hello的消息队列
            //channel.QueueDeclare("hello", false, false, false, null);//创建一个名称为hello的消息队列
            for (int i = 0; i < 20; i++)
            {
                string message = i + "Hello World"; //控制台传递的消息内容
                var body = Encoding.UTF8.GetBytes(message);

                /*
                * 发送一个消息
                * 1.发送到那个交换机
                * 2.路由的 key 是哪个
                * 3.其他的参数信息
                * 4.发送消息的消息体
                */

                if (i == 5)
                {
                    var basicProperties = channel.CreateBasicProperties();
                    basicProperties.Priority = 5;
                    channel.BasicPublish(exchange: "", "hello", basicProperties, body); //开始传递
                }
                else
                {
                    channel.BasicPublish(exchange:"", "hello", null, body); //开始传递
                }

                Console.WriteLine("已发送： {0}", message);
            }     
            Console.ReadKey();
        }
    }

}