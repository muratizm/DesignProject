using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance  { get; private set; }
    private SceneCoordinator sceneCoordinator;
    public bool IsGamePaused;


    private GameManager(){}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Found more than one GameManager. Destroying the duplicate.");
            Destroy(gameObject);
        }
        Instance = this;
        sceneCoordinator = SceneCoordinator.Instance;
    }


    void Start(){
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            sceneCoordinator.PressedEscape();
        }
    }

    public void SetVolume(float volume){
        AudioListener.volume = volume;
    }



}
