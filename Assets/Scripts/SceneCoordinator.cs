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
        else    { OpenPauseMenu();}
    }


}
