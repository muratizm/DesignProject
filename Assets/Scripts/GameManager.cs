using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance  { get; private set; }
    private SceneCoordinator sceneCoordinator;
    public bool IsGamePaused;


    // settings variables
    [SerializeField] private Slider volumeSlider;
    private int newVolume;
    private int newResolutionIndex;
    private int newWindowModeIndex;


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
        // Loads player preferences and saves settings when game is started.
        LoadPlayerPrefs(); 
        SaveSettings(); 

        //
        
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            sceneCoordinator.PressedEscape();
        }
    }

    public void SetVolume(float volume){ // puts new volume in the newVolume variable
        newVolume = (int) volume;
        Debug.Log("Volume: " + newVolume);
    }

    public void SetResolution(int resolution){ // puts new resolution in the newResolution variable
        newResolutionIndex = resolution;
    }

    public void SetWindowMode(int windowMode){ // puts new window mode in the newWindowMode variable
        newWindowModeIndex = windowMode;
    }

    public void SaveSettings(){ // sets the new settings if player presses the save button
        AudioListener.volume = newVolume;
        Screen.SetResolution(1920, 1080, (FullScreenMode) newWindowModeIndex);
        Debug.Log("Volume: " + newVolume + " Resolution: " + newResolutionIndex + " WindowMode: " + newWindowModeIndex);

        // update slider's handle position
        volumeSlider.value = newVolume;
    }



    public void SavePlayerPrefs(){ // saves the new settings to player prefs
        PlayerPrefs.SetInt("volume", newVolume);
        PlayerPrefs.SetInt("resolution", newResolutionIndex);
        PlayerPrefs.SetInt("windowMode", newWindowModeIndex);
    }

    public void LoadPlayerPrefs(){ // loads the settings from player prefs
        newVolume = PlayerPrefs.GetInt("volume", 100);
        newResolutionIndex = PlayerPrefs.GetInt("resolution", 0);
        newWindowModeIndex = PlayerPrefs.GetInt("windowMode", 0);
    }

    void OnDestroy() // saves the settings when the game is closed
    {
        SavePlayerPrefs();
    }



}
