using Microsoft.AspNetCore.Mvc;

using rabbitmq.common;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TTLExchange
{

    [Route("api/[controller]")]
    [ApiController]
    public class TestContorller:ControllerBase
    {
        private ILogger<TestContorller> logger;

        public TestContorller(ILogger<TestContorller> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        [Route("api/test/SendMsg")]
        public IActionResult SendMsg([Required]string msg)
        {
            logger.LogInformation($"开始发送消息");
            using var channel = RabbitmqUntils.GetTTLQueue();
            var qaMsg = $"消息来自ttl 为 10s QA队列{msg}";
            var qbMsg = $"消息来自ttl 为 40s QB队列{msg}";
            var qaBody = Encoding.UTF8.GetBytes(qaMsg);
            var qbBody = Encoding.UTF8.GetBytes(qbMsg);
            channel.BasicPublish(RabbitmqUntils.X_Exchange,"XA",false,null, qaBody);
            channel.BasicPublish(RabbitmqUntils.X_Exchange,"XB",false,null, qbBody);

            logger.LogInformation($"{DateTime.Now} 发送消息：{qaMsg}");
            logger.LogInformation($"{DateTime.Now} 发送消息：{qbMsg}");
            logger.LogInformation($"发送完成");

            return Ok($"{DateTime.Now} 发送消息：{msg}");
        }

        [HttpGet]
        [Route("api/test/SenExpirationMsg")]
        public IActionResult SenExpirationMsg([Required] string msg, [Required] string expiration)
        {
            logger.LogInformation($"开始发送消息");
            using var channel = RabbitmqUntils.GetTTLQueue();          
            var body = Encoding.UTF8.GetBytes(msg);

            // 设置消息TTL
            var props = channel.CreateBasicProperties();
            props.Expiration = expiration;
            channel.BasicPublish(RabbitmqUntils.X_Exchange, "XC", false, props, body);

            logger.LogInformation($"{DateTime.Now} 发送消息：{msg} expiration={expiration}ms");
            return Ok();
        }

        [HttpGet]
        [Route("api/test/SendDelayedMsg")]
        public IActionResult SendDelayedMsg([Required] string msg, [Required] string delayedTime)
        {
            logger.LogInformation($"开始发送消息");
            using var channel = RabbitmqUntils.GetDelayedQueue();
            var body = Encoding.UTF8.GetBytes(msg);
            
            var props = channel.CreateBasicProperties();
            props.Headers = new Dictionary<string, object> 
            {
                { "x-delay", delayedTime }   // 一定要设置，否则无效
            };
            channel.BasicPublish(RabbitmqUntils.Delayed_Exchange, RabbitmqUntils.Delayed_Routingkey, false, props, body);

            logger.LogInformation($"{DateTime.Now} 发送消息：{msg} delayedTime={delayedTime}ms");
            return Ok();
        }

        [HttpGet]
        [Route("api/test/ReciveMsg")]
        public IActionResult ReciveMsg()
        {
            logger.LogInformation($"开始接受消息");
            using var channel = RabbitmqUntils.GetTTLQueue();
            //事件对象
            var consumer = new EventingBasicConsumer(channel);           
            // 接收消息回调
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                logger.LogInformation($"{DateTime.Now} 接收消息：{message}");
                channel.BasicAck(e.DeliveryTag, false);
            };
            // autoAck：false 手动应答
            channel.BasicConsume(queue: RabbitmqUntils.QD_Queue, false, consumer);

            Thread.Sleep(60000); // 注意测试需要：此处需要手动休眠等待 演示消息正常消费，否则return之后线程结束,消费者即不在线无法正常消费消息
            return Ok();
        }

        [HttpGet]
        [Route("api/test/ReciveDelayedMsg")]
        public IActionResult ReciveDelayedMsg()
        {
            logger.LogInformation($"开始接受消息");
            using var channel = RabbitmqUntils.GetDelayedQueue();
            //事件对象
            var consumer = new EventingBasicConsumer(channel);
            // 接收消息回调
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                logger.LogInformation($"{DateTime.Now} 接收消息：{message}");
                channel.BasicAck(e.DeliveryTag, false);
            };
            // autoAck：false 手动应答
            channel.BasicConsume(queue: RabbitmqUntils.Delayed_Queue, false, consumer);

            Thread.Sleep(60000); // 注意测试需要：此处需要手动休眠等待 演示消息正常消费，否则return之后线程结束,消费者即不在线无法正常消费消息
            return Ok();
        }


    }
}
