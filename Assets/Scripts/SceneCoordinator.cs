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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ClosePauseMenu(){
        pauseScenePanel.SetActive(false);
        GameManager.Instance.IsGamePaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PressedEscape(){
        if(settingsPanel.activeSelf)    { CloseSettings();} 
        else if(pauseScenePanel.activeSelf)    { ClosePauseMenu();}
        else if(taskPanel.activeSelf)    { CloseTaskButton();}
        else    { OpenPauseMenu();}
    }


    
    public void OnPressedTAB() // tab button means tasks button
    {
        // if taskPanel is active, deactivate it, if taskPanel is deactive, activate it
        if (taskPanel.activeSelf) CloseTaskButton(); else OpenTasks();
    }


    public void CloseTaskButton()
    {
        taskPanel.SetActive(false); // deactivate taskPanel
        GameManager.Instance.IsGamePaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OpenTasks()
    {
        taskPanel.SetActive(true); // activate taskPanel
        GameManager.Instance.IsGamePaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }


}
