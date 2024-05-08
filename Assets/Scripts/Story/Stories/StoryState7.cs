using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState7 : StoryBaseState
{
    private AudioManager _audioManager;


    [SerializeField] private Animator ss7_path1;
    [SerializeField] private Animator ss7_path2;
    private bool hasMinigameTriggered = false;
    private bool hasMinigamePlayed = false;
    private bool hasPath1Opened = false;



    public override void EnterState()
    {
        Debug.Log("Entering StoryState7");
        _audioManager = AudioManager.Instance;

        _audioManager.PlayMusic(Constants.Paths.Sounds.MUSIC.STORY1, 0.1f);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState7");
        //delete every object that needed in this state
        
        //storyOperations.DeleteObjects("DemoEnvironment");
    
    }

 
    public override void UpdateState()
    {
        Debug.Log("Updating StoryState7");
        if(hasMinigameTriggered && !hasMinigamePlayed){
            hasMinigamePlayed = true;
            MinigameManager.Instance.StartMinigame(MinigameManager.MinigameType.ClickRush);
        }
        else if(hasMinigameTriggered && hasMinigamePlayed && MinigameManager.Instance.IsWon){
            Debug.Log("Minigame is won");
            OpenPath2();
        }


    }


    public void TriggerMinigame()
    {
        hasMinigameTriggered = true;
        this.UpdateState();
    }

    public void TriggerPath1()
    {
        if(!hasPath1Opened && InventoryManager.Instance.GetRingCount() >= 4){
            hasPath1Opened = true;
            OpenPath1();
        }
    }

    private void OpenPath1(){
        ss7_path1.SetTrigger("OpenPath");
    }
    
    private void OpenPath2(){
        ss7_path2.SetTrigger("OpenPath");
    }
}
