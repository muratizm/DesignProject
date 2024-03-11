using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] public string itemTag;


    [SerializeField] private bool isConsumable; // If true, the item will be removed from the inventory after use
    public bool IsConsumable { get { return isConsumable; } }
    

    [SerializeField] private Sprite itemIcon;
    public Sprite ItemIcon { get { return itemIcon; } }


    public abstract void Use();

    


}
