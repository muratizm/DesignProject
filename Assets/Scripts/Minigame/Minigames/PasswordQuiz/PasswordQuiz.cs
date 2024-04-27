using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class PasswordQuiz : MiniGame
{
    private TextMeshProUGUI quizQuestionText;
    private InputField inputField;
    private int selectedQuestionIndex;
 
    [System.Serializable]
    public struct QuestionAnswerPair
    {
        [TextArea] public string question;
        public string answer;
    }

    [SerializeField] QuestionAnswerPair[] questionAnswerPairs;    


    public override void StartGame(){
        quizQuestionText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        inputField = gameObject.GetComponentInChildren<InputField>();
        inputField.onEndEdit.AddListener(OnSubmit);

        SetQuestion();

        timeToFinish = Constants.Durations.TIME_TO_FINISH_PASSWORDQUIZ;
        base.StartGame();
    }

    private void SetQuestion(){
        selectedQuestionIndex = Random.Range(0, questionAnswerPairs.Length);
        quizQuestionText.text = questionAnswerPairs[selectedQuestionIndex].question.ToString();
    }

    public void OnSubmit(string input)
    {
        bool isTrue = input == questionAnswerPairs[selectedQuestionIndex].answer;
        ExitGame(isTrue);
    }
}