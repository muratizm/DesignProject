using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.Linq;
using Ink.UnityIntegration;
using System.IO;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    private StoryVariables storyVariables; 
    private string path = "Assets/Choice/debug.txt";


    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI speakerNameText;
    [SerializeField] private Image speakerImage;
    private Animator layoutAnimator;
    private bool lineEnded = false;


    [Header("Choices")]
    [SerializeField] private GameObject[] choices;
    List<Choice> currentChoices;
    private TextMeshProUGUI[] choicesText;
    private int selectedChoiceIndex = -1;
    

    private Story currentStory;

    public bool dialogueIsPlaying {get; private set;}


    public static DialogueManager GetInstance(){
        return instance;
    }
        void Awake()
    {
        if(instance != null){
            Debug.LogError("found more than one StoryStateManager.");
        }
        instance = this;

        
    }


    private void Start() {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        storyVariables = StoryStateManager.Instance.GetStoryVariables();


        layoutAnimator = dialoguePanel.GetComponent<Animator>();

        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices){
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++; 
        }
    }

    private void Update() {
        if(!dialogueIsPlaying){
            return;
        }
        if(Input.GetKeyDown(KeyCode.Return) && lineEnded){
            MakeChoice();
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) && selectedChoiceIndex != -1){
            GoPastChoice();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow) && selectedChoiceIndex != -1){
            GoNextChoice();
        }

    }

    public void EnterDialogueMode(TextAsset inkJSON){
        currentStory = new Story(inkJSON.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);

        storyVariables.StartListening(currentStory);
        speakerNameText.text = "???";
        speakerImage.sprite = Resources.Load<Sprite>("Icons/Speakers/default");
        
        ContinueStory();
    }

    private void ExitDialogueMode()
    {
        storyVariables.StartListening(currentStory);

        dialogueIsPlaying = false;
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
            yield return new WaitForSeconds(Constants.WAIT_BETWEEN_LETTERS);
        }

        lineEnded = true;

        File.AppendAllText(path, "========= situation : " + content + "\n");
        
        yield return new WaitForEndOfFrame();
        DisplayChoices();
    }




    private void DisplayChoices(){
        currentChoices = currentStory.currentChoices;

        if(currentChoices.Count > choices.Length){
            Debug.LogError("more choices were given than the ui can support. there is not enough space for this choices. number of choices given: " + currentChoices);
        }

        //reveal choices that are given in story
        int index = 0;
        foreach (Choice choice in currentChoices){
            choicesText[index].text = choice.text;
            choices[index].gameObject.SetActive(true);
            File.AppendAllText(path, "========= given choice" + index + " : " + choicesText[index].text + "\n");
            index++;
        }

        //hide remaining choices
        for(int i = index; i < choices.Length; i++){
            choices[i].gameObject.SetActive(false);
        }

        if(currentChoices.Count > 0){
            selectedChoiceIndex = 0;
            HighlightSelectedChoice();
        }
        else{
            selectedChoiceIndex = -1;
        }

    }

    public void MakeChoice(){
        if(selectedChoiceIndex != -1){
            UnhighlightChoice();
            for(int i = 0; i < choices.Length; i++){ //hide all choices
                choices[i].gameObject.SetActive(false);
            }
            currentStory.ChooseChoiceIndex(selectedChoiceIndex);
            File.AppendAllText(path, "=========choice made: " + choicesText[selectedChoiceIndex].text + "\n");
            
        }
        ContinueStory();
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

            if(tagKey == Constants.SPEAKER_TAG){
                speakerNameText.text = tagValue;
                tagValue = (Resources.Load<Sprite>("Icons/Speakers/" + tagValue) != null) ? tagValue : "default";
                speakerImage.sprite = Resources.Load<Sprite>("Icons/Speakers/" + tagValue);          
            }
            if(tagKey == Constants.LAYOUT_TAG){
                layoutAnimator.Play(tagValue);
            }
        }
    }
    private void GoPastChoice(){
        UnhighlightChoice();
        selectedChoiceIndex = (selectedChoiceIndex == 0) ? (currentChoices.Count-1) : selectedChoiceIndex - 1;
        HighlightSelectedChoice();
    }

    private void GoNextChoice(){
        UnhighlightChoice();
        selectedChoiceIndex = (selectedChoiceIndex == (currentChoices.Count-1)) ? 0 : selectedChoiceIndex + 1;
        HighlightSelectedChoice();
    }



    private void HighlightSelectedChoice(){
        Button button = choices[selectedChoiceIndex].GetComponent<Button>();
        if (choices[selectedChoiceIndex] != null)
        {
            button.GetComponent<Image>().color = new Color(.25f, 1f, 0f, .1f);
        }
        else{
            Debug.Log("This button is null!");
        }
    }

    public void UnhighlightChoice()
    {
        Button button = choices[selectedChoiceIndex].GetComponent<Button>();
        if (choices[selectedChoiceIndex] != null)
        {
            button.GetComponent<Image>().color = new Color(.25f, 1f, 0f, 0f);
        }
        else{
            Debug.Log("This button is null!");
        }
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
