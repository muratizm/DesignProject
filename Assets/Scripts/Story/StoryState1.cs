using System.Collections;
using System.Collections.Generic;
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
    }
}
