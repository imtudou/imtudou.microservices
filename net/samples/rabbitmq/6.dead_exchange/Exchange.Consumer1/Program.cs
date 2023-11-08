using rabbitmq.common;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Text;

namespace Exchange.Consumer1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("C1 开始接消息");
            using var channel = RabbitmqUntils.GetChannel();

            /*
              申明一个 test_exchange 交换机
              配置转发到死信队列参数
              绑定交换机与对列
              
             */
            channel.ExchangeDeclare(RabbitmqUntils.test_exchange, "direct",false,false,null);
            var arguments = new Dictionary<string, object> { };
            arguments.Add("x-dead-letter-exchange", "dead_exchange");//正常队列设置死信交换机 参数 key 是固定值
            arguments.Add("x-dead-letter-routing-key", "dead");//正常队列设置死信 routing-key 参数 key 是固定值
            //arguments.Add("x-max-length", 6);//正常队列设置长度限制
            channel.QueueDeclare(RabbitmqUntils.test_queue, false,false,false, arguments);
            channel.QueueBind(RabbitmqUntils.test_queue, RabbitmqUntils.test_exchange, RabbitmqUntils.test_routingkey, null);

            /*
             申明一个死信交换机
             绑定交换机与对列
             */
            channel.ExchangeDeclare(RabbitmqUntils.dead_exchange, "direct", false, false, null);            
            channel.QueueDeclare("dead_queue", false, false, false, null);
            channel.QueueBind(RabbitmqUntils.dead_queue, RabbitmqUntils.dead_exchange, RabbitmqUntils.dead_routingkey, null);


            //事件对象
            var consumer = new EventingBasicConsumer(channel);
            // autoAck：false 手动应答
            channel.BasicConsume(queue: RabbitmqUntils.test_queue, false, consumer);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if (message.Equals("消息1"))
                {
                    Console.WriteLine("C1 接收消息： {0},消息被拒绝", message);
                    // 设置requeue：false 代表拒绝重新入队列，如果配置了死信交换机将发送到死信队列中
                    channel.BasicReject(e.DeliveryTag,false);

                }
                else
                {
                    Console.WriteLine("C1 接收消息： {0}", message);
                    channel.BasicAck(e.DeliveryTag, false);

                }

            };


            

            Console.ReadKey();
        }
    }
}