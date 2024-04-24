using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER_TAG))
        {
            InventoryManager.Instance.AddCrystal();
            Destroy(gameObject);
        }
    }
}

    
