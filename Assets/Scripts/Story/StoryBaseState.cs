using UnityEngine;

public abstract class StoryBaseState
{
    public abstract void EnterState(StoryStateManager storyStateManager);
    public abstract void ExitState(StoryStateManager storyStateManager);
    public abstract void UpdateState(StoryStateManager storyStateManager);
}
