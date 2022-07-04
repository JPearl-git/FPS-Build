using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Command_Manager : MonoBehaviour
{
    [SerializeField] List<CustomCommand> commands = new List<CustomCommand>();
    public string RemoveCommands(string text)
    {
        if(text.IndexOf('{') == -1)
            return text;

        string writableText = "";
        bool isCommand = false;

        for(int i = 0; i < text.Length; i++)
        {
            if(text[i] == '{')
            {
                isCommand = true;
                continue;
            }
            if(text[i] == '}')
            {
                isCommand = false;
                continue;
            }

            if(!isCommand)
                writableText += text[i];
        }

        return writableText;
    }

    public int RunCommand(string text, int n)
    {
        string sub = text.Substring(n);
        int m = sub.IndexOf('}');
        
        // Check if command has {}
        if(m == -1)
            return sub.Length;

        // Isolate the Command
        sub = sub.Substring(0, m).Replace("{","").Replace("}","");

        // Find Command
        var cmd = commands.Find(x => x.name == sub);
        if(cmd.command != null)
            cmd.command.Invoke();

        return sub.Length + 2;
    }
}

[Serializable]
public struct CustomCommand
{
    public string name;
    public UnityEvent command;
}
