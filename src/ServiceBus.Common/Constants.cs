using System;

namespace ServiceBus.Common
{
    public static class Constants
    {
        public const string ConnectionString = "{Connection_String}";
        public const string OrderCreatedQueueName = "OrderCreatedQueueTest";
        public const string OrderDeletedQueueName = "OrderDeletedQueueTest";
        public const string OrderTopicName = "OrderTopicTest";
        public const string OrderCreatedTopicSubscription = "OrderCreatedTopicSub";
        public const string OrderDeletedTopicSubscription = "OrderDeletedTopicSub";
    }
}
