//create connection
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new();
factory.HostName = "localhost";

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

#region Direct Exchange

//channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);
//var queueName = channel.QueueDeclare().QueueName;
//channel.QueueBind(queue: queueName, exchange: "direct-exchange-example", routingKey: "direct-queue-example");
//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: true,
//    consumer: consumer);
//consumer.Received += (sender, e) =>
//{
//string message = Encoding.UTF8.GetString(e.Body.Span);
//Console.WriteLine(message);
//};

#endregion

#region Fanout Exchange

//channel.ExchangeDeclare(
//    exchange: "fanout-exchange-example",
//    type: ExchangeType.Fanout);
//Console.Write("Enter a queue name : ");
//string queueName = Console.ReadLine();
//channel.QueueDeclare(
//    queue: queueName,
//    exclusive: false);
//channel.QueueBind(
//    queue:queueName,
//    exchange: "fanout-exchange-example",
//    routingKey: string.Empty);
//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck:true,
//    consumer:consumer);
//consumer.Received += (sender, e) =>
//{
//    string message = Encoding.UTF8.GetString(e.Body.Span);
//    Console.WriteLine(message);
//};

#endregion

#region Topic Exchange

//channel.ExchangeDeclare(
//    exchange: "topic-exchange-example",
//    type: ExchangeType.Topic);
//Console.Write("Specify the format of the topic to be listened to : ");
//string topic = Console.ReadLine();
//string queueName = channel.QueueDeclare().QueueName;
//channel.QueueBind(
//    queue: queueName,
//    exchange: "topic-exchange-example",
//    routingKey: topic);
//EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: true,
//    consumer);
//consumer.Received += (sender, e) =>
//{
//    string message = Encoding.UTF8.GetString(e.Body.Span);
//    Console.WriteLine(message);
//};

#endregion

#region Header Exhange

//channel.ExchangeDeclare(
//    exchange: "header-exchange-example",
//    type: ExchangeType.Headers);
//Console.Write("Please enter header value : ");
//string value = Console.ReadLine();
//string queueName = channel.QueueDeclare().QueueName;
//channel.QueueBind(
//    queue:queueName,
//    exchange: "header-exchange-example",
//    routingKey: string.Empty,
//    new Dictionary<string, object>
//    {
//        ["no"] = value
//    });
//EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: true,
//    consumer: consumer);
//consumer.Received += (sender, e) =>
//{
//string message = Encoding.UTF8.GetString(e.Body.Span);
//Console.WriteLine(message);
//};

#endregion



#region Point To Point (P2P) Pattern

//string queueName = "example-p2p-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//EventingBasicConsumer consumer = new EventingBasicConsumer(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: false,
//    consumer: consumer);

//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion

#region Publish/Subscribe (Pub/Sub) Pattern

//string exchangeName = "example-pub-sub-exchange";

//channel.ExchangeDeclare(
//    exchange: exchangeName,
//    type: ExchangeType.Fanout);

//string queueName = channel.QueueDeclare().QueueName;

//channel.QueueBind(
//    queue: queueName,
//    exchange: exchangeName,
//    routingKey: string.Empty
//    );

//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: false,
//    consumer: consumer);

//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion

#region Work Queue Pattern

//string queueName = "example-work-queue";

//channel.QueueDeclare(
//    queue: queueName,
//    durable: false,
//    exclusive: false,
//    autoDelete: false);

//EventingBasicConsumer consumer = new (channel);
//channel.BasicConsume(
//    queue: queueName,
//    autoAck: true,
//    consumer: consumer);

//channel.BasicQos(
//    prefetchCount: 1,
//    prefetchSize: 0,
//    global: false);

//consumer.Received += (sender, e) =>
//{
//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
//};

#endregion

#region Request/Response Pattern

string requestQueueName = "example-request-queque";
channel.QueueDeclare(
    queue: requestQueueName,
    durable: false,
    exclusive: false,
    autoDelete: false);

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(
    queue: requestQueueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);

    byte[] responseMessage = Encoding.UTF8.GetBytes($"Process completed : {message}");

    IBasicProperties properties = channel.CreateBasicProperties();
    properties.CorrelationId = e.BasicProperties.CorrelationId;

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: e.BasicProperties.ReplyTo,
        basicProperties: properties,
        body: responseMessage);

};

#endregion

Console.Read();