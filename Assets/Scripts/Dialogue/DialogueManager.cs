using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;
using Ink.UnityIntegration;
using System.IO;
using System.Threading;

public class DialogueManager : MonoBehaviour
{
    private GameManager gameManager;
    private StoryStateManager storyStateManager;
    public static DialogueManager Instance { get; private set; }
    private StoryVariables storyVariables; 
    private Timer timer = new Timer();
    


    [Header("Dialogue UI - Only works on Awake")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private Image speakerImage;
    [SerializeField] private float textSize = 31.0f;
    [SerializeField] private float textDelay = Constants.Durations.WAIT_BETWEEN_LETTERS;
    private Animator layoutAnimator;
    private bool lineEnded = false;


    [Header("Choices")]
    [SerializeField] private GameObject[] choiceObjects;
    List<Choice> currentChoices;
    private TextMeshProUGUI[] choicesText;
    private int selectedChoiceIndex = -1;
    [SerializeField] private Slider timerSlider;
    private bool choiceMade = false;
    private bool showChoices = true;
    

    private Story currentStory;

    private bool isDialoguePlaying;
    public bool IsDialoguePlaying { get { return isDialoguePlaying; } private set { isDialoguePlaying = value; } }


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Found more than one DialogueManager. Destroying the duplicate.");
            Destroy(gameObject);
        }
        Instance = this;
        dialogueText.fontSize = textSize;
        
    }


    private void Start() {
        storyStateManager = StoryStateManager.Instance;
        gameManager = GameManager.Instance;

        IsDialoguePlaying = false;
        dialoguePanel.SetActive(false);

        storyVariables = StoryStateManager.Instance.GetStoryVariables();
        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        timer.OnTimerComplete += () => TimeIsUp();
        timer.OnTimerTick += () => UpdateTimerSlider(timer.ElapsedTime);
        AiInteraction.Instance.OnAITurnEnd += () => RestartTimer();



        choicesText = new TextMeshProUGUI[choiceObjects.Length];
        int index = 0;
        foreach (GameObject choice in choiceObjects){
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++; 
        }
    }

    private void Update() {
        if(gameManager.IsGamePaused){return;} //if game is paused, dont do anything

        if(!IsDialoguePlaying){ return; } //if dialogue is not playing, dont do anything
            

        if(Input.GetKeyDown(KeyCode.Return) && lineEnded){
            MakeChoice(selectedChoiceIndex);
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && selectedChoiceIndex != -1){
            GoPastChoice();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && selectedChoiceIndex != -1){
            GoNextChoice();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON, bool showChoices = true){
        this.showChoices = showChoices;
        currentStory = new Story(inkJSON.text);
        IsDialoguePlaying = true;
        dialoguePanel.SetActive(true);

        storyVariables.StartListening(currentStory);
        speakerNameText.text = "???";
        speakerImage.sprite = Resources.Load<Sprite>("Icons/Speakers/default");
        
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        IsDialoguePlaying = false;
        storyVariables.StartListening(currentStory);
        storyStateManager.UpdateCurrentState();
        dialoguePanel.SetActive(false);
        dialogueText.text  = "";
    }


    public void ContinueStory(){
        if(currentStory.canContinue){
            StartCoroutine(DisplayDialogueInSeconds(dialogueText, currentStory.Continue()));
        }
        else{
            ExitDialogueMode();
        }
    }


    private IEnumerator DisplayDialogueInSeconds(TMP_Text tmp_text, string content){
        tmp_text.text = "";
        
        HandleTags(currentStory.currentTags);
        
        lineEnded = false;
         
        foreach(char letter in content){
            tmp_text.text += letter;
            yield return new WaitForSeconds(textDelay);
        }

        lineEnded = true;

        File.AppendAllText(Constants.Paths.DIALOGUE_HISTORY_TEXT, "========= situation : " + content + "\n");
        choiceMade = false;

        UpdateTimerSlider(0);

        
        yield return new WaitForEndOfFrame();

        if(showChoices){
            DisplayChoices();
        }
        
    }




    private void DisplayChoices(){
        currentChoices = currentStory.currentChoices;

        if(currentChoices.Count == 0){return;} //if there are no choices, dont display anything

        // burda kaldın 792
        timer.SetTimer(Constants.Durations.DIALOGUE_WAIT_FOR_INPUT);
        timer.StartTimer();

        if(currentChoices.Count > choiceObjects.Length){
            Debug.LogError("more choices were given than the ui can support. there is not enough space for this choices. number of choices given: " + currentChoices);
        }

        //reveal choices that are given in story
        for(int i = 0; i < choiceObjects.Length; i++){ // 4 choices (last one is askAI choice)
            choiceObjects[i].gameObject.SetActive(true);
        }

        int index = 0;
        foreach(Choice choice in currentChoices){ // 3 real choices 
            choicesText[index].text = choice.text;
            File.AppendAllText(Constants.Paths.DIALOGUE_HISTORY_TEXT, "========= given choice : " + choice.text + "\n");
            index++;
        }


        if(currentChoices.Count > 0){
            selectedChoiceIndex = 0;
            HighlightSelectedChoice();
        }
        else{
            selectedChoiceIndex = -1;
        }

    }

    private void TimeIsUp(){
        if(!choiceMade){
            MakeChoice(Constants.ASKAI_CHOICE_INDEX);
        }
    }

    public void MakeChoice(int index){
        if(index == Constants.ASKAI_CHOICE_INDEX)
        {
            timer.PauseTimer();
            AiInteraction.Instance.AskAI(currentStory, timer);
            return;
        }

        if(index != -1){
            UnhighlightChoice();
            HideAllChoices();
            
            currentStory.ChooseChoiceIndex(index);
            Debug.Log("Choice made: " + currentChoices[index].text);
            timer.PauseTimer();
            choiceMade = true;
            ResetSelectedChoice();
            File.AppendAllText(Constants.Paths.DIALOGUE_HISTORY_TEXT, "=========choice made: " + choicesText[index].text + "\n");

        }

        storyStateManager.UpdateCurrentState();
        ContinueStory();
        

    }

    private void UpdateTimerSlider(float timePassed, float duration = Constants.Durations.DIALOGUE_WAIT_FOR_INPUT){
        timerSlider.value = 1 - (timePassed / duration);
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach(string tag in currentTags){
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2){
                Debug.LogError("tag couldnt parsed : " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            if(tagKey == Constants.Tags.SPEAKER_TAG){
                speakerNameText.text = tagValue;
                tagValue = (Resources.Load<Sprite>("Icons/Speakers/" + tagValue) != null) ? tagValue : "default";
                speakerImage.sprite = Resources.Load<Sprite>("Icons/Speakers/" + tagValue);          
            }
            if(tagKey == Constants.Tags.LAYOUT_TAG){
                layoutAnimator.Play(tagValue);
            }
        }
    }
    private void GoPastChoice(){
        UnhighlightChoice();
        selectedChoiceIndex = (selectedChoiceIndex == 0) ? (choiceObjects.Count() - 1) : selectedChoiceIndex - 1;
        HighlightSelectedChoice();
    }

    private void GoNextChoice(){
        UnhighlightChoice();
        selectedChoiceIndex = (selectedChoiceIndex == (choiceObjects.Count() - 1)) ? 0 : selectedChoiceIndex + 1;
        HighlightSelectedChoice();
    }



    private void HighlightSelectedChoice(){
        Button button = choiceObjects[selectedChoiceIndex].GetComponent<Button>();
        if (choiceObjects[selectedChoiceIndex] != null)
        {
            button.GetComponent<Image>().color = new Color(.25f, 1f, 0f, .1f);
        }
        else{
            Debug.Log("This button is null!");
        }
    }

    public void UnhighlightChoice()
    {
        Button button = choiceObjects[selectedChoiceIndex].GetComponent<Button>();
        if (choiceObjects[selectedChoiceIndex] != null)
        {
            button.GetComponent<Image>().color = new Color(.25f, 1f, 0f, 0f);
        }
        else{
            Debug.Log("This button is null!");
        }
    }

    private void HideAllChoices(){
        for(int i = 0; i < choiceObjects.Length; i++){
            choiceObjects[i].gameObject.SetActive(false);
        }
    }

    private void ResetSelectedChoice(){
        selectedChoiceIndex = -1;
    }
    public Ink.Runtime.Object GetStoryState(string variableName){
        Ink.Runtime.Object variableValue = null;
        storyVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableValue == null){
            Debug.LogWarning("ink variable was found to be null: " + variableName );
        }
        return variableValue;
    }


    public void RestartTimer(){
        timer.SetTimer(Constants.Durations.DIALOGUE_WAIT_FOR_INPUT);
        timer.StartTimer();
    }

}
