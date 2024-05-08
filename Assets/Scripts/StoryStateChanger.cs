using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStateChanger : MonoBehaviour
{    
    private bool canTrigger = true;
    public bool CanTrigger { get => canTrigger; set => canTrigger = value; }

    [SerializeField] private StoryStateManager.StoryState storyState;



    public  void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER_TAG) && canTrigger)
        {
            Debug.Log("Triggered");
            Debug.Log("Changing state to " + storyState);
            StoryStateManager.Instance.ChangeState(storyState);
            canTrigger = false;
        }
    }
    
}
