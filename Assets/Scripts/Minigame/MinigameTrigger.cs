using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    [SerializeField] private MinigameManager.MinigameType minigameType;

    private bool hasTriggered = false;
    public bool HasTriggered { get => hasTriggered; set => hasTriggered = value; }


    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER_TAG) && !hasTriggered)
        {
            MinigameManager.Instance.StartMinigame(minigameType);
            hasTriggered = true;
        }
    }
}
