
using MassTransit;
using RabbitMQ.MassTransit.Shared;

namespace RabbitMQ.MassTransit.Consumer.Consumers
{
    public class ExampleMessageConsumer : IConsumer<IMessage>
    {
        public Task Consume(ConsumeContext<IMessage> context)
        {
            Console.WriteLine($"Received Message : { context.Message.Text}");
            return Task.CompletedTask;
        }
    }
}
