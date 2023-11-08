using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using rabbitmq.common;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PublishConfirmAdvanced
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfirmController : ControllerBase
    {
        private ILogger<ConfirmController> _logger;

        public ConfirmController(ILogger<ConfirmController> logger)
        {
            this._logger = logger;
        }

        [HttpGet]
        [Route("api/confirm/SendMsg")]
        public IActionResult SendMsg([Required] string msg)
        {
            _logger.LogInformation($"开始发送消息:");
            using var channel = RabbitmqUntils.GetConfirmAdvancedQueue();            
            var body = Encoding.UTF8.GetBytes(msg);
            channel.ConfirmSelect();// 开启发布确认
            channel.BasicPublish(RabbitmqUntils.Confirm_Exchange, RabbitmqUntils.Confirm_Routingkey, false, null, body);
            if (channel.WaitForConfirms())
            { 
            }
            else
            {
            }

            // 监听确认的消息
            channel.BasicAcks += (sender, e) =>
            {
                _logger.LogInformation($"交换机已收到:{e.DeliveryTag}");
                _logger.LogInformation($"交换机已收到消息的序列号:{e.DeliveryTag}");
            };

            // 监听未确认的消息
            channel.BasicNacks += (sender, e) =>
            {
                _logger.LogInformation($"交换机未收到:{e.DeliveryTag}");
                _logger.LogInformation($"交换机未收到消息的序列号:{e.DeliveryTag}");

            };
            return Ok();
        }

        [HttpGet]
        [Route("api/confirm/ReciveMsg")]
        public IActionResult ReciveMsg()
        {
            _logger.LogInformation("开始接受消息:");
            var channel = RabbitmqUntils.GetConfirmAdvancedQueue();
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += ((sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _logger.LogInformation($"{DateTime.Now} 接收消息：{message}");
                channel.BasicAck(e.DeliveryTag, false);
            });
            channel.BasicConsume(RabbitmqUntils.Confirm_Queue, false, consumer);
            Thread.Sleep(60000);
            return Ok();
        }
    }

    public class ServiceClass
    {
        public void ShowMsg()
        {
            Console.WriteLine("SowMsg");
        }
    }



    
}
