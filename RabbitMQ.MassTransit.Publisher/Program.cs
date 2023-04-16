
using MassTransit;
using RabbitMQ.MassTransit.Shared;

string queueName = "example-queue";

IBusControl bus = Bus.Factory.CreateUsingRabbitMq(factory =>
{
    factory.Host("rabbitmq://localhost");
});

ISendEndpoint sendEndpoint = await bus.GetSendEndpoint(new($"rabbitmq://localhost/{queueName}"));

Console.Write("Message to be sent : ");
string message = Console.ReadLine();

await sendEndpoint.Send<IMessage>(new Message()
{
    Text = message
});

Console.Read();