using RabbitMQ.Client;
using System.Text;

//create connection
ConnectionFactory factory = new();
factory.HostName = "localhost";

//activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//create queue
channel.QueueDeclare(queue: "example-queue", exclusive: false);

//send message to queue
/*
 * RabbitMQ accept messages only byte type. So we have to convert messages to bytes.
 */
for (int i = 1; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes($"Hello world - {i}");
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);
    Console.WriteLine($"Message {i} send");
}

Console.Read();
