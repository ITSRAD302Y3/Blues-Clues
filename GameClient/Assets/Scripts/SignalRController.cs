using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;

public class SignalRController : MonoBehaviour
{
    public ChatBox chat;

    public string Endpoint = "https://rad302signalrserver.azurewebsites.net/";
    public string HubName = "ChatHub";

    void Start()
    {
        DontDestroyOnLoad(this); // Persistent between scenes

        ConnectToHub(); // Connect to server
        // Join chat
    }

    private void OnApplicationQuit()
    {
        // Leave chat
        if (_connected)
            _connection.Stop();
    }

    private HubConnection _connection;
    private IHubProxy _proxy;
    bool _connected;

    public void ConnectToHub()
    {
        if (!string.IsNullOrEmpty(Endpoint) && !string.IsNullOrEmpty(HubName))
        {
            _connection = new HubConnection(Endpoint);
            _proxy = _connection.CreateHubProxy(HubName);

            // Add actions to proxy
            _proxy.On("ReceiveMessage", new Action<string, string>(chat.OnMessageReceived));
            _proxy.On("PlayerJoined", new Action<string>(chat.OnPlayerJoined));
            _proxy.On("PlayerLeft", new Action<string>(chat.OnPlayerLeft));

            _connection.Start().Wait();
            _connected = true;
            Debug.Log("Connected");
        }
    }

    public void JoinChat()
    {
        if (_connected)
        {
            _proxy.Invoke("Join", GameController.PlayerType);
        }
    }

    public void LeaveChat()
    {
        if (_connected)
        {
            _proxy.Invoke("Leave", GameController.PlayerType);
        }
    }

    public void SendMessageToChat(string message)
    {
        if (_connected)
        {
            _proxy.Invoke("SendMessage", GameController.PlayerType, message);
        }
    }
}
