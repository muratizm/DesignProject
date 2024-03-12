
using UnityEngine;
using Ink.Runtime;


public class Action1 : BaseAction
{
    private ActionManager actionManager;
    private Task1 task1;
    Story currentStory;
    public override void EnterAction()
    {
        Debug.Log("Entering Action1");
        actionManager = ActionManager.Instance;
        currentStory = actionManager.GetCurrentStory();

        task1 = gameObject.AddComponent<Task1>();
        task1.taskName = "Task313131";
        task1.TimeToAchieve = 70f;
        task1.EnterTask();
    }

    public override void UpdateAction()
    {
        if(Input.GetKeyDown(KeyCode.X)){
            actionManager.MakeChoice(0);

        }   
        else if(Input.GetKeyDown(KeyCode.C)){
            actionManager.MakeChoice(1);

        }
        else if(Input.GetKeyDown(KeyCode.V)){
            actionManager.MakeChoice(2);
        }
    }

    public override void ExitAction()
    {
        Debug.Log("Exiting Action1");
        Destroy(this);
    }
}
