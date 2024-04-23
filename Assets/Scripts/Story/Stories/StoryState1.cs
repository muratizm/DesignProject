using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState1 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private StoryOperations _storyOperations;
    private AudioManager _audioManager;


    public override void EnterState()
    {
        Debug.Log("Entering StoryState1");
        _storyStateManager = StoryStateManager.Instance;
        _storyOperations = StoryOperations.Instance;
        _audioManager = AudioManager.Instance;

        _audioManager.PlayMusic(Constants.Paths.Sounds.MUSIC.STORY1, 0.1f);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState1");
        //delete every object that needed in this state
        
        //storyOperations.DeleteObjects("DemoEnvironment");
    
    }

 
    public override void UpdateState()
    {
        string value = ((Ink.Runtime.StringValue) _storyStateManager.GetStoryState("curstate")).value;
        Debug.Log("Updating StoryState1");
        if( value == "respect_to_tree"){
            _storyOperations.GetRidOfTheObstacle();
            _storyOperations.AddTask1();

            _storyStateManager.ChangeState("StoryState2");
        }
        else if( value == "attack_to_tree"){
            _storyOperations.BranchFall();
            _storyOperations.AddTask1();

            _storyStateManager.ChangeState("StoryState3");
        }
        else if ( value == "scared_of_adventure"){
            _storyOperations.GetRidOfTheObstacle();
            _storyOperations.GoldenLeafFall();
            _storyOperations.AddTask1();

            _storyStateManager.ChangeState("StoryState2");
        }

    }


}
