using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
                { "StoryState0", new StoryState0() },
                { "StoryState1", new StoryState1() },
                { "StoryState2", new StoryState2() },
                // Add all your states here
            };


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
                ChangeState("StoryState0");
            }
            else
            {
                ChangeState("StoryState1"); //  burda playerprefs falan bi şekilde
                // şuan içinde bulundugum stroy state i alıp ona göre değiştirme yapılacak
            }
    }

    public void UpdateCurrentState()
    {
        //its warned by MakeChoice() method
        Debug.Log("Current State: " + currentStoryState);
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
