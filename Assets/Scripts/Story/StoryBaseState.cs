using UnityEngine;

public abstract class StoryBaseState : MonoBehaviour
{
    public abstract void EnterState();
    public abstract void ExitState();
    public abstract void UpdateState();
}
