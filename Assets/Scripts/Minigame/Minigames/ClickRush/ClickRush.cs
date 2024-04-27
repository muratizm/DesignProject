using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ClickRush : MiniGame
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Slider timeBar;


    [SerializeField] private float _difficulty = 1; // 1 is the hardest, 0 is the easiest
    private float _targetScore = 100;
    private float _currentScore = 0;
    private float _clickAmount = 20;
    private float _timeToFinish = Constants.Durations.TIME_TO_FINISH_CLICKRUSH;



    public override void StartGame(){
        timeToFinish = _timeToFinish * (1-_difficulty + 1f);
        base.StartGame();
    }


    private void Update(){

        ApplyGravity();
    }   

    public void OnClick()
    {
        _currentScore += _clickAmount * (1-_difficulty + 0.25f);
        if (_currentScore >= _targetScore)
        {
            ExitGame(true);
        }
        UpdateScore();
    }


    private void ApplyGravity()
    {
        if(_currentScore > 0)
            _currentScore -= .5f;
        UpdateScore();
    }


    private void UpdateScore()
    {
        scoreText.text = "Score: " + (int) _currentScore;
        progressBar.value = _currentScore / _targetScore;
    }



}