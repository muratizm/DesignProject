using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryStateManager : MonoBehaviour
{
    public static StoryStateManager Instance { get; private set; }

    private StoryBaseState currentState;
    private StoryBaseState startState;
    private Dictionary<string, StoryBaseState> states;

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
            startState = states["StoryState1"];
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
        currentState = startState;
        currentState.EnterState(this);


    }

    void Update()
    {
        currentState.UpdateState(this);
    }


    public void ChangeState(string newState)
    {
        if (!states.ContainsKey(newState))
        {
            Debug.LogError("State " + newState + " does not exist.");
            return;
        }

        if (currentState != null)
        {
            currentState.ExitState(this);
        }
        currentState = states[newState];
        currentState.EnterState(this);
    }



}
