using rabbitmq.common;

using RabbitMQ.Client;

using System.Text;

namespace PublishConfirm.Producer
{
    public class Program
    {
        static void Main(string[] args)
        {
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
                var channel = RabbitmqUntils.GetConfirmAdvancedQueue();
                while (true)
                {
                    SendMsg(message, channel);
                    message = Console.ReadLine();
                    
                }
            }
            Console.ReadKey();
        }

        public static void SendMsg(string msg,IModel channel)
        {
            var body1 = Encoding.UTF8.GetBytes("路由正常_"+msg);
            var body2 = Encoding.UTF8.GetBytes("路由不正常_" + msg);
            channel.ConfirmSelect();// 开启发布确认
            channel.BasicPublish(RabbitmqUntils.Confirm_Exchange, RabbitmqUntils.Confirm_Routingkey, mandatory: true, null, body1);

            //演示Routingkey配置错误
            //如果发布了带有“mandatory” = true 标志集的消息，但无法传递，代理将其返回给发送客户端( channel.BasicReturn)。
            channel.BasicPublish(RabbitmqUntils.Confirm_Exchange, RabbitmqUntils.Confirm_Routingkey+"111", mandatory:true, null, body2);
            // 监听确认的消息
            channel.BasicAcks += (sender, e) =>
            {
                Console.WriteLine($"交换机已收到:{e.DeliveryTag}");
            };

            //监听回退的消息
            channel.BasicReturn += ((sender, e) =>
            {
                var body = e.Body.ToArray();
                var msg = System.Text.Encoding.UTF8.GetString(body);
                Console.WriteLine($"监听回退的消息{msg}；RoutingKey：{e.RoutingKey}；退回原因：{e.ReplyText}");
            });
        }


        
    }
}