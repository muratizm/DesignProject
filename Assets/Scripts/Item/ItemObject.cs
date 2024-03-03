using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeField]
    private Item item;
    public Item Item { get { return item; } }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.Instance.AddItem(item);
            Destroy(gameObject);
        }
    }

}
