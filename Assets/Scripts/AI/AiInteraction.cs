using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ink.Runtime;
using TMPro;
using UnityEngine;


public class AiInteraction : MonoBehaviour
{
    private static AiInteraction instance;
    public static AiInteraction Instance { get { return instance; } }

    [SerializeField][TextArea] public string _textToAi = "Hi im friendly person"; // 792 - this will be deleted


    // question to ai
    private string _ourText = "What do you think we should do in this situation Alduin?";

    // answer from ai
    private string _aiText;

    

    // UI
    [SerializeField] private GameObject aiPanel;
    [SerializeField] private GameObject TMPPanel;
    private TextMeshProUGUI TMP;


    public event Action OnAITurnEnd;




    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;

        TMP = TMPPanel.GetComponentInChildren<TextMeshProUGUI>();
        _textToAi = GetPersonalityLogs();
    }

    public async void AskAI(Story story, Timer timer)
    {

        // show begins
        await AIPanel();

        
    }

    private async Task AIPanel()
    {
        //activate ui
        ActivateAIPanel();

        // get necessary data for our personality
        //_textToAi = GetPersonalityLogs();

        // ask ai 
        Task<string> aiCallTask = AI.Instance.OnApiCall(_textToAi); // start waiting for the response from the ai
        await AskQuestionToAI(); // showing the question to the players (what do you think we should do in this situation Alduin?)
        await PretendAIIsThinking(); // ai is thinking (hmm... let me think about it...)
        _aiText = await aiCallTask; // getting the response from the ai (get the response that we are waiting for few seconds)

        // show ai answer
        await ShowAiAnswer();

        // deactivate ui
        DeactivateAIPanel();

        // notify every listener that ai turn has ended
        OnAITurnEnd?.Invoke();
    }

    private async Task ShowAiAnswer()
    {
        TMP.text = ""; 
        ActivateTMPPanel();

        foreach (char c in _aiText)
        {
            TMP.text += c; // Add the next character
            await Task.Delay(Constants.Durations.WAIT_BETWEEN_LETTERS_MS * 2); 
        }

        await Task.Delay(2000);
    }   

    private async Task AskQuestionToAI()
    {
        TMP.text = ""; 
        ActivateTMPPanel();

        foreach (char c in _ourText)
        {
            TMP.text += c; // Add the next character
            await Task.Delay(Constants.Durations.WAIT_BETWEEN_LETTERS_MS); 
        }

        await Task.Delay(1500);
    }

    private async Task PretendAIIsThinking()
    {
        TMP.text = ""; 

        string thinkingText = "Hmm... Let me think about it...";
        foreach (char c in thinkingText)
        {
            TMP.text += c; // Add the next character
            await Task.Delay(Constants.Durations.WAIT_BETWEEN_LETTERS_MS); 
        }

        await Task.Delay(1000);
    }

    private void ActivateTMPPanel()
    {
        TMPPanel.SetActive(true);
    }

    private void DeactivateTMPPanel()
    {
        TMPPanel.SetActive(false);
    }

    private void ActivateAIPanel()
    {
        aiPanel.SetActive(true);
    }

    private void DeactivateAIPanel()
    {
        aiPanel.SetActive(false);
    }

    private string GetPersonalityLogs(){
        string path = Constants.Paths.DIALOGUE_HISTORY_TEXT;
        string logsOfChoices = "";

        if (System.IO.File.Exists(path))
        {
            logsOfChoices = System.IO.File.ReadAllText(path);
        }
        else
        {
            Debug.LogError("Cannot find the file at " + path);
        }
        return logsOfChoices;
    }

    public void AddLogToPersonalityFile(string log)
    {
        // Remove new line characters from the log string
        string logWithoutNewLines = log.Replace("\n", "").Replace("\r", "");

        // Append the modified log string to the file
        System.IO.File.AppendAllText(Constants.Paths.DIALOGUE_HISTORY_TEXT, logWithoutNewLines);
    }
}
