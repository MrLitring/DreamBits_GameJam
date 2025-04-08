using UnityEngine;
using UnityEngine.UI;

public class DialogueData
{
    public string author { get; private set; }
    public string content { get; private set; }
    public Image image { get; private set; }


    public DialogueData()
    {
        author = "NONE.DialogueData()";
        content = "NONE.DialogueData()";
        image = null;
    }
    public DialogueData(string author, string content, Image image = null)
    {
        this.author = author;
        this.content = content;
        this.image = image;
    }
}

