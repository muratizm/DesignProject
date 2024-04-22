using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryState3 : StoryBaseState
{
    StoryStateManager storyStateManager;

    
    public override void EnterState()
    {   
        Debug.Log("Entering StoryState3");
        storyStateManager = StoryStateManager.Instance;
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState3");
    }

    public override void UpdateState()
    {
    }
}
