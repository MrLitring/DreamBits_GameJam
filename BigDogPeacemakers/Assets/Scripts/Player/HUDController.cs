using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Collections;
using UnityEngine.U2D;
public class HUDController : MonoBehaviour
{
    public VisualTreeAsset heartVTA;
    public SpriteAtlas sprites;
    

    VisualElement heartVE;
    UIDocument uiDocumentHUD;
    PlayerState playerState;
    VisualElement rootHUD;
    VisualElement container;
    int heartsCount;
    List<VisualElement> hearts;
    
    void Start()
    {
        hearts = new List<VisualElement>();
        playerState = GetComponent<PlayerState>();
        uiDocumentHUD = GetComponent<UIDocument>();
        rootHUD = uiDocumentHUD.rootVisualElement;
        container = rootHUD.Q<VisualElement>("container");
        heartVE = heartVTA.Instantiate(); //.rootVisualElement.Q<VisualElement>("heart");
        heartsCount = (int)playerState.Health;
    }

    // Update is called once per frame
    void Update()
    {
        heartsCount = (int)playerState.Health;
        UpdateHeartsCount();
    }

    void UpdateHeartsCount()
    {
        int currentHeartsCount = container.childCount;
        if (currentHeartsCount != heartsCount)
        {
            if (currentHeartsCount < heartsCount)
            {
                VisualElement temp = new VisualElement();
                temp = heartVTA.Instantiate();
                hearts.Add(temp);
                container.Add(temp);
                StartCoroutine(Apear(temp));
            }
            else
            {
                StartCoroutine(Disapear(container.ElementAt(currentHeartsCount - 1)));
                
                
            }
        }
    }

    IEnumerator Apear(VisualElement heart)
    {
        
        for (int i = sprites.spriteCount - 1; i >= 0; i--)
        {
            heart.style.backgroundImage = new StyleBackground(sprites.GetSprite($"Sprite-0004-Sheet_{i}"));
            yield return new WaitForSecondsRealtime(0.1f);
        }

    }
    IEnumerator Disapear(VisualElement heart)
    {
        for (int i = 0;i < sprites.spriteCount; i++)
        {
            heart.style.backgroundImage = new StyleBackground(sprites.GetSprite($"Sprite-0004-Sheet_{i}"));
            yield return new WaitForSecondsRealtime(0.05f);
        }
        //container.Remove(heart);
    }
}
