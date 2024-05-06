using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState1 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private StoryOperations _storyOperations;
    private AudioManager _audioManager;


    public const string NAME = "StoryState1";
    public const string SS1_OBSTACLE = "SS1_Obstacle";
    public const string SS1_BRANCH = "SS1_Branch";

    [SerializeField] private GameObject ss1_obstacle;
    [SerializeField] private GameObject ss1_branch;
    [SerializeField] private GameObject ss1_goldenLeaf;



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
            GetRidOfTheObstacle();
            _storyOperations.AddTask1();

        }
        else if( value == "attack_to_tree"){
            BranchFall();
            _storyOperations.AddTask1();

        }
        else if ( value == "scared_of_adventure"){
            GetRidOfTheObstacle();
            GoldenLeafFall();
            _storyOperations.AddTask1();

        }

    }


    public void GetRidOfTheObstacle(){
        if(ss1_obstacle != null){
            Debug.Log("Obstacle is falling!");
            ss1_obstacle.GetComponent<Animation>().Play("anim");
        }
    }

    public void BranchFall(){
        if(ss1_branch != null){
            Debug.Log("Branch is falling!");

            ss1_branch.AddComponent<Rigidbody>();
            Rigidbody rb = ss1_branch.GetComponent<Rigidbody>();
            rb.mass = 100;
        }
    }

    public void GoldenLeafFall(){
        if(ss1_goldenLeaf != null){
            Debug.Log("Golden Leaf is falling!");

            Rigidbody rb = ss1_goldenLeaf.AddComponent<Rigidbody>();
        }
    }

}
