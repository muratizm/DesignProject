using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState1 : StoryBaseState
{
    StoryStateManager storyStateManager;
    StoryOperations storyOperations;


    public override void EnterState()
    {
        Debug.Log("Entering StoryState1");
        storyStateManager = StoryStateManager.Instance;
        storyOperations = StoryOperations.Instance;
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState1");
    }
 
    public override void UpdateState()
    {
        
        if( ((Ink.Runtime.StringValue) storyStateManager.GetStoryState("curstate")).value == "osurduk"){
            storyOperations.GetRidOfTheObstacle();
            storyStateManager.ChangeState("StoryState2");
        }
    }
}
