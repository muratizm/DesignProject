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
    private GameManager gameManager;
    private StoryStateManager storyStateManager;
    public static ActionManager Instance  { get; private set; }
    private StoryVariables storyVariables; 

    private Story currentActionStory;
    private BaseAction currentActionScript;
    private NoAction noAction;

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
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Found more than one ActionManager. Destroying the duplicate.");
            Destroy(gameObject);
        }
        Instance = this;
        actionText.fontSize = textSize;
        noAction = gameObject.AddComponent<NoAction>();
    }

    void Start()
    {
        storyStateManager = StoryStateManager.Instance;
        gameManager = GameManager.Instance;

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
        if(gameManager.IsGamePaused){return;} //if game is paused, dont do anything

        if (!currentActionScript){return;} //if no action is playing, dont do anything

        currentActionScript.UpdateAction();
    }

    
    public void EnterActionMode(TextAsset inkJSON, BaseAction actionScript){
        Debug.Log("entering action mode");

        currentActionStory = new Story(inkJSON.text);
        Debug.Log("story: " + currentActionStory);
        Debug.Log("actionScript: " + actionScript);
        actionIsPlaying = true;
        actionPanel.SetActive(true);

        storyVariables.StartListening(currentActionStory);        
        ContinueStory();
        currentActionScript = actionScript;
        Debug.Log("ac: " + currentActionScript);
        currentActionScript.EnterAction();
    }


    public StoryVariables GetStoryVariables(){
        return storyVariables;
    }


    

    public void ContinueStory(){
        if(currentActionStory.canContinue)
        {   
            Display(actionText, currentActionStory.Continue());
        }
        else{
            ExitDialogueMode();
        }
    }

    
    private void Display(TMP_Text tmp_text, string content){
        tmp_text.text = content;
        
        AiInteraction.Instance.AddLogToPersonalityFile("=> situation : " + content + "---");
        choiceMade = false;
        

        if(currentActionStory.currentTags.Contains("end3"))
        {
            Invoke("ExitDialogueMode", 3);
        }

        DisplayChoices();
    }



    
    private void DisplayChoices(){
        Debug.Log("displaying choices");
        StartCoroutine(StartCountdown(3f));

        currentActionChoices = currentActionStory.currentChoices;

        if(currentActionChoices.Count > actionChoices.Length){
            Debug.LogError("more choices were given than the ui can support. there is not enough space for this choices. number of choices given: " + currentActionChoices);
        }

        //reveal choices that are given in story
        int index = 0;
        foreach (Choice choice in currentActionChoices){
            actionChoicesText[index].text = choice.text;
            actionChoices[index].gameObject.SetActive(true);
            AiInteraction.Instance.AddLogToPersonalityFile("=> given choice" + index + " : " + actionChoicesText[index].text + "---");
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
            Debug.LogWarning("No choice made, making no choice, you missed the opportunity to interact.");
            //ai implementation will go here
            //MakeChoice(UnityEngine.Random.Range(0, currentActionChoices.Count));
            ChangeAction(noAction);
            ExitDialogueMode();
        }

        timerSlider.value = 0; // Reset the slider
    }

    public void MakeChoice(int index){

        currentActionStory.ChooseChoiceIndex(index);

        storyStateManager.UpdateCurrentState();
        ChangeAction(noAction);

        AiInteraction.Instance.AddLogToPersonalityFile("=> choice made: " + actionChoicesText[index].text + "---");
        choiceMade = true;

        ContinueStory();
    }

        public void ChangeAction(BaseAction newAction)
    {
        if (!newAction)
        {
            Debug.LogError("Action " + newAction + " does not exist.");
            return;
        }

        if (currentActionScript != null)
        {
            currentActionScript.ExitAction();
        }
        currentActionScript = newAction;
        currentActionScript.EnterAction();
    }


    private void ExitDialogueMode()
    {
        storyVariables.StartListening(currentActionStory);
        actionIsPlaying = false;
        actionPanel.SetActive(false);
        actionText.text  = "";
    }

    public Story GetCurrentStory(){
        return currentActionStory;
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
