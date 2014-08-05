using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ByteNom.Demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start a server
            var server = new Server(12345);
            server.ConnectionReceived += server_ConnectionReceived;
            server.Start();

            // Start a client
            var client = new Client("localhost", 12345);
            client.MessageReceived += client_MessageReceived;
            client.Connect();
            client.Send("hello world");

            Thread.Sleep(Timeout.Infinite);
        }

        // Called whenever our client receives a message
        static void client_MessageReceived(object sender, Message message)
        {
            if (message.Type == "HAI")
            {
                Console.WriteLine("HAI message received from server!");
            }
        }

        // Called whenever our server receives a new connection
        static void server_ConnectionReceived(object sender, Connection connection)
        {
            connection.MessageReceived += connection_MessageReceived;
            connection.Send("HAI");
        }

        // Called whenever a client sends a message to our server
        static void connection_MessageReceived(object sender, Message message)
        {
            if (message.Type == "hello world")
            {
                Console.WriteLine("hello world message received from server!");
            }
        }
    }
}
