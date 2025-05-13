using UnityEngine;
using System.Collections.Generic;

public class SpawnerBonus : MonoBehaviour
{
    public List<GameObject> bonus;
    GameObject currentBonus;

    float timer = 0;
    public float cooldown = 5f;
    bool isExist = false;
    void Start()
    {
        

        CreateBonus();
    }

    // Update is called once per frame
    void Update()
    {

        if (currentBonus == null && !isExist)
        {
            timer = cooldown;
            isExist = true;
        }
        if (timer <= 0 && isExist)
        {
            CreateBonus();
            isExist = false;
        }
        if (timer > 0) timer -= Time.deltaTime;
    }

    void CreateBonus()
    {
        if (bonus.Count > 1)
        {
            currentBonus = GameObject.Instantiate(bonus[Random.Range(0, bonus.Count)], transform);
        }
        else
        {
            currentBonus = GameObject.Instantiate(bonus[0], transform);
        }

    }
}
