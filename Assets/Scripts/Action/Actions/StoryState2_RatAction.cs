
using UnityEngine;
using Ink.Runtime;


public class StoryState2_RatAction : BaseAction
{
    private ActionManager actionManager;
    private Task1 task1;
    Story currentStory;
    public override void EnterAction()
    {
        Debug.Log("Entering StoryState2_RatAction");
        actionManager = ActionManager.Instance;
        currentStory = actionManager.GetCurrentStory();

        /*
        task1 = gameObject.AddComponent<Task1>();
        task1.taskName = "Task313131";
        task1.TimeToAchieve = 70f;
        task1.EnterTask();
        */
    }

    public override void UpdateAction()
    {
        Debug.Log("Updating StoryState2_RatAction");
        if(Input.GetKeyDown(KeyCode.X)){
            actionManager.MakeChoice(1);
            // open minigame to try to kill the rat
        }   
        else if(Input.GetKeyDown(KeyCode.E)){
            Debug.Log("E pressed");
            actionManager.MakeChoice(2);
            // open dialogue 
            StoryOperations.Instance.EnterRatDialogue();
        }
    }

    public override void ExitAction()
    {
        Debug.Log("Exiting Action1");
        Destroy(this);
    }
}
