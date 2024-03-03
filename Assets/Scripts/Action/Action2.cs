
using UnityEngine;
using Ink.Runtime;


public class Action2 : BaseAction
{
    private ActionManager actionManager;
    private Story currentStory;
    public override void EnterAction()
    {
        Debug.Log("Entering Action2");
        actionManager = ActionManager.Instance;
        currentStory = actionManager.GetCurrentStory();
    }

    public override void UpdateAction()
    {
        Debug.Log("updating action2");
        if(Input.GetKeyDown(KeyCode.Y)){
            actionManager.MakeChoice(0);
        }   
    }

}
