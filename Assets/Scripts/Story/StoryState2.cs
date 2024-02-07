using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryState2 : StoryBaseState
{
    public override void EnterState(StoryStateManager storyStateManager)
    {
        Debug.Log("Entering StoryState2");
    }

    public override void ExitState(StoryStateManager storyStateManager)
    {
        Debug.Log("Exiting StoryState2");
    }

    public override void UpdateState(StoryStateManager storyStateManager)
    {
    }
}
