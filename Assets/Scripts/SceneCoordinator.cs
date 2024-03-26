using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCoordinator : MonoBehaviour
{
    public static SceneCoordinator Instance  { get; private set; }

    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject pauseScenePanel;
    [SerializeField] private GameObject taskPanel;
    [SerializeField] private GameObject celebratePanel;


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
        if(settingsPanel.activeSelf)    { CloseSettings();} 
        else if(pauseScenePanel.activeSelf)    { ClosePauseMenu();}
        else if(taskPanel.activeSelf)    { CloseTaskButton();}
        else    { OpenPauseMenu();}
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

    private void OpenCelebratePanel()
    {
        celebratePanel.SetActive(true);
        Invoke("CloseCelebratePanel", 0.5f);
    }

    private void CloseCelebratePanel()
    {
        celebratePanel.SetActive(false);
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


}
