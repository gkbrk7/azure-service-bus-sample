using System;

namespace ServiceBus.Common
{
    public static class Constants
    {
        public const string ConnectionString = "Endpoint=sb://pubinnoservicebus.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=hOU3kkp1hGi706AHg7Uevn+RATR0kgscq/Au4icxMtI=";
        public const string OrderCreatedQueueName = "OrderCreatedQueueTest";
        public const string OrderDeletedQueueName = "OrderDeletedQueueTest";
        public const string OrderTopicName = "OrderTopicTest";
        public const string OrderCreatedTopicSubscription = "OrderCreatedTopicSub";
        public const string OrderDeletedTopicSubscription = "OrderDeletedTopicSub";
    }
}
