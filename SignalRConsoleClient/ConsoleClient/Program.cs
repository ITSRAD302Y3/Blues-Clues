using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        private static string _endpoint = "https://rad302signalrserver.azurewebsites.net/";
        private static string _hubName = "ChatHub";
        private static string _username = "Locator";

        static void Main(string[] args)
        {
            ConnectToHub();

            bool stayConnected = true;

            while (stayConnected)
            {
                Console.Write("Enter your message: ");
                string message = Console.ReadLine();

                if (message == "-1")
                {
                    stayConnected = false;
                }
                else
                {
                    Console.WriteLine("{0} says, {1}", _username, message);
                    _proxy.Invoke("SendMessage", _username, message);
                }
            }

            _proxy.Invoke("Leave", "Locator");

            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

        private static HubConnection _connection;
        private static IHubProxy _proxy;

        public static void ConnectToHub()
        {
            if (!string.IsNullOrEmpty(_endpoint) && !string.IsNullOrEmpty(_hubName))
            {
                _connection = new HubConnection(_endpoint);
                _proxy = _connection.CreateHubProxy(_hubName);

                // Setup methods and links
                _proxy.On("PlayerJoined", new Action<string>(OnPlayerJoined));
                _proxy.On("PlayerLeft", new Action<string>(OnPlayerLeft));
                _proxy.On("ReceiveMessage", new Action<string, string>(OnReceiveMessage));

                // Connect to server
                _connection.Received += _connection_Received;
                _connection.Start().Wait(); // Wait for tasks to complete
                Console.WriteLine("Connected");

                _proxy.Invoke("Join", "Locator");
            }
        }

        private static void _connection_Received(string obj)
        {

        }

        // Mapped on to hub in server
        public static void OnReceiveMessage(string sender, string message)
        {
            Console.WriteLine("{0} says, {1}", sender, message);
        }

        public static void OnPlayerJoined(string username)
        {
            Console.WriteLine(username + " has connected");
        }

        public static void OnPlayerLeft(string username)
        {
            Console.WriteLine(username + " has left the game");
        }
    }
}
