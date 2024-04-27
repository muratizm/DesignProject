using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState6 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private StoryOperations _storyOperations;
    private AudioManager _audioManager;


    [SerializeField] private GameObject ss4_zort;



    public override void EnterState()
    {
        Debug.Log("Entering StoryState6");
        _storyStateManager = StoryStateManager.Instance;
        _storyOperations = StoryOperations.Instance;
        _audioManager = AudioManager.Instance;

        _audioManager.PlayMusic(Constants.Paths.Sounds.MUSIC.STORY1, 0.1f);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState6");
        //delete every object that needed in this state
        
        //storyOperations.DeleteObjects("DemoEnvironment");
    
    }

 
    public override void UpdateState()
    {
        string value = ((Ink.Runtime.StringValue) _storyStateManager.GetStoryState("curstate")).value;
        Debug.Log("Updating StoryState6");
        if( value == "blabla"){
            Invoke("DoSomething", 2f);

        }

    }


    private void DoSomething()
    {
        //do something
    }
}
