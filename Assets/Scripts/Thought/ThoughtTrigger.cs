using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtTrigger : StoryTrigger
{
    public override void TriggerResult()
    {
        ThoughtManager.Instance.EnterThoughtBubble(inkJSON);
    }

}
