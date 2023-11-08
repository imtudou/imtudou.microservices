using rabbitmq.common;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Collections.Concurrent;
using System.Text;
using System.Threading.Channels;

namespace Exchange.Consumer
{
    public class Program
    {
        static void Main(string[] args)
        {
            // FanoutExchang($"{args[0]}");// 演示Fanout交换机，发布订阅模式 消费者

            //DirectExchangeClient1(); // 演示Direct交换机，  
            //DirectExchangeClient2();// 演示Direct交换机，

            if (args[0] == "topic1")
            {
                TopicExchangeClient1(); // 演示topic
            }

            if (args[0] == "topic2")
            {
                TopicExchangeClient2(); // 演示topic
            }


            Console.ReadKey();
        }

        public static void FanoutExchang(string clientName)
        {
            Console.WriteLine($"{clientName}接收消息");
            using var channel = RabbitmqUntils.GetChannel();
            channel.ExchangeDeclare(exchange: RabbitmqUntils.FanoutExchangeName, type: "fanout", durable: false, autoDelete: false, null); // 创建交换机
            string queueName = channel.QueueDeclare(queue: "", durable: false, exclusive: false, autoDelete: true, null).QueueName; // 创建临时队列
            channel.QueueBind(queue: queueName, exchange: RabbitmqUntils.FanoutExchangeName, routingKey: "", null);

            //事件对象
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: queueName, false, consumer);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("接收消息： {0}", message);
            };
        }

        public static void DirectExchangeClient1()
        {
            Console.WriteLine("DirectExchangeClient1 开始接受消息：");
            using var channel = RabbitmqUntils.GetChannel();
            channel.ExchangeDeclare(exchange: RabbitmqUntils.DirectExchangeName, type: "direct", durable: false, autoDelete: false, null); // 创建交换机
            channel.QueueDeclare(queue: RabbitmqUntils.DirectQueueOneName, durable: false, exclusive: false, autoDelete: false, null);//申明Q1队列
            //routingkey orange绑定Q1队列
            channel.QueueBind(queue: RabbitmqUntils.DirectQueueOneName, exchange: RabbitmqUntils.DirectExchangeName, routingKey: RabbitmqUntils.DirectRoutingkeyOrange, null);
            
            //事件对象
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: RabbitmqUntils.DirectQueueOneName, false, consumer);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("DirectExchangeClient1 接收消息： {0}", message);
            };

        }

        public static void DirectExchangeClient2()
        {
            
            Console.WriteLine("DirectExchangeClient2 开始接受消息：");
            using var channel = RabbitmqUntils.GetChannel();
            channel.ExchangeDeclare(exchange: RabbitmqUntils.DirectExchangeName, type: "direct", durable: false, autoDelete: false, null); // 创建交换机
            channel.QueueDeclare(queue: RabbitmqUntils.DirectQueueTwoName, durable: false, exclusive: false, autoDelete: false, null);//申明Q2队列
            //routingkey black绑定Q2队列
            channel.QueueBind(queue: RabbitmqUntils.DirectQueueTwoName, exchange: RabbitmqUntils.DirectExchangeName, routingKey: RabbitmqUntils.DirectRoutingkeyBlack, null);
            //routingkey green绑定Q2队列
            channel.QueueBind(queue: RabbitmqUntils.DirectQueueTwoName, exchange: RabbitmqUntils.DirectExchangeName, routingKey: RabbitmqUntils.DirectRoutingkeyGreen, null);

            //事件对象
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: RabbitmqUntils.DirectQueueTwoName, false, consumer);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("DirectExchangeClient2 接收消息： {0}", message);
            };

        }

        public static void TopicExchangeClient1()
        {
            Console.WriteLine("TopicExchangeClient1 开始接受消息：");
            using var channel = RabbitmqUntils.GetChannel();
            channel.ExchangeDeclare(exchange: RabbitmqUntils.TopicExchangeName, type: "topic", durable: false, autoDelete: false, null); // 创建交换机
            channel.QueueDeclare(queue: "Q1", durable: false, exclusive: false, autoDelete: false, null);//申明Q1队列
            //routingkey orange绑定Q1队列
            channel.QueueBind(queue: "Q1", exchange: RabbitmqUntils.TopicExchangeName, routingKey: "*.orange.*", null);

            //事件对象
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "Q1", false, consumer);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("TopicExchangeClient1 接收消息： {0}", message);
            };
        }

        public static void TopicExchangeClient2()
        {
            Console.WriteLine("TopicExchangeClient2 开始接受消息：");
            using var channel = RabbitmqUntils.GetChannel();
            channel.ExchangeDeclare(exchange: RabbitmqUntils.TopicExchangeName, type: "topic", durable: false, autoDelete: false, null); // 创建交换机
            channel.QueueDeclare(queue: "Q2", durable: false, exclusive: false, autoDelete: false, null);//申明Q1队列
            //routingkey orange绑定Q1队列
            channel.QueueBind(queue: "Q2", exchange: RabbitmqUntils.TopicExchangeName, routingKey: "*.*.rabbit", null);
            channel.QueueBind(queue: "Q2", exchange: RabbitmqUntils.TopicExchangeName, routingKey: "lazy.#", null);

            //事件对象
            var consumer = new EventingBasicConsumer(channel);
            channel.BasicConsume(queue: "Q2", false, consumer);

            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("TopicExchangeClient2 接收消息： {0}", message);
            };
        }
    }
}