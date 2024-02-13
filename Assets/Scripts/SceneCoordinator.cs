using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCoordinator : MonoBehaviour
{
    private static SceneCoordinator instance;
    public static SceneCoordinator Instance => instance ?? (instance = new SceneCoordinator());

    [SerializeField] private GameObject settingsPanel;

    private SceneCoordinator(){}

    public void Awake(){

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
}
