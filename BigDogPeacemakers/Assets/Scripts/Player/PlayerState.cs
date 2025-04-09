using UnityEngine;

public class PlayerState : MonoBehaviour
{
    int maxHP = 5;
    public int Health {  get; private set; }
    void Start()
    {
        Health = maxHP;
    }


    public void TakeDamage(int damage)
    {
        Health = Mathf.Max(Health - damage, 0);
        print("Health = " + Health);
    }
     public void HealHealth(int heal)
    {
        Health = Mathf.Min(Health + heal, maxHP);
        print("Health = " + Health);
    }


}
