using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour, INeedToLoad
{
    public bool IsShowDebug = false;

    public List<string> querys = new System.Collections.Generic.List<string>();
    public bool isActive { get; private set; } = false;
    


    [SerializeField] private List<DialogueData> dialogueDatas = new List<DialogueData>();
    [SerializeField] private UIDocument uidocument;

    private VisualElement root;
    private Label contentLabel;
    private Label authorText;
    private VisualElement imageElement;
    
    private bool isValidate { get; set; } = false;
    public int CurrentID = 0;

    void INeedToLoad.NeedToFirstLoad()
    {
        CurrentID = 0;
        SplitRoot();

        if (!isValidate)
        {
            if (IsShowDebug) Debug.Log("DialogueManager: Ќе прошли валидацию, что-то было утер€но из UI");
            return;
        }
        else
            uidocument.gameObject.SetActive(isActive);
        DataLoad();
        UpdateContent();
    }

    public void ShowDialogue(bool active = true)
    {
        if (!isValidate) return;

        uidocument.gameObject.SetActive(active);
        isActive = active;
        UpdateContent();
    }

    public void ShowNextDialogue()
    {
        if (!isValidate || !isActive) return;

        CurrentID++;
        UpdateContent();

    }


    private void UpdateContent()
    {
        if ((CurrentID > -1 && CurrentID < dialogueDatas.Count) == false) { ShowDialogue(false); return; }

        authorText.text = dialogueDatas[CurrentID].author;
        contentLabel.text = dialogueDatas[CurrentID].content;
                
    }

    private void DataLoad()
    {
        foreach (string elem in querys)
            dialogueDatas.AddRange(DialogueDataLoader.GetDatas(elem));

    }

    private void SplitRoot()
    {
        isValidate = false;

        if (uidocument != null)
        {
            try
            {
                root = uidocument.rootVisualElement;

                if (root == null) return;
                contentLabel = root.Q<Label>("content_text");
                authorText = root.Q<Label>("author_text");
                imageElement = root.Q<VisualElement>("image");

                isValidate = true;
            }
            catch
            {
                isValidate = false;
            }
        }

        if (IsShowDebug) Debug.Log("UI загружен - " + isValidate);
    }

}
