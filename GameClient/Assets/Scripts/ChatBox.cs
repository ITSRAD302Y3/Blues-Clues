using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChatBox : MonoBehaviour {

    public SignalRController SignalRController;
    public GameObject ListContent;
    public InputField txtMessage;

    public GameObject ChatMessageControl;

    private List<string> _pendingMessages = new List<string>();

    private void Update()
    {
        foreach (var message in _pendingMessages)
            AddMessage(message);

        _pendingMessages.Clear();
    }

    public void SendMessageToChat()
    {
        string message = txtMessage.text;

        if (!string.IsNullOrEmpty(message))
        {
            SignalRController.SendMessageToChat(message);
            AddMessage(FormatMessage(GameController.PlayerType) + ": " + message);

            txtMessage.ActivateInputField();
            txtMessage.Select();
            txtMessage.text = "";

            //EventSystem.current.SetSelectedGameObject(txtMessage.gameObject);
        }
    }

    public void OnPlayerJoined(string username)
    {
        _pendingMessages.Add(FormatMessage(username) + " has connected");
    }

    public void OnPlayerLeft(string username)
    {
        _pendingMessages.Add(FormatMessage(username) + " has left the game");
    }

    public void OnMessageReceived(string username, string message)
    {
        _pendingMessages.Add(FormatMessage(username) + ": " + message);
    }

    public void AddMessage(string message)
    {
        var messageControl = Instantiate(ChatMessageControl, ListContent.transform);
        var messageScript = messageControl.GetComponent<ChatMessage>();

        messageScript.txtMessage.text = message;
    }

    private string FormatMessage(string message)
    {
        switch (message)
        {
            case "Locator":
                return message.Insert(0, "<b><color=#f44242>") + "</color></b>";
            case "Answerer":
                return message.Insert(0, "<b><color=#4286f4>") + "</color></b>";
            default:
                return message.Insert(0, "<b><color=#42f442>") + "</color></b>";
        }
    }
}
