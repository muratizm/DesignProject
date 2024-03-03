using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStateManager : MonoBehaviour
{
    public static StoryStateManager Instance { get; private set; }
    private StoryVariables storyVariables;
    private StoryBaseState currentStoryState;
    private Dictionary<string, StoryBaseState> states;




    [Header("Globals Ink File")]
    [SerializeField] private TextAsset globalsTextFile;



    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            states = new Dictionary<string, StoryBaseState>
            {
                { "StoryState1", new StoryState1() },
                { "StoryState2", new StoryState2() },
                // Add all your states here
            };

            currentStoryState = states["StoryState1"];
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
        
        currentStoryState.EnterState();

    }

    public void UpdateCurrentState()
    {
        //its warned by MakeChoice() method
        currentStoryState.UpdateState();
    }


    public void ChangeState(string newState)
    {
        if (!states.ContainsKey(newState))
        {
            Debug.LogError("State " + newState + " does not exist.");
            return;
        }
        currentStoryState?.ExitState();
        currentStoryState = states[newState];
        currentStoryState.EnterState();
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
