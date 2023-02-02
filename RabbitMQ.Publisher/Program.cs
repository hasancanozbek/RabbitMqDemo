using RabbitMQ.Client;
using System.Text;

//create connection
ConnectionFactory factory = new ();
factory.HostName= "localhost";

//activate connection and create channel
using IConnection connection = factory.CreateConnection ();
using IModel channel = connection.CreateModel();

//create queue
channel.QueueDeclare(queue:"exapmle-queue", exclusive:false );

//send message to queue
/*
 * RabbitMQ accept messages only byte type. So we have to convert messages to bytes.
 */
byte[] message = Encoding.UTF8.GetBytes("Hello world!");
channel.BasicPublish(exchange:"", routingKey: "exapmle-queue", body:message);

Console.Read();
