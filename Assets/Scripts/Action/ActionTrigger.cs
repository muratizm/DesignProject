using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ActionTrigger : StoryTrigger
{
    public override void TriggerResult()
    {
        ActionManager.Instance.EnterActionMode(inkJSON, GetComponent<BaseAction>());
    }
}
