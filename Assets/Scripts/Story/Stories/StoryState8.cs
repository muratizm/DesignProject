using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState8 : StoryBaseState
{
    private AudioManager _audioManager;

    public override void EnterState()
    {
        Debug.Log("Entering StoryState8");
        _audioManager = AudioManager.Instance;

        _audioManager.PlayMusic(Constants.Paths.Sounds.MUSIC.STORY1, 0.1f);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState8");
        //delete every object that needed in this state
        
        //storyOperations.DeleteObjects("DemoEnvironment");
    
    }

 
    public override void UpdateState()
    {
        Debug.Log("Updating StoryState8");


    }

}
