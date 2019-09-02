
using System;
using UnityEngine;

public class Conversation : MonoBehaviour
{
    [Serializable]
    public class Chat
    {
        public bool Player;
        public string Message;
    }
    
    public GameObject NPC;
    public float Time;
    public string ExpectedTag;
    public GameObject NextEvent;
    public Chat[] Messages;
    public bool Active;
    
    private int _current;
    private ChatText _npcText;
    private ChatText _playerText;
    private bool _triggered;

    void Start()
    {
        if (NPC != null)
        {
            _npcText = NPC.GetComponent<ChatText>();
        }

        _triggered = false;
    }

    void BeginConversation()
    {
        _current = 0;
        _playerText.SetConversation(this);
        _npcText.SetConversation(this);
        
        InvokeRepeating(nameof(DisplayMessage), 0f, Time);
    }

    int DisplayMessage()
    {
        _npcText.Clear();
        _playerText.Clear();
        
        if (_current == Messages.Length)
        {
            CancelInvoke(nameof(DisplayMessage));
            _playerText.ClearConversation();
            _npcText.ClearConversation();

            if (NextEvent != null)
            {
                Instantiate(NextEvent);
            }

            return 0;
        }
        
        var msg = Messages[_current];
        if (msg.Player)
        {
            _playerText.Display(msg.Message);
        }
        else _npcText.Display(msg.Message);

        _current += 1;
        return 0;
    }

    public void Skip()
    {
        CancelInvoke(nameof(DisplayMessage));
        InvokeRepeating(nameof(DisplayMessage), 0f, Time);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (Active && other.gameObject.CompareTag(ExpectedTag) && !_triggered)
        {
            Begin(_npcText, other.gameObject.GetComponent<ChatText>());
        }
    }

    public void Begin(ChatText first, ChatText other)
    {
        _npcText = first;
        _playerText = other;

        _triggered = true; // REEEEEE
        
        BeginConversation();
    }
}
