using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;
using Ink.UnityIntegration;

public class ActionManager : MonoBehaviour
{
    private static ActionManager instance;
    private StoryVariables storyVariables; 
    private Story currentStory;
    private BaseAction currentAction;

    public bool actionIsPlaying {get; private set;}
        private bool lineEnded = false;
    
    [Header("ActionScript")]

    public BaseAction action;

    [Header("Globals Ink File")]
    [SerializeField] private TextAsset globalsTextFile;


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

        storyVariables = new StoryVariables(globalsTextFile);
    }


    void Start()
    {
        actionIsPlaying = false;
        actionPanel.SetActive(false);
        
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
        if(currentStory.canContinue){
            StartCoroutine(DisplayTextInSeconds(actionText, currentStory.Continue()));
        }
        else{
            ExitDialogueMode();
        }
    }


    
    private IEnumerator DisplayTextInSeconds(TMP_Text tmp_text, string content){
        tmp_text.text = "";
        
        //HandleTags(currentStory.currentTags);
        // action için uyarlama yapılacak   

        lineEnded = false;
         
        foreach(char letter in content){
            tmp_text.text += letter;
            yield return new WaitForSeconds(Constants.WAIT_BETWEEN_LETTERS);
        }

        lineEnded = true;

        if(actionText.text == ""){
            ContinueStory();
            
            // bu if ne işe yarıyor:
            /*
            .ink kodu: 
            === choices ===
            +[good ty] -> END           
            böyle bir seçenek olduğunda (goodty seçeneği seçilirse direkt kapat demek oluyor)
            direkt kapatmak yerine boş konuşma paneli açıyordu sonra kapatıyordu
            boş konuşma panelini geçsin diye bunu koydum.
            */
        }
            
        yield return new WaitForEndOfFrame();
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
            index++;
        }

        //hide remaining choices
        for(int i = index; i < actionChoices.Length; i++){
            actionChoices[i].gameObject.SetActive(false);
        }

    }

    public void MakeChoice(int index){

        currentStory.ChooseChoiceIndex(index);
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
}
