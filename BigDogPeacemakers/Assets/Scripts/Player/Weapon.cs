using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [NonSerialized]public WeaponData weaponData;
    
    public List<GameObject> enemy;
    

    

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponData = GetComponent<WeaponData>();

    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            
            enemy.Add(collision.gameObject);
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        enemy.Remove(collision.gameObject);
    }

}
