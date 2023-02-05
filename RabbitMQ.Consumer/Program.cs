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
channel.BasicConsume(queue:"example-queue", autoAck: false, consumer: consumer);
consumer.Received += (sender, e) =>
{
    /*
     * queued message will be processed here
     * e.Body : get in the queue message's complete data 
     * e.Body.Span or e.Body.ToArray() : get in the queue message's byte data
     */
    Thread.Sleep(1000);
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    /*
     * multiple : send signal for all messages sent before the processed message
     * requeue : specifies whether to add the reported message back to the queue.
     * BasicNack :  indicates that the message will not be processed
     * BascicAck : indicates that the message processed
     */
    channel.BasicNack(deliveryTag: e.DeliveryTag, multiple: false, requeue: false);
};

Console.Read();