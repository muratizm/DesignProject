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
    private float _clickAmount = 10;
    private float _timeToFinish = 3;



    public override void StartGame(){
        Debug.Log("Starting ClickRush");
        timeToFinish = _timeToFinish * (1-_difficulty + 1f);
        base.StartGame();
    }


    private void Update(){

        ApplyGravity();
    }   

    public void OnClick()
    {
        Debug.Log("Clicked");
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
            _currentScore -= .1f;
        UpdateScore();
    }


    private void UpdateScore()
    {
        scoreText.text = "Score: " + (int) _currentScore;
        progressBar.value = _currentScore / _targetScore;
    }



}