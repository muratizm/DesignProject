using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryState3 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private StoryOperations _storyOperations;
    private bool hasPlayedMinigame = false;

    public override void EnterState()
    {
        Debug.Log("Entering StoryState3");
        _storyStateManager = StoryStateManager.Instance;
        _storyOperations = StoryOperations.Instance;
    }


    public override void ExitState()
    {
        Debug.Log("Exiting StoryState3");
    }

    public override void UpdateState()
    {
        if (!hasPlayedMinigame)
        {
            string value = ((Ink.Runtime.StringValue) _storyStateManager.GetStoryState("curstate")).value;
            Debug.Log("Updating StoryState3");
            if( value == "minigame"){
                MinigameManager.Instance.StartMinigame(MinigameManager.MinigameType.ClickRush);
                MinigameManager.Instance.OnMinigameFinished += () => PainterMinigameEnded();
                
            }
            else{
                Debug.Log("Curstate value is not minigame");
            }
        }
        else{
            // minigame is already played
            // there may be conclusion of minigame

        }



    }

    private void PainterMinigameEnded(){
        if(MinigameManager.Instance.IsWon){
            hasPlayedMinigame = true;
            StoryOperations.Instance.GiveRandomRing();
        }
        else{
            hasPlayedMinigame = false;
        }
    }
}
