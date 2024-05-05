using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StoryState3 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private StoryOperations _storyOperations;
    private bool hasPlayedMinigame = false;
    private bool hasExplainedNextTask = false;
    private bool hasGivenTask = false;

    [SerializeField] private TextAsset explainNextTask;
    [SerializeField] private ItemSO ss3_packetToDeliver;




    public override void EnterState()
    {
        Debug.Log("Entering StoryState3");
        _storyStateManager = StoryStateManager.Instance;
        _storyOperations = StoryOperations.Instance;
    }


    public override void ExitState()
    {
        Debug.Log("Exiting StoryState3");
    }

    public override void UpdateState()
    {
        Debug.Log("Updating StoryState3");
        if (!hasPlayedMinigame)
        {
            string value = ((Ink.Runtime.StringValue) _storyStateManager.GetStoryState("curstate")).value;
            if(value == "minigame"){
                hasPlayedMinigame = true;
                MinigameManager.Instance.StartMinigame(MinigameManager.MinigameType.ClickRush);
                MinigameManager.Instance.OnMinigameFinished += () => PainterMinigameEnded();
            }
            else{
                Debug.Log("Curstate value is not minigame");
            }
        }
        else if (hasPlayedMinigame && !hasExplainedNextTask && MinigameManager.Instance.IsWon){ 
            // minigame is already played
            // there may be conclusion of minigame

            Invoke("ExplainNextTask", 2f);

        }
        else if(hasExplainedNextTask && !hasGivenTask && !DialogueManager.Instance.IsDialoguePlaying){
            // minigame is played and next task is explained
            // do nothing
            Invoke("AddTask4", 3f);
            hasGivenTask = true;
        }



    }

    private void PainterMinigameEnded(){
        if(MinigameManager.Instance.IsWon){
            hasPlayedMinigame = true;
            StoryOperations.Instance.GiveRandomRing();
        }
    }

    private void ExplainNextTask(){
        bool showChoices = false;
        if(!hasExplainedNextTask){
            hasExplainedNextTask = true;
            DialogueManager.Instance.EnterDialogueMode(explainNextTask, showChoices);
            // give packet to player
            // player will have to deliver it to the 'kaşif'
            Player.Instance.TakeItem(ss3_packetToDeliver);
        }
    }

    public void AddTask4(){
        Task4 task4 = gameObject.AddComponent<Task4>();
        task4.taskName = "Task4 Task4 ";
        task4.TimeToAchieve = 570f;
        TasksManager.Instance.AddTask(task4);
    }
}
