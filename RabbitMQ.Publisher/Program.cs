using RabbitMQ.Client;
using System.Text;

//create connection
ConnectionFactory factory = new();
factory.HostName = "localhost";

//activate connection and create channel
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


Console.Read();
