using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ring", menuName = "Inventory/Item/Ring")]
public class Ring : Item
{
    public override void Use()
    {
        // Implement the logic to show us everywhere
        Debug.Log("Ring using!");
        
    }
}