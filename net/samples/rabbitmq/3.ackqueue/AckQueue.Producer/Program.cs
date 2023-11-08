using rabbitmq.common;
using RabbitMQ.Client;
using System.Text;
namespace AckQueue.Producer
{
    public class Program
    {
        static void Main(string[] args)
        {
            using var channel = RabbitmqUntils.GetChannel();
            /*
                *生成一个队列
                *1.队列名称
                *2.队列里面的消息是否持久化默认消息存储在内存中
                *3.该队列是否只供一个消费者进行消费是否进行共享true可以多个消费者消费
                *4.是否自动删除最后一个消费者端开连接以后该队列是否自动删除true自动删除*
                *5.其他参数
             */
            channel.QueueDeclare(queue:RabbitmqUntils.AckQueueName, durable:true, exclusive:false, autoDelete:false, null);//创建一个消息队列
            Console.WriteLine("请输入要发送的消息：");
            string message = Console.ReadLine();
            if (string.IsNullOrEmpty(message))
            {
                while (true)
                {
                    Console.WriteLine("请输入要发送的消息： {0}", message);
                }
            }
            else
            {

                while (true)
                {
                    var body = Encoding.UTF8.GetBytes(message);
                    /*
                    * 发送一个消息
                    * 1.发送到那个交换机
                    * 2.路由的 key 是哪个
                    * 3.其他的参数信息
                    * 4.发送消息的消息体
                    */

                    // 设置消息持久化(保存在磁盘上)
                    var props = channel.CreateBasicProperties();
                    props.Persistent = true;

                    channel.BasicPublish(exchange: "", RabbitmqUntils.AckQueueName, props, body); //开始传递
                    Console.WriteLine("已发送： {0}", message);
                    Console.WriteLine("请输入要发送的消息：");
                    message = Console.ReadLine();
                }
            }

            Console.ReadKey();
        }
    }
}