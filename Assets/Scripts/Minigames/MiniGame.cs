using UnityEngine;
using UnityEngine.UI;

public class MiniGame : MonoBehaviour
{
    protected bool isMiniGameRunning = false;
    public bool IsMiniGameRunning { get { return isMiniGameRunning; } }
    protected float timeToFinish = 2f;
    protected Slider slider;
    protected Timer timer;
    
    void Update()
    {
        if (!isMiniGameRunning){return;}
    }

    public virtual void StartGame()
    {
        // Initialization code goes here
        Debug.Log("Starting MiniGame");
        SceneCoordinator.Instance.UnlockCursor();

        slider = gameObject.transform.Find("Slider").GetComponent<Slider>();

        GameManager.Instance.IsGamePaused = true;
        isMiniGameRunning = true;

        timer = new Timer();
        timer.OnTimerTick += ManageSlider;
        timer.OnTimerComplete += () => ExitGame(false);
        StartTimer();
    }



    public virtual void StartTimer()
    {
        timer.SetTimer(timeToFinish);
        StartCoroutine(timer.TimerCoroutine());
    }

    public virtual void ExitGame(bool isWon = false)
    {
        Debug.Log("Exiting MiniGame");
        isMiniGameRunning = false;
        MinigameManager.Instance.EndMinigame(isWon);
        GameManager.Instance.IsGamePaused = false;
        SceneCoordinator.Instance.LockCursor();
        Destroy(gameObject);
    }

    protected void ManageSlider()
    {
        slider.value = timer.ElapsedTime / timeToFinish;
    }
}