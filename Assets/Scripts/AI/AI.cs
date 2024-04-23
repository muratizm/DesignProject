using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class AI : MonoBehaviour
{
    private static AI instance;
    public static AI Instance { get { return instance; } }


    // AI's story
    [SerializeField] private TextAsset _ourQuestionJson;
    [SerializeField] private TextAsset[] _aiChoicesJson;
    private Story _ourQuestionStory;
    private Story[] _aiChoicesStories;
    

    // UI
    [SerializeField] private GameObject aiPanel;
    private TextMeshProUGUI _aiTMP;


    public event Action OnAITurnEnd;

    private string _ourText;
    private string _aiText;



    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        _aiTMP = aiPanel.GetComponentInChildren<TextMeshProUGUI>();
    }

    public int AskAI(Story story, Timer timer)
    {
        // init
        int selectedByAI = 0;
        InitializeStory(); // must reinitialize every time

        // ask AI
        selectedByAI = UnityEngine.Random.Range(0, story.currentChoices.Count);

        // show AI's choice
        AIPanel(selectedByAI, timer);

        
        // return AI's choice
        Debug.Log("AI selected: " + selectedByAI);
        return selectedByAI;
    }

    private async void AIPanel(int selectedByAI, Timer timer)
    {
        ActivateAIPanel();

        ThoughtManager.Instance.EnterThoughtBubble(_ourQuestionJson);
        
        await Task.Delay(Constants.Durations.AI_DIALOGUE_WAIT_MS);

        ThoughtManager.Instance.EnterThoughtBubble(_aiChoicesJson[selectedByAI]);

        await Task.Delay(Constants.Durations.AI_DIALOGUE_WAIT_MS);

        DeactivateAIPanel();
        OnAITurnEnd?.Invoke();
    }
    

    private void InitializeStory(){
        _ourQuestionStory = new Story(_ourQuestionJson.text);
        _aiChoicesStories = new Story[_aiChoicesJson.Length];
        for (int i = 0; i < _aiChoicesJson.Length; i++)
        {
            _aiChoicesStories[i] = new Story(_aiChoicesJson[i].text);
        }
    }


    private void ActivateAIPanel()
    {
        aiPanel.SetActive(true);
    }


    private void DeactivateAIPanel()
    {
        aiPanel.SetActive(false);
    }
}
