using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : StoryTrigger
{
    public override void TriggerResult()
    {
        DialogueManager.Instance.EnterDialogueMode(inkJSON);
    }

}
