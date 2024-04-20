using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using System.IO;

public class ThoughtManager : MonoBehaviour
{
    private static ThoughtManager instance;
    public static ThoughtManager Instance { get { return instance; } private set { instance = value; } }


    private GameManager gameManager;
    private StoryStateManager storyStateManager;
    private StoryVariables storyVariables; 
    private Timer timer;



    [Header("Thought Bubble UI - Only works on Awake")]
    [SerializeField] private GameObject thoughtBubblePanel;
    [SerializeField] private TextMeshProUGUI thoughtBubbleText;
    [SerializeField] private float textSize = 12.0f;
    [SerializeField] private float textDelay = Constants.Times.WAIT_BETWEEN_LETTERS;
    private float thoughtDuration = Constants.Times.THOUGHT_BUBBLE_DURATION;
    private bool isThinking = false;



    private Story currentStory;


    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Found more than one ThoughtManager. Destroying the duplicate.");
            Destroy(gameObject);
        }
        Instance = this;
        thoughtBubbleText.fontSize = textSize;
        
    }


    private void Start() {
        storyStateManager = StoryStateManager.Instance;
        gameManager = GameManager.Instance;
        timer = new Timer();
        timer.OnTimerComplete += ContinueThinking;
        thoughtBubblePanel.SetActive(false);

        storyVariables = StoryStateManager.Instance.GetStoryVariables();
    }

    private void Update() {
        if(gameManager.IsGamePaused){return;} //if game is paused, dont do anything
    }

    public void EnterThoughtBubble(TextAsset inkJSON){
        if(isThinking){return;}

        currentStory = new Story(inkJSON.text);
        thoughtBubblePanel.SetActive(true);
        isThinking = true;

        storyVariables.StartListening(currentStory);
        
        ContinueThinking();
    }

    private void ExitThoughtBubble()
    {
        storyVariables.StartListening(currentStory);
        isThinking = false;
        thoughtBubblePanel.SetActive(false);
        thoughtBubbleText.text  = "";
    }

    public void ContinueThinking(){
        if(currentStory.canContinue){
            StartCoroutine(DisplayThoughtInSeconds(thoughtBubbleText, currentStory.Continue()));
            timer.SetTimer(thoughtDuration);
            StartCoroutine(timer.TimerCoroutine());
        }
        else{
            ExitThoughtBubble();
        }
    }
    
    private IEnumerator DisplayThoughtInSeconds(TMP_Text tmp_text, string content){
        tmp_text.text = "";
        
        foreach(char letter in content){
            tmp_text.text += letter;
            yield return new WaitForSeconds(textDelay);
        }


        File.AppendAllText(Constants.Paths.DIALOGUE_HISTORY_TEXT, "========= thought bubble : " + content + "\n");
    }




}
