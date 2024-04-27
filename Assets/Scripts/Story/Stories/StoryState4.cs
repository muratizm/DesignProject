using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class StoryState4 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private StoryOperations _storyOperations;
    private AudioManager _audioManager;

    bool canGiveAxe = false;
    bool hasCutTree = false;
    bool hasPoisoned = false;

    [SerializeField] private TextAsset ss4_rewardDialogueJSON;




    public override void EnterState()
    {
        Debug.Log("Entering StoryState4");
        _storyStateManager = StoryStateManager.Instance;
        _storyOperations = StoryOperations.Instance;
        _audioManager = AudioManager.Instance;

        _audioManager.PlayMusic(Constants.Paths.Sounds.MUSIC.STORY1, 0.1f);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState4");
        //delete every object that needed in this state
        
        //storyOperations.DeleteObjects("DemoEnvironment");
    }

 
    public override void UpdateState()
    {
        //TODO: stop the cutting animation, pass to idle here


        string value = ((Ink.Runtime.StringValue) _storyStateManager.GetStoryState("curstate")).value;

        if(!hasCutTree && value == "give_axe"){
            //TODO: start the cutting animation again
            canGiveAxe = InventoryManager.Instance.RemoveSpecificTypeFromInventory(ItemSO.Type.Axe);
            hasCutTree = true;
            ConclusionsOfCuttingTree();
            return;
        }

        if(!hasPoisoned && (value == "protect_tree" || value == "donotcare")){
            hasPoisoned = true;
            Invoke("PoisionFromTree", 3f);
            return;
        }

        //TODO: i need to give reward to player when the reward dialogue ends
        // but dont know how to check if reward dialogue is ended
        // storystate2 3 tefalan yapmoş olabilrim bak
        

    }

    private async void ConclusionsOfCuttingTree(){
        //wait for 2 seconds
        await Task.Delay(1000);

        //then, play tree falling animation and sound
        //TODO: stop the cutting animation, pass to idle here
        StartTreeFalling();
        await Task.Delay(2000);

        //and then, start reward dialogue
        StartRewardDialogue();
    }

    private void StartTreeFalling(){
        //animation
        Debug.Log("Tree falling animation");
        //sound
        Debug.Log("Tree falling sound");
    }

    private void StartRewardDialogue(){
        DialogueManager.Instance.EnterDialogueMode(ss4_rewardDialogueJSON, false);
    }


    private void PoisionFromTree()
    {
        //TODO: injure player

        //TODO: maybe coaching sound in the future
        float timeToRecover = 20f;
        Player.Instance.Injure(timeToRecover);
    }
}
