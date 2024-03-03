using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    private string itemName;
    public string ItemName { get { return itemName; } }
    [SerializeField]
    private bool isConsumable; // If true, the item will be removed from the inventory after use
    public bool IsConsumable { get { return isConsumable; } }
    [SerializeField]
    private Sprite itemIcon;
    public Sprite ItemIcon { get { return itemIcon; } }


    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Use() 
    {
        // Use the item
        Debug.Log("Using " + itemName);
    }

    


}
