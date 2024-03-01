
using UnityEngine;
using Ink.Runtime;


public class Action1 : BaseAction
{
    ActionManager actionManager;
    Story currentStory;
    public override void EnterAction()
    {
        Debug.Log("Entering Action1");
        actionManager = ActionManager.Instance;
        currentStory = actionManager.GetCurrentStory();
    }

    public override void UpdateAction()
    {
        Debug.Log("Updating Action1");
        if(Input.GetKeyDown(KeyCode.X)){
            Debug.Log("choice 1 selected");
            actionManager.MakeChoice(0);

        }   
        else if(Input.GetKeyDown(KeyCode.C)){
            Debug.Log("choice 2 selected");
            actionManager.MakeChoice(1);

        }
        else if(Input.GetKeyDown(KeyCode.V)){
            Debug.Log("choice 3 selected"); 
            actionManager.MakeChoice(2);
        }
    }

    public override void ExitAction()
    {
        Debug.Log("Exiting Action1");
        Destroy(this);
    }
}
