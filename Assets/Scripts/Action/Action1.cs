
using UnityEngine;
using Ink.Runtime;


public class Action1 : BaseAction
{
    ActionManager actionManager;
    Story currentStory;
    public override void EnterAction(ActionManager story)
    {
        Debug.Log("Entering Story1");
        actionManager = story;
        currentStory = actionManager.GetCurrentStory();
    }

    public override void UpdateAction(ActionManager story)
    {
        if(Input.GetKeyDown(KeyCode.X)){
            actionManager.MakeChoice(0);
        Debug.Log("x1");

        }   
        else if(Input.GetKeyDown(KeyCode.C)){
            actionManager.MakeChoice(1);
        Debug.Log("c1");

        }
        else if(Input.GetKeyDown(KeyCode.V)){
            actionManager.MakeChoice(2);
        Debug.Log("v1");

        }
    }
}
