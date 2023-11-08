using rabbitmq.common;

using RabbitMQ.Client.Events;

using System;
using System.Collections.Concurrent;
using System.Text;

namespace PublishConfirm.Producer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("发布确认");
            //SinglePublishConfirm();  //单个发布确认耗时:00:00:00.6743354
            //MulitPublishConfirm(); //批量发布确认耗时:00:00:00.4526167
            PublishConfirmAsync(); // 异步发布确认:00:00:00.3534272
        }

        /// <summary>
        /// 单个发布确认
        /// </summary>
        public static void SinglePublishConfirm()
        {
            var begin = DateTime.Now;
            using var channel = RabbitmqUntils.GetChannel();
            string queueName = Guid.NewGuid().ToString();
            channel.QueueDeclare(queue:queueName,durable:false,exclusive:false,autoDelete:false,null);
            // 开启发布确认
            channel.ConfirmSelect();
            var begintime = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {                                                   
                var body = Encoding.UTF8.GetBytes($"{i}");
                channel.BasicPublish(exchange: "", queueName, false, null, body);
                if (channel.WaitForConfirms())// 等待所有消息确认，如果所有的消息都被服务端成功接收返回true，只要有一条没有被成功接收就返回false
                {
                    Console.WriteLine($"消息发送成功：{i}");
                }
                else
                {
                    //服务端返回 false 或超时时间内未返回，生产者可以消息重发 需添加补救措施
                }

            }
            var end = DateTime.Now;
            Console.WriteLine($"单个发布确认耗时:{end - begin}");
        }

        /// <summary>
        /// 批量发布确认
        /// </summary>
        public static void MulitPublishConfirm()
        {
            var begin = DateTime.Now;
            using var channel = RabbitmqUntils.GetChannel();
            string queueName = Guid.NewGuid().ToString();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, null);
            // 开启发布确认
            channel.ConfirmSelect();
            var begintime = DateTime.Now;
            for (int i = 0; i < 1000; i++)
            {
                var body = Encoding.UTF8.GetBytes($"{i}");
                channel.BasicPublish(exchange: "", queueName, false, null, body);

                // 批量确认：100条确认异常
                if (i%100 == 0)
                {

                    if (channel.WaitForConfirms())// 等待所有消息确认，如果所有的消息都被服务端成功接收返回true，只要有一条没有被成功接收就返回false
                    {
                        Console.WriteLine($"消息发送成功：{i}");
                    }
                    else
                    {
                        //服务端返回 false 或超时时间内未返回，生产者可以消息重发 需添加补救措施
                    }
                }
               

            }
            var end = DateTime.Now;
            Console.WriteLine($"批量发布确认耗时:{end - begin}");
        }

        /// <summary>
        /// 异步发布确认
        /// </summary>
        public static void PublishConfirmAsync()
        {
            var begin = DateTime.Now;
            using var channel = RabbitmqUntils.GetChannel();
            string queueName = Guid.NewGuid().ToString();
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, null);

            // 开启发布确认
            channel.ConfirmSelect();
            ConcurrentDictionary<ulong, string> confirmDic = new ConcurrentDictionary<ulong, string>();
            var begintime = DateTime.Now;
            for (int i = 1; i <= 1000; i++)
            {
                string msg = $"msg_{i}";
                var body = Encoding.UTF8.GetBytes(msg);
                confirmDic.TryAdd(channel.NextPublishSeqNo, msg);
                channel.BasicPublish(exchange: "", queueName, false, null, body);
            }

            // 监听确认的消息
            channel.BasicAcks += (sender, e) =>
            {
                Console.WriteLine($"监听确认的消息的序列号:{e.DeliveryTag}");
                confirmDic.Remove(e.DeliveryTag, out string body);
                Console.WriteLine($"删除确认的消息:{body}");
            };

            // 监听未确认的消息
            channel.BasicNacks += (sender, e) =>
            {
                Console.WriteLine($"监听未确认的消息序列号:{e.DeliveryTag}");
                confirmDic.TryGetValue(e.DeliveryTag, out string body);
                Console.WriteLine($"监听未确认的消息:{body}");

            };
            var end = DateTime.Now;
            Console.WriteLine($"异步发布确认:{end - begin}");
        }
    }
}