using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField] GameObject DialoguePanel;
    [SerializeField] Text DialogueText;
    public float textSpeed, textLifeSpan;

    Queue<string> TextQueue = new Queue<string>();
    string currentText;
    [HideInInspector] public bool isTyping;
    int textcount = 0;

    void Awake()
    {
        DialoguePanel.SetActive(false);
    }

    public void DisplayText(string text)
    {
        if(text.Length < 1)
            return;

        DialoguePanel.SetActive(true);
        TextQueue.Enqueue(text);

        if(!isTyping)
            BeginTyping();
    }

    public void DisplayText(List<string> texts)
    {
        DialoguePanel.SetActive(true);

        for (int i = 0; i < texts.Count; i++)
        {
            if(texts[i].Length > 0)
                TextQueue.Enqueue(texts[i]);
        }

        if(TextQueue.Count < 1)
            return;

        if(!isTyping)
            BeginTyping();
    }

    void BeginTyping()
    {
        isTyping = true;
        currentText = TextQueue.Dequeue();
        DialogueText.text = "";
        textcount = 0;

        InvokeRepeating("TextTyping", 0, textSpeed);
    }

    void EndTyping()
    {
        DialogueText.text = "";
        DialoguePanel.SetActive(false);
        isTyping = false;
    }

    void CheckQueue()
    {
        if(TextQueue.Count > 0)
            Invoke("BeginTyping", textLifeSpan);
        else
            Invoke("EndTyping", textLifeSpan);
    }

    void TextTyping()
    {
        string visibleText = DialogueText.text;
        if(visibleText.Equals(currentText))
        {
            CancelInvoke("TextTyping");
            CheckQueue();
            return;
        }

        visibleText += currentText[textcount];
        textcount ++;
        DialogueText.text = visibleText;
    }
}
