
using UnityEngine;
using Ink.Runtime;


public class NoAction : BaseAction
{
    private ActionManager actionManager = ActionManager.Instance;
    public override void EnterAction()
    {
        Debug.Log("Entering NoAction");
    }


    public override void ExitAction()
    {
        Debug.Log("Exiting NoAction");
        Destroy(this);
    }
}
