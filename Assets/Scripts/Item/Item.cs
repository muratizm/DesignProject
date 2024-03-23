using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private string itemTag; // addressable tag
    [SerializeField] private bool isBuggy;   
    [SerializeField] private bool isConsumable; // If true, the item will be removed from the inventory after use
    [SerializeField] private Sprite itemIcon;


    public string ItemName { get { return itemName; } }
    public string ItemTag { get { return itemTag; } }
    public bool IsBuggy { get { return isBuggy; } }
    public bool IsConsumable { get { return isConsumable; } }
    public Sprite ItemIcon { get { return itemIcon; } }


    public abstract void Use();

    


}
