using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using ServiceBus.Common;
using ServiceBus.Common.Events;

namespace ServiceBus.ConsumerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("All Queues Listeners have started.");
            ConsumeQueue<OrderCreatedEvent>(Constants.OrderCreatedQueueName, x =>
            {
                System.Console.WriteLine($"OrderCreatedEvent ReceivedMessage with id: {x.Id}, Name: {x.ProductName}");
            }).Wait();

            ConsumeQueue<OrderDeletedEvent>(Constants.OrderDeletedQueueName, x =>
            {
                System.Console.WriteLine($"OrderCreatedEvent ReceivedMessage with id: {x.Id}");
            }).Wait();

            ConsumeSubcription<OrderCreatedEvent>(Constants.OrderTopicName, Constants.OrderCreatedTopicSubscription, x =>
            {
                System.Console.WriteLine($"OrderCreatedEvent ReceivedMessage with id: {x.Id}, Name: {x.ProductName}");
            }).Wait();

            ConsumeSubcription<OrderDeletedEvent>(Constants.OrderTopicName, Constants.OrderDeletedTopicSubscription, x =>
            {
                System.Console.WriteLine($"OrderDeletedEvent ReceivedMessage with id: {x.Id}");
            }).Wait();

            Console.ReadLine();
        }

        private static async Task ConsumeQueue<T>(string queueName, Action<T> receivedAction)
        {
            IQueueClient client = new QueueClient(Constants.ConnectionString, queueName);
            client.RegisterMessageHandler(async (message, ct) =>
            {
                var model = JsonConvert.DeserializeObject<T>(Encoding.ASCII.GetString(message.Body));
                receivedAction(model);
                await Task.CompletedTask;
            }, new MessageHandlerOptions(x => Task.CompletedTask));
            System.Console.WriteLine($"{typeof(T).Name} is listening...");
        }

        private static async Task ConsumeSubcription<T>(string topicName, string subscriptionName, Action<T> receivedAction)
        {
            ISubscriptionClient client = new SubscriptionClient(Constants.ConnectionString, topicName, subscriptionName);
            client.RegisterMessageHandler(async (message, ct) =>
            {
                var model = JsonConvert.DeserializeObject<T>(Encoding.ASCII.GetString(message.Body));
                receivedAction(model);
                await Task.CompletedTask;
            }, new MessageHandlerOptions(x => Task.CompletedTask));
            System.Console.WriteLine($"{typeof(T).Name} is listening...");
        }
    }
}
