using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState0 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private StoryOperations _storyOperations;
    private AudioManager _audioManager;


    public override void EnterState()
    {
        Debug.Log("Entering StoryState0");
        _storyStateManager = StoryStateManager.Instance;
        _storyOperations = StoryOperations.Instance;
        _audioManager = AudioManager.Instance;

        _audioManager.PlayMusic(Constants.Paths.Sounds.MUSIC.HOME, 0.5f);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState1");
        //delete every object that needed in this state
        
        //storyOperations.DeleteObjects("DemoEnvironment");
    
    }

 
    public override void UpdateState()
    {
        
    }


}
