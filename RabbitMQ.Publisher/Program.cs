using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.HostName = "localhost";

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region Direct Exchange

//channel.ExchangeDeclare(exchange:"direct-exchange-example", type:ExchangeType.Direct);

//while (true)
//{
//    Console.Write("Message : ");
//    string message = Console.ReadLine();
//    byte[] byteMessage = Encoding.UTF8.GetBytes(message);

//    channel.BasicPublish(exchange:"direct-exchange-example",
//        routingKey:"direct-queue-example",
//        body:byteMessage);
//}

#endregion

#region Fanout Exchange

//channel.ExchangeDeclare(
//    exchange: "fanout-exchange-example",
//    type: ExchangeType.Fanout);
//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);
//    byte[] message = Encoding.UTF8.GetBytes($"Hello {i}");
//    channel.BasicPublish(
//        exchange: "fanout-exchange-example",
//        routingKey: string.Empty,
//        body: message);
//}

#endregion

#region Topic Exchange

//channel.ExchangeDeclare(
//    exchange: "topic-exchange-example",
//    type: ExchangeType.Topic);
//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);
//    byte[] message = Encoding.UTF8.GetBytes($"Message {i} recieved.");
//    Console.WriteLine("Please choose topic for queue : ");
//    string topic = Console.ReadLine();
//    channel.BasicPublish(
//        exchange: "topic-exchange-example",
//        routingKey: topic,
//        body: message);
//}

#endregion

#region Header Exhange

//channel.ExchangeDeclare(
//    exchange: "header-exchange-example",
//    type: ExchangeType.Headers);
//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);
//    byte[] message = Encoding.UTF8.GetBytes($"Message {i} recieved");
//    Console.Write("Please enter header value : ");
//    string value = Console.ReadLine();
//    IBasicProperties basicProperties = channel.CreateBasicProperties();
//    basicProperties.Headers = new Dictionary<string, object>
//    {
//        ["no"] = value
//    };
//    channel.BasicPublish(
//        exchange: "header-exchange-example",
//        routingKey: string.Empty,
//        body: message,
//        basicProperties: basicProperties);
//}

#endregion



#region Point To Point (P2P) Pattern

//string queueName = "example-p2p-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//byte[] message = Encoding.UTF8.GetBytes("Hello!");

//channel.BasicPublish(
//    exchange: string.Empty,
//    routingKey: queueName,
//    body: message);

#endregion

#region Publish/Subscribe (Pub/Sub) Pattern

//string exchangeName = "example-pub-sub-exchange";

//channel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);


//for (int i = 0; i < 100; i++)
//{
//    byte[] message = Encoding.UTF8.GetBytes("Hello!");
//    await Task.Delay(200);

//    channel.BasicPublish(exchange: exchangeName,
//        routingKey: string.Empty,
//        body: message);
//}

#endregion

#region Work Queue Pattern

//string queueName = "example-work-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//for (int i = 0; i < 100; i++)
//{
//    await Task.Delay(200);

//    byte[] message = Encoding.UTF8.GetBytes($"Message {i}");

//    channel.BasicPublish(
//        exchange: string.Empty,
//        routingKey: queueName,
//        body: message);
//}

#endregion

#region Request/Response Pattern

string requestQueueName = "example-request-queque";
channel.QueueDeclare(
    queue: requestQueueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

string replyQueueName = channel.QueueDeclare().QueueName;

string correlationId = Guid.NewGuid().ToString();

//Generate & Send Request Message
IBasicProperties properties = channel.CreateBasicProperties();
properties.CorrelationId = correlationId;
properties.ReplyTo = replyQueueName;

for (int i = 0; i < 10; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Hello" + i);

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: requestQueueName,
        body: message,
        basicProperties: properties);
}

//Listening Response Queue
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: replyQueueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    if (e.BasicProperties.CorrelationId == correlationId)
    {
        Console.WriteLine($"response : {Encoding.UTF8.GetString(e.Body.Span)}");
    }
};

#endregion

Console.Read();
