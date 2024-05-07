using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class StoryState5 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private StoryOperations _storyOperations;
    private AudioManager _audioManager;


    private bool hasPlayedMinigame = false;
    private bool hasAfterDialogue = false;
    private bool hasChoosedPath = false;
    [SerializeField] private TextAsset ss5_afterDialogue;
    [SerializeField] private ItemSO ss5_wizardNote;
    [SerializeField] private Animator pathA;
    [SerializeField] private Animator pathB;
    [SerializeField] private Animator pathC;




    public override void EnterState()
    {
        Debug.Log("Entering StoryState5");
        _storyStateManager = StoryStateManager.Instance;
        _storyOperations = StoryOperations.Instance;
        _audioManager = AudioManager.Instance;

        _audioManager.PlayMusic(Constants.Paths.Sounds.MUSIC.STORY1, 0.1f);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState5");
        //delete every object that needed in this state
        
        //storyOperations.DeleteObjects("DemoEnvironment");
    
    }

 
    public override void UpdateState()
    {
        Debug.Log("Updating StoryState5");
        if (!hasPlayedMinigame)
        {
            Debug.Log("Playing minigame");
            string value = ((Ink.Runtime.StringValue) _storyStateManager.GetStoryState("curstate")).value;
            if( value == "minigame"){
                hasPlayedMinigame = true;
                MinigameManager.Instance.StartMinigame(MinigameManager.MinigameType.PasswordQuiz);
                MinigameManager.Instance.OnMinigameFinished += () => ExplorerMinigameEnded();
            }
            else{
                Debug.Log("Curstate value is not minigame");
            }
        }
        else if (hasPlayedMinigame && !hasAfterDialogue && MinigameManager.Instance.IsWon){
            // minigame is already played
            // there may be conclusion of minigame

            Invoke("DialogueAfterPackage", 1f);
        }
        else if(hasPlayedMinigame && hasAfterDialogue && !hasChoosedPath && !DialogueManager.Instance.IsDialoguePlaying){
            // after dialogue is ended
            // player should choose a path
            hasChoosedPath = true;
            string value = ((Ink.Runtime.StringValue) _storyStateManager.GetStoryState("curstate")).value;
            if( value == "a"){
                pathA.SetTrigger("OpenPath");
                // give not itemi
                Player.Instance.TakeItem(ss5_wizardNote);
            }
            else if(value == "b"){
                pathB.SetTrigger("OpenPath");
            }
            else if(value == "c"){
                pathC.SetTrigger("OpenPath");
            }
            else{
                Debug.LogError("Curstate value is not a, b or c");
            }
        }

    }

    private void ExplorerMinigameEnded(){
        if(MinigameManager.Instance.IsWon){
            Debug.Log("Explorer minigame is won");
            hasPlayedMinigame = true;
            GivePackageToExplorer();
        }
    }

    private void DialogueAfterPackage()
    {
        DialogueManager.Instance.EnterDialogueMode(ss5_afterDialogue);
        hasAfterDialogue = true;
    }

    private void GivePackageToExplorer()
    {
        InventoryManager.Instance.RemoveSpecificTypeFromInventory(ItemSO.Type.Package);
    }
}
