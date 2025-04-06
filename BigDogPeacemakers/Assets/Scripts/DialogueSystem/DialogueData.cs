using UnityEngine;
using UnityEngine.UI;

public class DialogueData
{
    public string author { get; private set; }
    public string content { get; private set; }
    public Image Image { get; private set; }

    public DialogueData(string author, string content, Image image = null)
    {
        this.author = author;
        this.content = content;
        Image = image;
    }
}

