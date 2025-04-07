using System;
using UnityEngine;

[Serializable]
public class ItemData
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ItemType ItemType { get; set; }


    public ItemData(string name, string description, ItemType itemType)
    {
        Name = name;
        Description = description;
        ItemType = itemType;
    }


}
