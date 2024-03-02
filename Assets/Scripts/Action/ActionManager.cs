using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;
using Ink.UnityIntegration;
using System.IO;

public class ActionManager : MonoBehaviour
{

    public static ActionManager Instance  { get; private set; }
    private StoryVariables storyVariables; 

    private Story currentStory;
    private BaseAction currentAction;
    private BaseAction noAction = new NoAction();

    public bool actionIsPlaying {get; private set;}
    


    [Header("Action UI")]
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private float textSize = 31.0f;


    
    [Header("Choices")]
    [SerializeField] private GameObject[] actionChoices;
    List<Choice> currentActionChoices;
    private TextMeshProUGUI[] actionChoicesText;
    private bool choiceMade = false;
    [SerializeField] private Slider timerSlider;
    
    
    

    void Awake()
    {
        if(Instance != null){
            Debug.LogError("found more than one DialogueManager.");
        }
        Instance = this;
        actionText.fontSize = textSize;

    }

    void Start()
    {
        actionIsPlaying = false;
        actionPanel.SetActive(false);

        storyVariables = StoryStateManager.Instance.GetStoryVariables();
        
        actionChoicesText = new TextMeshProUGUI[actionChoices.Length];
        int index = 0;
        foreach (GameObject choice in actionChoices){
            actionChoicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++; 
        }
    }

    void Update()
    {
        currentAction?.UpdateAction();
    }
    
    public void EnterActionMode(TextAsset inkJSON, BaseAction actionScript){
        currentStory = new Story(inkJSON.text);
        actionIsPlaying = true;
        actionPanel.SetActive(true);

        storyVariables.StartListening(currentStory);        
        ContinueStory();
        currentAction = actionScript;
        currentAction.EnterAction();
    }


    public StoryVariables GetStoryVariables(){
        return storyVariables;
    }


    

    public void ContinueStory(){
        if(currentStory.canContinue)
        {   
            Display(actionText, currentStory.Continue());
        }
        else{
            ExitDialogueMode();
        }
    }

    
    private void Display(TMP_Text tmp_text, string content){
        tmp_text.text = content;
        
        File.AppendAllText(Constants.Paths.DIALOGUE_HISTORY_TEXT, "========= situation : " + content + "\n");
        choiceMade = false;
        

        if(currentStory.currentTags.Contains("end3"))
        {
            Invoke("ExitDialogueMode", 3);
        }

        DisplayChoices();
    }



    
    private void DisplayChoices(){
        Debug.Log("displaying choices");
        StartCoroutine(StartCountdown(3f));

        currentActionChoices = currentStory.currentChoices;

        if(currentActionChoices.Count > actionChoices.Length){
            Debug.LogError("more choices were given than the ui can support. there is not enough space for this choices. number of choices given: " + currentActionChoices);
        }

        //reveal choices that are given in story
        int index = 0;
        foreach (Choice choice in currentActionChoices){
            actionChoicesText[index].text = choice.text;
            actionChoices[index].gameObject.SetActive(true);
            File.AppendAllText(Constants.Paths.DIALOGUE_HISTORY_TEXT, "========= given choice" + index + " : " + actionChoicesText[index].text + "\n");
            index++;
        }

        //hide remaining choices
        for(int i = index; i < actionChoices.Length; i++){
            actionChoices[i].gameObject.SetActive(false);
        }

    }
    

    IEnumerator StartCountdown(float duration)
    {
        float timePassed = 0;

        while (timePassed < duration)
        {
            // If a choice has been made, stop the countdown
            if (choiceMade)
            {
                timerSlider.value = 0; // Reset the slider
                yield break;
            }

            timePassed += Time.deltaTime;
            timerSlider.value = 1 - (timePassed / duration); // Update the slider

            yield return null;
        }

        if (!choiceMade && currentActionChoices.Count > 0)
        {
            Debug.Log("No choice made, making random choice");
            //ai implementation will go here
            MakeChoice(UnityEngine.Random.Range(0, currentActionChoices.Count));
        }

        timerSlider.value = 0; // Reset the slider
    }

    public void MakeChoice(int index){
        // just made a choice
        // exit the action
        // noaction will be entered

        currentAction.ExitAction();
        currentAction = noAction;
        currentAction.EnterAction();

        currentStory.ChooseChoiceIndex(index);

        File.AppendAllText(Constants.Paths.DIALOGUE_HISTORY_TEXT, "=========choice made: " + actionChoicesText[index].text + "\n");
        choiceMade = true;

        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        storyVariables.StartListening(currentStory);
        actionIsPlaying = false;
        actionPanel.SetActive(false);
        actionText.text  = "";
    }

    public Story GetCurrentStory(){
        return currentStory;
    }

    public Ink.Runtime.Object GetStoryState(string variableName){
        Ink.Runtime.Object variableValue = null;
        storyVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableValue == null){
            Debug.LogWarning("ink variable was found to be null: " + variableName );
        }
        return variableValue;
    }
}
