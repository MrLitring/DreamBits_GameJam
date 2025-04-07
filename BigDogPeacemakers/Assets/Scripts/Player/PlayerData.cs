
using UnityEngine;

[System.Serializable]
public class PlayerData
{

    public int Health;
    public int MaxHealth;
   

    public PlayerData(int health, int maxHealth)
    {
        Health = health;
        MaxHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Health -= Mathf.Max(Health - damage, 0);
    }

    public void Heal(int heal)
    {
        Health -= Mathf.Min(Health  + heal, MaxHealth);
    }

    // AddItem

    // EquipedItem

}
