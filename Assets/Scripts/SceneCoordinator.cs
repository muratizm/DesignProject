using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class SceneCoordinator : MonoBehaviour
{
    public static SceneCoordinator Instance  { get; private set; }

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pauseScenePanel;
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private GameObject celebratePanel;
    [SerializeField] private GameObject fadePanel;
    [SerializeField] private GameObject bookPanel;
    private Animation fadeAnimation;

    private MinigameManager _minigameManager;
    private SceneCoordinator(){}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Found more than one GameManager. Destroying the duplicate.");
            Destroy(gameObject);
        }
        Instance = this;

    }

    void Start ()
    {
        _minigameManager = MinigameManager.Instance;
        _minigameManager.OnMinigameFinished += () => OpenCelebratePanel();

        fadeAnimation = fadePanel.GetComponent<Animation>();
        FadeIn();
    }

    public void OpenScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void OpenSettings(){
        settingsPanel.SetActive(true);
    }

    public void CloseSettings(){
        settingsPanel.SetActive(false);
    }

    public void OpenPauseMenu(){
        pauseScenePanel.SetActive(true);
        GameManager.Instance.IsGamePaused = true;
        Time.timeScale = 0;
        UnlockCursor();
    }

    public void ClosePauseMenu(){
        pauseScenePanel.SetActive(false);
        GameManager.Instance.IsGamePaused = false;
        Time.timeScale = 1;
        LockCursor();
    }

    public void PressedEscape(){
        // kötü kod ama zaman yok
        GameObject[] closeables = GetCloseables();
        if(closeables.Length > 0){
            CloseNextCloseable(closeables);
        }
        else{
            OpenPauseMenu();
        }
        
    }

    private GameObject[] GetCloseables(){
        return GameObject.FindGameObjectsWithTag("CloseableByESC");
    }

    private void CloseNextCloseable(GameObject[] closeables){
        for (int i = closeables.Length - 1; i >= 0; i--)
        {
            Transform child = closeables[i].transform;
            if (child.gameObject.activeSelf)
            {
                child.gameObject.SetActive(false);
            if(child.gameObject.name.StartsWith("ItemUI")){
                Destroy(child.gameObject);
            }
                break;
            }
        }

        if (closeables.Length == 1)
        {
            ContinueGame();
        }
    }

    private void ContinueGame(){
        Time.timeScale = 1;
        FirstPersonController.Instance.CanRotateView = true;
        GameManager.Instance.IsGamePaused = false;
        LockCursor();
        
    }

    public void OnPressedTAB() // tab button means tasks button
    {
        if (pauseScenePanel.activeSelf || settingsPanel.activeSelf) {return;} // if settingsPanel is active, do not open taskPanel

        // if taskPanel is active, deactivate it, if taskPanel is deactive, activate it
        if (taskPanel.activeSelf) CloseTaskButton(); else OpenTasks();
    }

    public void CloseTaskButton()
    {
        taskPanel.SetActive(false); // deactivate taskPanel
        GameManager.Instance.IsGamePaused = false;
        LockCursor();
    }

    public void OpenTasks()
    {
        taskPanel.SetActive(true); // activate taskPanel
        GameManager.Instance.IsGamePaused = true;
        UnlockCursor();
    }

    private async void OpenCelebratePanel()
    {
        if(MinigameManager.Instance.IsWon){
            celebratePanel.SetActive(true);
            await Task.Delay(500);
            CloseCelebratePanel();
        }

    }

    private void CloseCelebratePanel()
    {
        celebratePanel.SetActive(false);
    }

    public void OpenBookPanel(Sprite[] pages)
    {
        bookPanel.SetActive(true);
        bookPanel.GetComponent<Book>().bookPages = pages;
        FirstPersonController.Instance.CanRotateView = false;
        UnlockCursor();
    }

    public void CloseBookPanel()
    {
        bookPanel.SetActive(false);
        FirstPersonController.Instance.CanRotateView = true;
        LockCursor();
    }

    public void LockCursor ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void UnlockCursor ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public async void OpenNewScene(string sceneName){
        FadeOut();
        await Task.Delay(Constants.Durations.FADEOUT_DURATION_MS);
        SceneManager.LoadScene(sceneName);
    }

    public async void FadeAnimation(){
        FadeOut();
        await Task.Delay(Constants.Durations.FADEOUT_DURATION_MS); // wait for the fadeOut finish

        await Task.Delay(1000); // wait extra 1 second for more realistic effect
        FadeIn();
    }

    public void FadeOut()
    {
        fadePanel.SetActive(true);
        fadeAnimation.Play("FadeOut");
    }

    public async void FadeIn()
    {
        fadePanel.SetActive(true);
        fadeAnimation.Play("FadeIn");
        await Task.Delay(Constants.Durations.FADEIN_DURATION_MS); // wait for 1 second to finish the fadeIn animation
        fadePanel.SetActive(false);
    }
}
