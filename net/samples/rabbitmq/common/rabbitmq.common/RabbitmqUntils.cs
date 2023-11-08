using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.Data.SqlTypes;
using System.Runtime.CompilerServices;
using System.Threading.Channels;

namespace rabbitmq.common
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class RabbitmqUntils
    {
        /// <summary>
        /// 对列名称
        /// </summary>
        public static string QueueName { get; set; } = "test_hello";
        public static string WorkQueueName { get; set; } = "test_WorkQueue";
        public static string AckQueueName { get; set; } = "test_AckQueueName";
        public static string FanoutExchangeName { get; set; } = "test_FanoutExchangeName";
        public static string DirectExchangeName { get; set; } = "test_DirectExchangeName";
        public static string DirectQueueOneName { get; set; } = "test_DirectQueueOneName";
        public static string DirectQueueTwoName { get; set; } = "test_DirectQueueTwoName";
        public static string DirectRoutingkeyOrange { get; set; } = "test_DirectRoutingkeyOrange";
        public static string DirectRoutingkeyBlack { get; set; } = "test_DirectRoutingkeyBlack";
        public static string DirectRoutingkeyGreen { get; set; } = "test_DirectRoutingkeyGreen";
        public static string TopicExchangeName { get; set; } = "test_TopicExchangeName";

        public static string test_exchange { get; set; } = "test_exchange";
        public static string test_queue { get; set; } = "test_queue";
        public static string test_routingkey { get; set; } = "test";
        public static string dead_exchange { get; set; } = "dead_exchange";
        public static string dead_queue { get; set; } = "dead_queue";
        public static string dead_routingkey { get; set; } = "dead";

        public static string X_Exchange { get; set; } = "X";
        public static string Y_Exchange { get; set; } = "Y";
        public static string QA_Queue { get; set; } = "QA";
        public static string QB_Queue { get; set; } = "QB";
        public static string QD_Queue { get; set; } = "QD";
        public static string QC_Queue { get; set; } = "QC";
        public static string Delayed_Queue { get; set; } = "delayed.queue";
        public static string Delayed_Exchange { get; set; } = "delayed.exchange";
        public static string Delayed_Routingkey { get; set; } = "delayed.routingkey";

        public static string Confirm_Exchange { get; set; } = "confirm.exchange";
        public static string Confirm_Queue { get; set; } = "confirm.queue";
        public static string Confirm_Routingkey { get; set; } = "confirm.routingkey";
        public static string Back_Exchange { get; set; } = "back.exchange";
        public static string Back_Queue { get; set; } = "back.queue";
        public static string Warning_Queue { get; set; } = "warning.queue";



        /// <summary>
        /// 得到一个Channel  作为轻量级的Connection极大减少了操作系统建立TCPconnection的开销
        /// </summary>
        /// <returns></returns>
        public static IModel GetChannel()
        {
            //创建一个连接工厂
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = "localhost";
            connectionFactory.UserName = "guest";
            connectionFactory.Password = "guest";
            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();
            return channel;
        }


        /// <summary>
        /// 创建一个延迟队列及其相关配置
        /// </summary>
        public static IModel GetTTLQueue()
        {
            var channel = RabbitmqUntils.GetChannel();

            // 声明X交换机
            // 声明Y交换机
            // 声明QA队列 ttl 为 10s 并绑定到对应的死信交换机
            // 声明QB队列 ttl 为 40s 并绑定到对应的死信交换机
            // 声明QD队列
            // 声明QC队列

            channel.ExchangeDeclare(RabbitmqUntils.X_Exchange, "direct");
            channel.ExchangeDeclare(RabbitmqUntils.Y_Exchange, "direct");


            var argumentsA = new Dictionary<string, object> { };
            argumentsA.Add("x-dead-letter-exchange", RabbitmqUntils.Y_Exchange);//声明当前队列绑定的死信交换机
            argumentsA.Add("x-dead-letter-routing-key", "YD");//声明当前队列的死信路由 key
            argumentsA.Add("x-message-ttl", 10000);//声明队列的 TTL
            channel.QueueDeclare(RabbitmqUntils.QA_Queue, false, false, false, argumentsA);
            channel.QueueBind(RabbitmqUntils.QA_Queue, RabbitmqUntils.X_Exchange, "XA");


            var argumentsB = new Dictionary<string, object> { };
            argumentsB.Add("x-dead-letter-exchange", RabbitmqUntils.Y_Exchange);//声明当前队列绑定的死信交换机
            argumentsB.Add("x-dead-letter-routing-key", "YD");//声明当前队列的死信路由 key
            argumentsB.Add("x-message-ttl", 40000);//声明队列的 TTL
            channel.QueueDeclare(RabbitmqUntils.QB_Queue, false, false, false, argumentsB);
            channel.QueueBind(RabbitmqUntils.QB_Queue, RabbitmqUntils.X_Exchange, "XB");


            channel.QueueDeclare(RabbitmqUntils.QD_Queue, false, false, false, null);
            channel.QueueBind(RabbitmqUntils.QD_Queue, RabbitmqUntils.Y_Exchange, "YD");


            // 申明QC队列并配置转发到死信队列QD
            // X交换机绑定QC
            var argumentsC = new Dictionary<string, object>();
            argumentsC.Add("x-dead-letter-exchange", Y_Exchange);
            argumentsC.Add("x-dead-letter-routing-key", "YD");
            channel.QueueDeclare(QC_Queue, false, false, false, argumentsC);
            channel.QueueBind(QC_Queue, X_Exchange, "XC"); //队列QC绑定X交换机

            return channel;
        }

        public static IModel GetDelayedQueue()
        {
            var channel = RabbitmqUntils.GetChannel();
            var arguments = new Dictionary<string, object>();
            arguments.Add("x-delayed-type", "direct");// 指定x-delayed-message 类型的交换机，并且添加x-delayed-type属性
            channel.ExchangeDeclare(Delayed_Exchange, "x-delayed-message", true,false, arguments);
            channel.QueueDeclare(Delayed_Queue,false,false,false,null);
            // 队列delayed.queue 绑定自定义交换机delayed.exchange
            channel.QueueBind(Delayed_Queue, Delayed_Exchange, Delayed_Routingkey);
            return channel;
        }


        /// <summary>
        /// 发布确认高级
        /// </summary>
        /// <returns></returns>
        public static IModel GetConfirmAdvancedQueue()
        {
            /*
                1. 声明confirm.exchange   type = direct
                2. 声明confirm.queue
                3. 声明confirm.routingkey
                4. 绑定队列与交换机
             */

            var channel = RabbitmqUntils.GetChannel();

            var arguments = new Dictionary<string, object>();
            arguments.Add("alternate-exchange",Back_Exchange);

            channel.ExchangeDeclare(Confirm_Exchange,"direct",true,false, arguments);
            channel.QueueDeclare(Confirm_Queue, false, false, false, null);
            channel.QueueBind(Confirm_Queue,Confirm_Exchange,Confirm_Routingkey);

            /*
               1. 声明backup.exchange type = fanout
               2. 声明backup.queue，warning.queue
               4. 绑定队列与交换机
               5. 配置确认交换机（Confirm_Exchange）转发到备份交换机（Back_Exchange）
            */
            channel.ExchangeDeclare(Back_Exchange, "fanout", true, false, null);
            channel.QueueDeclare(Back_Queue,false,false, false, null);
            channel.QueueDeclare(Warning_Queue,false,false, false, null);
            channel.QueueBind(Back_Queue, Back_Exchange, "");
            channel.QueueBind(Warning_Queue, Back_Exchange, "");

            return channel;
        }
    }

}