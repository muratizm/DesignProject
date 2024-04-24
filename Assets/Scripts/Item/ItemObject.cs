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
        if (other.CompareTag(Constants.Tags.PLAYER_TAG))
        {
            //InventoryManager.Instance.AddItem(item);
            Player.Instance.TakeItem(item);
            Destroy(gameObject);
        }
    }

}
