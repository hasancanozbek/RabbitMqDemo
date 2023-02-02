//create connection
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new ();
factory.HostName = "localhost";

//activate connection and create channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel ();

//create queue
channel.QueueDeclare(queue: "example-queue", exclusive:false);
/*
 * The consumer's queue and publisher's queue must be in the same configuration. 
 */

//read message from queue
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume("example-queue", false, consumer);
consumer.Received += (sender, e) =>
{
    /*
     * queued message will be processed here
     * e.Body : get in the queue message's complete data 
     * e.Body.Span or e.Body.ToArray() : get in the queue message's byte data
     */

    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.Read();