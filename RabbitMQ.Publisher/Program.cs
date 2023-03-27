using RabbitMQ.Client;
using System.Text;

//create connection
ConnectionFactory factory = new();
factory.HostName = "localhost";

//activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

////create queue
//channel.QueueDeclare(queue: "example-queue", exclusive: false, durable: true);
//IBasicProperties properties = channel.CreateBasicProperties();
//properties.Persistent = true;

//send message to queue
/*
 * RabbitMQ accept messages only byte type. So we have to convert messages to bytes.
 */

//for (int i = 1; i < 100; i++)
//{
//    byte[] message = Encoding.UTF8.GetBytes($"Hello world - {i}");
//    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message, basicProperties: properties);
//    Console.WriteLine($"Message {i} send");
//}

channel.ExchangeDeclare(exchange:"direct-exchange-example", type:ExchangeType.Direct);

while (true)
{
    Console.Write("Message : ");
    string message = Console.ReadLine();
    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(exchange:"direct-exchange-example",
        routingKey:"direct-queue-example",
        body:byteMessage);
}

Console.Read();
