using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState1 : StoryBaseState
{

    public override void EnterState(StoryStateManager storyStateManager)
    {
        Debug.Log("Entering StoryState1");
    }

    public override void ExitState(StoryStateManager storyStateManager)
    {
        Debug.Log("Exiting StoryState1");
    }

    public override void UpdateState(StoryStateManager storyStateManager)
    {
        Debug.Log("Updating StoryState1");
        if( ((Ink.Runtime.StringValue) StoryStateManager.Instance.GetStoryState("curstate")).value == "osurduk"){
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaa");
            storyStateManager.Getridoftheobstacle();
            storyStateManager.ChangeState("StoryState2");
        }
    }
}
