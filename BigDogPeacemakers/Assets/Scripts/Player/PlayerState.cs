using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Drawing;

public class PlayerState : MonoBehaviour
{
    int maxHP = 5;
    public float timerInvincibility { get; private set; }
    public float Health {  get; private set; }
    public float timeInvincibility {  get; private set; }

    Animator animator;
    List<SpriteRenderer> visualObjects;
    List<MaterialPropertyBlock> materialProperties;

    GameControllerLocal gameControllerLocal;

    void Awake()
    {
        Health = maxHP;
        timeInvincibility = 1f;
        timerInvincibility = 0f;
        animator = transform.Find("VisContainer").gameObject.GetComponentInChildren<Animator>();
        Transform visual = animator.transform;
        
        visualObjects = new List<SpriteRenderer>();
        materialProperties = new List<MaterialPropertyBlock>();
        for (int i = 0; i < visual.childCount; i++)
        {
            SpriteRenderer sr = visual.GetChild(i).GetComponent<SpriteRenderer>();

            visualObjects.Add(sr);
            
            var mpb = new MaterialPropertyBlock();
            mpb.SetColor("_Color", sr.color);
            sr.SetPropertyBlock(mpb);
            materialProperties.Add(mpb);
        }
    }

    public void UpdateAnimator()
    {
        animator = transform.Find("VisContainer").gameObject.GetComponentInChildren<Animator>();
        Transform visual = animator.transform;

        visualObjects = new List<SpriteRenderer>();
        materialProperties = new List<MaterialPropertyBlock>();
        for (int i = 0; i < visual.childCount; i++)
        {
            SpriteRenderer sr = visual.GetChild(i).GetComponent<SpriteRenderer>();

            visualObjects.Add(sr);

            var mpb = new MaterialPropertyBlock();
            mpb.SetColor("_Color", sr.color);
            sr.SetPropertyBlock(mpb);
            materialProperties.Add(mpb);
        }
    }

   
    

    public void Update()
    {
        if (timerInvincibility > 0f) timerInvincibility -= Time.deltaTime;
        PlayInvincibilityAnim();
        try
        {
            foreach (var item in visualObjects) item.color = item.color;
        }
        catch
        {
            UpdateAnimator();
        }
        
    }

    public void TakeDamage(float damage)
    {
        if (timerInvincibility <= 0f)
        {
            Health = Mathf.Max(Health - damage, 0);
            print("Health = " + Health);
            timerInvincibility = timeInvincibility;
            PaintRed();

            if (Health <= 0)
            {
                StartCoroutine(PlayDeathAnim());   
            }
        }
    }
     public void HealHealth(int heal)
    {
        
        Health = Mathf.Min(Health + heal, maxHP);
        print("Health = " + Health);
        
    }

    IEnumerator PlayDeathAnim()
    {
        animator.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
        
    }

    void PlayInvincibilityAnim()
    {
        if (timerInvincibility > 0f)
        {
            foreach (var sr in visualObjects)
            {
                StartCoroutine(Blinking(sr));
            }  
        }
    }

    void PaintRed()
    {
        try
        {
            for (int i = 0; i < visualObjects.Count; i++)
            {
                visualObjects[i].GetPropertyBlock(materialProperties[i]);
                materialProperties[i].SetColor("_Color", new UnityEngine.Color(visualObjects[i].color.r * 255 - 255 * 0.2f, visualObjects[i].color.g, visualObjects[i].color.b, 1));
                visualObjects[i].SetPropertyBlock(materialProperties[i]);
            }
        }
        catch
        {
            UpdateAnimator();
        }
        
        
    }

    IEnumerator Blinking(SpriteRenderer sr)
    {
        UnityEngine.Color color = sr.color;
        sr.color = new UnityEngine.Color(color.r, color.g, color.b, 0);
        yield return new WaitForSecondsRealtime(0.1f);
        sr.color = new UnityEngine.Color(color.r, color.g, color.b, 1);
        yield return new WaitForSecondsRealtime(0.1f);
    }

}
