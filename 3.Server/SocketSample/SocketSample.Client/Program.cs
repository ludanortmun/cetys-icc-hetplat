using System.Net;
using System.Net.Sockets;

namespace SocketSample.Client
{
    internal class Program
    {
        private const int ServerPort = 8080;

        static void Main(string[] args)
        {
            // Defines the endpoint (host and port) for the server to listen on.
            // This will use the loopback address (127.0.0.1) and the port defined above.
            IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Loopback, ServerPort);

            // Creates a new socket that will act as the client.
            // This is using the TCP protocol.
            Socket client = new Socket(
                serverEndpoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            // Connects the client to the specified endpoint.
            client.Connect(serverEndpoint);

            // Prepare to send a message to the server.
            Console.WriteLine("Enter a message to send to the server:");
            string message = Console.ReadLine() ?? string.Empty;

            // Encode the message into bytes.
            byte[] messageBytes = System.Text.Encoding.ASCII.GetBytes(message);

            // Send the message to the server.
            client.Send(messageBytes);
            Console.WriteLine($"Sent message: {message}");

            // Prepare to receive a response from the server.
            byte[] buffer = new byte[1024];
            int bytesRead = client.Receive(buffer);

            if (bytesRead > 0)
            {
                // Decode the received bytes into a string.
                string response = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"[SERVER]: {response}");
            }
            else
            {
                Console.WriteLine("--No response received from server.--");
            }

            // Close the client socket when done.
            client.Dispose();
            Console.WriteLine("Client disconnected.");
        }
    }
}
