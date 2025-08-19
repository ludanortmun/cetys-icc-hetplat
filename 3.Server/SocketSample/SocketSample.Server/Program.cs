using System.Net;
using System.Net.Sockets;

namespace SocketSample.Server
{
    internal class Program
    {
        private const int Port = 8080;

        static void Main(string[] args)
        {
            // Defines the endpoint (host and port) for the server to listen on.
            // This will use the loopback address (127.0.0.1) and the port defined above.
            IPEndPoint serverEndpoint = new IPEndPoint(IPAddress.Loopback, Port);

            // Creates a new socket that will act as the server listener.
            // This is using the TCP protocol.
            Socket listener = new Socket(
                serverEndpoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            // Binds the socket to the specified endpoint.
            listener.Bind(serverEndpoint);

            // Starts listening for incoming connections.
            listener.Listen();

            Console.WriteLine($"Server is listening on {serverEndpoint}");

            // The server needs to always be ready to handle incoming connections,
            // so we will use a loop to continuously accept and process client connections.
            while (true)
            {
                // Accepts an incoming connection. 
                // It creates a new socket each time a client connects.
                Socket clientSocket = listener.Accept();

                // Accepts an incoming connection.
                Console.WriteLine($"Accepted connection from {clientSocket.RemoteEndPoint}");

                // Because we want to handle multiple clients concurrently,
                // each client connection will be processed in a separate task in another thread.
                Task.Run(() =>
                {
                    try
                    {
                        // Read the stream of data sent by the client.
                        byte[] buffer = new byte[1024];
                        int bytesRead = clientSocket.Receive(buffer);
                        Console.WriteLine($"Received {bytesRead} bytes from client.");

                        // Once data is received, we can decode it to a string.
                        string receivedData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received data: {receivedData} from {clientSocket.RemoteEndPoint}");

                        // To send a response back to the client, we can use the same socket.
                        // First, we prepare a response message. In this case, we will just echo back the received data.
                        string responseMessage = $"You said: {receivedData}";

                        // Then we need to encode it into bytes.
                        byte[] responseBytes = System.Text.Encoding.ASCII.GetBytes(responseMessage);

                        // Finally, we send the response back to the client.
                        clientSocket.Send(responseBytes);
                    }
                    finally
                    {
                        // Ensure the client socket is disposed of after processing.
                        clientSocket.Dispose();
                        Console.WriteLine("Client socket disposed.");
                    }
                });
            }
        }
    }
}
