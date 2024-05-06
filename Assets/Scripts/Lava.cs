using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is the player
        if (other.gameObject.tag == "Player")
        {
            // Call the restart game method from the GameManager instance
            GameManager.Instance.RestartGame();
        }
    }
}
