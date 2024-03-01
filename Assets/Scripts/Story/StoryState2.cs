using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryState2 : StoryBaseState
{
    StoryStateManager storyStateManager;

    
    public override void EnterState()
    {   
        Debug.Log("Entering StoryState2");
        storyStateManager = StoryStateManager.Instance;
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState2");
    }

    public override void UpdateState()
    {
    }
}
