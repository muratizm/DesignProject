using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryStateManager : MonoBehaviour
{
    public static StoryStateManager Instance { get; private set; }
    private StoryVariables storyVariables;
    private StoryBaseState currentStoryState;

    [System.Serializable]
    public struct StatePair
    {
        public StoryState State;
        public StoryBaseState StateScript;
    }

    public enum StoryState
    {
        StoryState0,
        StoryState1,
        StoryState2,
        StoryState3,
        StoryState4,
        StoryState5,
        StoryState6,
        StoryState7,
        StoryState8,
        StoryState9
    }
    

    [SerializeField] private List<StatePair> statePairs;



    [Header("Globals Ink File")]
    [SerializeField] private TextAsset globalsTextFile;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            storyVariables = new StoryVariables(globalsTextFile);

        }
        else
        {
            Debug.LogError("found more than one DialogueManager.");
            Destroy(gameObject);
        }


    }

    void Start()
    {
        

            if (SceneManager.GetActiveScene().name == "HomeScene")
            {
                ChangeState(StoryState.StoryState0);
            }


    }

    public void UpdateCurrentState()
    {
        //its warned by MakeChoice() method
        currentStoryState.UpdateState();
    }


    public void ChangeState(StoryState newState)
    {
        StatePair pair = statePairs.Find(p => p.State == newState);
        Debug.Log("old state: " + currentStoryState);
        Debug.Log("Changing state to " + newState);
        Debug.Log("Pair: " + pair);
        Debug.Log("new state: " + pair.StateScript);
        if (!pair.Equals(default(KeyValuePair)))
        {
            Debug.Log("exiting : " + currentStoryState);
            currentStoryState?.ExitState();
            currentStoryState = pair.StateScript;
            Debug.Log("entering : " + currentStoryState);
            currentStoryState.EnterState();
        }
        else{
            Debug.LogError("State " + newState + " does not exist.");
            return;
        }

    }


    public Ink.Runtime.Object GetStoryState(string variableName){
        Ink.Runtime.Object variableValue = null;
        storyVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableValue == null){
            Debug.LogWarning("ink variable was found to be null: " + variableName );
        }
        return variableValue;
    }

    public StoryVariables GetStoryVariables(){
        return storyVariables;
    }

}
