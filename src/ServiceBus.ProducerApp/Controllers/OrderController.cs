using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServiceBus.Common;
using ServiceBus.Common.Dto;
using ServiceBus.Common.Events;
using ServiceBus.ProducerApp.Services;

namespace ServiceBus.ProducerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController
    {
        private readonly AzureService azureService;

        public OrderController(AzureService azureService)
        {
            this.azureService = azureService;
        }
        [HttpPost("CreateOrder")]
        public async Task CreateOrder(OrderDto order)
        {
            // Insert order into database
            var orderCreatedEvent = new OrderCreatedEvent
            {
                Id = order.Id,
                ProductName = order.ProductName,
                CreatedOn = DateTime.Now
            };
            await azureService.CreateQueueIfNotExists(Constants.OrderCreatedQueueName);
            await azureService.SendMessageToQueue(Constants.OrderCreatedQueueName, orderCreatedEvent);
        }

        [HttpDelete("{id}")]
        public async Task DeleteOrder(int id)
        {
            var orderDeletedEvent = new OrderDeletedEvent
            {
                Id = id,
            };
            await azureService.CreateQueueIfNotExists(Constants.OrderDeletedQueueName);
            await azureService.SendMessageToQueue(Constants.OrderDeletedQueueName, orderDeletedEvent);
        }

        [HttpPost("CreateOrderViaTopic")]
        public async Task CreateOrderViaTopic(OrderDto order)
        {
            // Insert order into database
            var orderCreatedEvent = new OrderCreatedEvent
            {
                Id = order.Id,
                ProductName = order.ProductName,
                CreatedOn = DateTime.Now
            };
            await azureService.CreateTopicIfNotExists(Constants.OrderTopicName);
            await azureService.CreateSubscriptionIfNotExists(Constants.OrderTopicName, Constants.OrderCreatedTopicSubscription, "OrderCreated", "OrderCreatedOnly");
            await azureService.SendMessageToTopic(Constants.OrderTopicName, orderCreatedEvent, "OrderCreated");
        }

        [HttpDelete("DeleteOrderViaTopic")]
        public async Task DeleteOrderViaTopic(OrderDto order)
        {
            // Insert order into database
            var orderDeletedEvent = new OrderDeletedEvent
            {
                Id = order.Id,
                CreatedOn = DateTime.Now
            };
            await azureService.CreateTopicIfNotExists(Constants.OrderTopicName);
            await azureService.CreateSubscriptionIfNotExists(Constants.OrderTopicName, Constants.OrderDeletedTopicSubscription, "OrderDeleted", "OrderDeletedOnly");
            await azureService.SendMessageToTopic(Constants.OrderTopicName, orderDeletedEvent, "OrderDeleted");
        }
    }
}