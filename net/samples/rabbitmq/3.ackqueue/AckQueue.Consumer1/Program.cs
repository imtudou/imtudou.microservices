using rabbitmq.common;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

namespace AckQueue.Consumer1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Client1：等待消费消息 休眠1s");
            using var channel = RabbitmqUntils.GetChannel();
            // 创建队列/交换机（如队列/交换机已存在的情况可不用再次创建/此创建为：确保先开启消费者，生产者未创建队列/交换机而引发报错）
            channel.QueueDeclare(RabbitmqUntils.AckQueueName, true, false, false, null);
            // 事件对象
            var consumer = new EventingBasicConsumer(channel);
            // 设置不公平分发
            channel.BasicQos(0, 5, false);
            /*
           * 消费者消费消息
           * 1.消费哪个队列
           * 2.消费成功之后是否要自动应答 true 代表自动应答 false 手动应答
           * 3.消费者未成功消费的回调
           */
            channel.BasicConsume(RabbitmqUntils.AckQueueName, false, consumer);
           

            // 接收消息回调
            consumer.Received += (sender, e) =>
            {
                Thread.Sleep(1000);

                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("接收消息： {0}", message);            
                // 手动应答
                channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            };

            Console.ReadKey();
        }
    }
}