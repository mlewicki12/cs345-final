
using System;
using UnityEngine;

public class ConversationEvent : MonoBehaviour
{
    public string FirstName;
    public string SecondName;

    private Conversation _conv;

    private void Start()
    {
        _conv = gameObject.GetComponent<Conversation>();

        var chatf = GameObject.Find(FirstName).GetComponent<ChatText>();
        var chats = GameObject.Find(SecondName).GetComponent<ChatText>();
        
        _conv.Begin(chatf, chats);
    }
}