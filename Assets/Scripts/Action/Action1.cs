
using UnityEngine;
using Ink.Runtime;


public class Action1 : BaseAction
{
    ActionManager actionManager;
    Story currentStory;
    public override void EnterAction(ActionManager story)
    {
        Debug.Log("Entering Action1");
        actionManager = story;
        currentStory = actionManager.GetCurrentStory();
    }

    public override void UpdateAction(ActionManager story)
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
}
