using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Book", menuName = "Inventory/Item/Book")]
public class BookSO : ItemSO
{
    [SerializeField] private Sprite[] pages;
    public override void Use()
    {
        // Implement the logic to show us everywhere
        if (ItemTag == "GrandfathersBook"){
            ItemOperations.Instance.HomeSceneOpenBook(pages);
        }
        else{
            ItemOperations.Instance.UseBookItem(pages);        
        }
    }
}