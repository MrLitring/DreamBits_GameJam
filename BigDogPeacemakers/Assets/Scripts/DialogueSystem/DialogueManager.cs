using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour, INeedToLoad
{
    public List<string> query = new System.Collections.Generic.List<string>();
    public int CurrentID = 0;
    


    [SerializeField] private List<DialogueData> dialogueDatas = new List<DialogueData>();
    [SerializeField] private UIDocument uidocument;

    private VisualElement root;
    private Label contentLabel;
    private Label authorText;
    private VisualElement imageElement;

    private bool isActive;

    private void SplitRoot()
    {
        if (uidocument == null) return;
        root = uidocument.rootVisualElement;

        if(root == null) return;
        contentLabel = root.Q<Label>("content_text");
        authorText = root.Q<Label>("author_text");
        imageElement = root.Q<VisualElement>("image");
    }


    void INeedToLoad.NeedToFirstLoad()
    {
        if (uidocument == null) return;
        isActive = uidocument.gameObject.activeSelf;
        SplitRoot();
        Load();

        var current = dialogueDatas[CurrentID];

        if (contentLabel != null)
            contentLabel.text = current.content;

        if (authorText != null)
            authorText.text = current.author;

    }

    public void NextText()
    {
        CurrentID++;
        UpdateContent();
    }

    public void Show()
    {
        uidocument.gameObject.SetActive(!isActive);
        UpdateContent();
    }
    public void Show(bool active)
    {
        isActive = active;
        uidocument.gameObject.SetActive(active);
        UpdateContent();
    }

    private void UpdateContent()
    {
        if (!isActive) return;

        CurrentID = Mathf.Clamp(CurrentID, 0, dialogueDatas.Count);

        authorText.text = dialogueDatas[CurrentID].author;
        contentLabel.text = dialogueDatas[CurrentID].content;

        if(CurrentID == dialogueDatas.Count - 1) Show(false);
    }

    private void Load()
    {
        for (int i = 0; i < query.Count; i++)
        {
            string author;
            int[] id;
            Format(query[i], out author,out id);

            for (int j = 0; j < id.Length; j++)
            {
                DialogueData data = new DialogueData(author, id[j].ToString());
                dialogueDatas.Add(data);
            }
        }
    }

    private string Format(string value, out string name, out int[] id)
    {
        value = value.Replace(" ", "");
        name = value.Split(':')[0];

        string[] parts = value.Split(":")[1].Split('-');
        id = new int[parts.Length];
        for (int i = 0; i < parts.Length; i++)
        {
            id[i] = int.Parse(parts[i]);
        }

        return value;
    }

}
