using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Call_Dialogue : Command_Manager
{
    [HideInInspector] public DialogueDisplay display;
    int numberOfMessages;

    [Header("Dialogue Feed")]
    [Tooltip("One-time messages that play, in order, everytime Call() is used")]
    [SerializeField] List<Initiate_Dialogue> MessageCalls;
    Queue<Initiate_Dialogue> OutgoingMessages;

    [Tooltip("A repeated message that will play if no MessageCalls are left")]
    [SerializeField] Initiate_Dialogue RepeatedMessage;
    bool hasRepeatCall;

    void Awake()
    {
        display = GameObject.Find("HUD").GetComponent<DialogueDisplay>();
        OutgoingMessages = new Queue<Initiate_Dialogue>(MessageCalls);
        numberOfMessages = OutgoingMessages.Count;

        if(RepeatedMessage.Messages.Count > 0)
            hasRepeatCall = true;
    }

    ///<summary>Display dialogue message, returns true if successful</summary>
    public bool Call()
    {
        // Check if Call() can be used
        if(display == null || !HasMessages())
            return false;

        // Check if display is currently typing
        if(display.isTyping)
            return false;

        display.commandManager = this;

        // If there are still outgoing messages available, use them first
        if(OutgoingMessages.Count > 0)
        {
            display.DisplayText(OutgoingMessages.Dequeue().Messages);
            numberOfMessages = OutgoingMessages.Count;
        }
        // Otherwise, use repeated Message
        else
            display.DisplayText(RepeatedMessage.Messages);

        return true;
    }

    ///<summary>Check that Call_Dialogue object has messages left to send</summary>
    public bool HasMessages()
    {
        return (OutgoingMessages.Count > 0) || hasRepeatCall;
    }

    [Serializable]
    public struct Initiate_Dialogue
    {
        public List<string> Messages;
    }
}
