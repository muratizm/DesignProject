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
    private static ActionManager instance;
    private StoryVariables storyVariables; 
    private string path = "Assets/Choice/debug.txt";

    private Story currentStory;
    private BaseAction currentAction;

    public bool actionIsPlaying {get; private set;}
    


    [Header("ActionScript")]
    public BaseAction action;


    [Header("Action UI")]
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private TextMeshProUGUI actionText;


    
    [Header("Choices")]
    [SerializeField] private GameObject[] actionChoices;
    List<Choice> currentActionChoices;
    private TextMeshProUGUI[] actionChoicesText;
    
    
    public static ActionManager GetInstance(){
        return instance;
    }
    

    void Awake()
    {
        if(instance != null){
            Debug.LogError("found more than one DialogueManager.");
        }
        instance = this;

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
        currentAction?.UpdateAction(this);
    }
    
    public void EnterActionMode(TextAsset inkJSON, BaseAction actionScript){
        currentStory = new Story(inkJSON.text);
        actionIsPlaying = true;
        actionPanel.SetActive(true);

        storyVariables.StartListening(currentStory);        
        ContinueStory();
        currentAction = actionScript;
        currentAction.EnterAction(this);
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
        
        File.AppendAllText(path, "========= situation : " + content + "\n");

        

        if(currentStory.currentTags.Contains("end3"))
        {
            Invoke("ExitDialogueMode", 3);
        }
        DisplayChoices();
    }



    
    private void DisplayChoices(){
        currentActionChoices = currentStory.currentChoices;

        if(currentActionChoices.Count > actionChoices.Length){
            Debug.LogError("more choices were given than the ui can support. there is not enough space for this choices. number of choices given: " + currentActionChoices);
        }

        //reveal choices that are given in story
        int index = 0;
        foreach (Choice choice in currentActionChoices){
            actionChoicesText[index].text = choice.text;
            actionChoices[index].gameObject.SetActive(true);
            File.AppendAllText(path, "========= given choice" + index + " : " + actionChoicesText[index].text + "\n");
            index++;
        }

        //hide remaining choices
        for(int i = index; i < actionChoices.Length; i++){
            actionChoices[i].gameObject.SetActive(false);
        }

    }

    public void MakeChoice(int index){

        currentStory.ChooseChoiceIndex(index);
        File.AppendAllText(path, "=========choice made: " + actionChoicesText[index].text + "\n");

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
