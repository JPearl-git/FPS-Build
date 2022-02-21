using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Call_Dialogue : MonoBehaviour
{
    [HideInInspector] public DialogueDisplay display;
    [HideInInspector] public int numberOfMessages;

    [SerializeField] List<Initiate_Dialogue> MessageCalls;
    Queue<Initiate_Dialogue> OutgoingMessages;

    void Awake()
    {
        display = GameObject.Find("HUD").GetComponent<DialogueDisplay>();
        OutgoingMessages = new Queue<Initiate_Dialogue>(MessageCalls);
        numberOfMessages = OutgoingMessages.Count;
    }

    public bool Call()
    {
        if(display == null || OutgoingMessages.Count < 1)
            return false;

        if(display.isTyping)
            return false;

        display.DisplayText(OutgoingMessages.Dequeue().Messages);
        numberOfMessages = OutgoingMessages.Count;
        return true;
    }

    [Serializable]
    public struct Initiate_Dialogue
    {
        public List<string> Messages;
    }
}
