using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItemTrigger : MonoBehaviour
{
    [SerializeField] private MinigameManager.MinigameType minigameType;
    [SerializeField] private ItemSO itemToGain;

    private bool hasTriggered = false;
    public bool HasTriggered { get => hasTriggered; set => hasTriggered = value; }


    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER_TAG) && !hasTriggered)
        {
            // if you dont want to check hasTriggered, you can remove it from the if statement
            // but be careful with the ~curstate in the ink story
            // you must reset it because old value from older choices will be stored
            Player.Instance.TakeItem(itemToGain, minigameType);
            hasTriggered = true;
        }
    }

}
