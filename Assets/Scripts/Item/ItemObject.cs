using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

    [SerializeField]
    private ItemSO item;
    public ItemSO Item { get { return item; } set { item = value; }}


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //InventoryManager.Instance.AddItem(item);
            Player.Instance.TakeItem(item);
            Destroy(gameObject);
        }
    }

}
