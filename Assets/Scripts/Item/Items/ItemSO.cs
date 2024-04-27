using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    public enum Type
    {
        
        Axe,
        Book,
        Paper,
        Task,
        Key,
        Ring,
        Crystal,
        Gold,
        Tool // for example map showing tool etc.

    }



    [SerializeField] private string itemName;
    [SerializeField] private Type itemType;
    [SerializeField] private string itemTag; // addressable tag
    [SerializeField] private bool isBuggy;   
    [SerializeField] private bool isMemories;
    [SerializeField] private bool isConsumable; // If true, the item will be removed from the inventory after use
    [SerializeField] private Sprite itemIcon;


    public string ItemName { get { return itemName; } }
    public Type ItemType { get { return itemType; } }
    public string ItemTag { get { return itemTag; } }
    public bool IsBuggy { get { return isBuggy; } }
    public bool IsMemories { get { return isMemories; } }
    public bool IsConsumable { get { return isConsumable; } }
    public Sprite ItemIcon { get { return itemIcon; } }


    public abstract void Use();

    


}
