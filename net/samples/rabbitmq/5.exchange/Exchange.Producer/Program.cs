using rabbitmq.common;

using RabbitMQ.Client;

using System.Text;

namespace Exchange.Producer
{
    public class Program
    {
        static void Main(string[] args)
        {

            //FanoutExchang();// 演示Fanout交换机，发布订阅模式 生产者
            // DirectExchange();// 演示Direct交换机，生产者
            TopicExchange();  // 演示Topic交换机，生产者
            Console.ReadKey();
        }

        public static void FanoutExchang()
        {
            using var channel = RabbitmqUntils.GetChannel();
            channel.ExchangeDeclare(exchange: RabbitmqUntils.FanoutExchangeName, type: "fanout", durable: false, autoDelete: false, null);// 创建交换机
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
                    channel.BasicPublish(exchange: RabbitmqUntils.FanoutExchangeName, "", false, null, body); //开始传递
                    Console.WriteLine("已发送： {0}", message);
                    Console.WriteLine("请输入要发送的消息：");
                    message = Console.ReadLine();
                }
            }
        }

        public static void DirectExchange()
        {
            using var channel = RabbitmqUntils.GetChannel();
            channel.ExchangeDeclare(exchange: RabbitmqUntils.DirectExchangeName, type: "direct", durable: false, autoDelete: false, null);// 创建交换机
            
            // 多个bindingkey
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add(RabbitmqUntils.DirectRoutingkeyOrange, "routingkey orange绑定Q1队列");
            dic.Add(RabbitmqUntils.DirectRoutingkeyBlack, "routingkey black绑定Q2队列");
            dic.Add(RabbitmqUntils.DirectRoutingkeyGreen, "routingkey green绑定Q2队列");

            foreach (var item in dic)
            {
                string message = item.Value;
                var body = Encoding.UTF8.GetBytes(message);
                /*
                * 发送一个消息
                * 1.发送到那个交换机
                * 2.路由的 key 是哪个
                * 3.其他的参数信息
                * 4.发送消息的消息体
                */
                channel.BasicPublish(exchange: RabbitmqUntils.DirectExchangeName, item.Key, false, null, body); //开始传递
                Console.WriteLine("已发送： {0}", message);
            }
           
        }

        public static void TopicExchange()
        {
            using var channel = RabbitmqUntils.GetChannel();
            channel.ExchangeDeclare(exchange: RabbitmqUntils.TopicExchangeName, type: "topic", durable: false, autoDelete: false, null);// 创建交换机
   
            // 多个bindingkey
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("quick.orange.rabbit", "被队列 Q1Q2 接收到");
            dic.Add("lazy.orange.elephant", "被队列 Q1Q2 接收到");
            dic.Add("quick.orange.fox", "被队列 Q1 接收到");
            dic.Add("lazy.brown.fox", "被队列 Q2 接收到");
            dic.Add("lazy.pink.rabbit", "虽然满足两个绑定但只被队列 Q2 接收一次");
            dic.Add("quick.brown.fox", "不匹配任何绑定不会被任何队列接收到会被丢弃");
            dic.Add("quick.orange.male.rabbit", "是四个单词不匹配任何绑定会被丢弃");
            dic.Add("lazy.orange.male.rabbit", "是四个单词但匹配 Q2");

            foreach (var item in dic)
            {
                string message = item.Value;
                var body = Encoding.UTF8.GetBytes(message);
                /*
                * 发送一个消息
                * 1.发送到那个交换机
                * 2.路由的 key 是哪个
                * 3.其他的参数信息
                * 4.发送消息的消息体
                */
                channel.BasicPublish(exchange: RabbitmqUntils.TopicExchangeName, item.Key, false, null, body); //开始传递
                Console.WriteLine("已发送： {0}", message);
            }
        }
    }
}