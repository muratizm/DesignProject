using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState6 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private StoryOperations _storyOperations;
    private AudioManager _audioManager;


    [SerializeField] private ItemSO omnivision;
    private bool hasGivenItem = false;



    public override void EnterState()
    {
        Debug.Log("Entering StoryState6");
        _storyStateManager = StoryStateManager.Instance;
        _storyOperations = StoryOperations.Instance;
        _audioManager = AudioManager.Instance;


                    StoryOperations.Instance.GiveRandomRing();
            StoryOperations.Instance.GiveRandomRing();

            StoryOperations.Instance.GiveRandomRing();

            StoryOperations.Instance.GiveRandomRing();
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState6");
        //delete every object that needed in this state
        
        //storyOperations.DeleteObjects("DemoEnvironment");
    
    }

 
    public override void UpdateState()
    {
        if(!hasGivenItem && !DialogueManager.Instance.IsDialoguePlaying){
            string value = ((Ink.Runtime.StringValue) _storyStateManager.GetStoryState("curstate")).value;
            Debug.Log("Updating StoryState6");
            if( value == "give_item"){
                Debug.Log("Give item");
                int ringCount = InventoryManager.Instance.GetRingCount();
                if(ringCount >= 4){
                    Debug.Log("Give omnivision");
                    Player.Instance.TakeItem(omnivision);
                }
                else{
                    Debug.Log("Give random ring");
                    StoryOperations.Instance.GiveRandomRing();
                }
                hasGivenItem = true;
            }
        }


    }

}
