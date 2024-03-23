using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance  { get; private set; }
    private SceneCoordinator sceneCoordinator;


    // settings variables
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TMP_Dropdown windowModeDropdown;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private int newVolume;
    private int newResolutionIndex;
    private int newWindowModeIndex;

    private SettingsManager(){}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Found more than one SettingsManager. Destroying the duplicate.");
            Destroy(gameObject);
        }
        Instance = this;
    }

    void Start(){
        sceneCoordinator = SceneCoordinator.Instance;

        LoadSettingsPlayerPrefs();
        ApplyNewSettings();
        UpdateSettingsPanel();
    }

    public void SetVolume(float volume){ // settings panel : puts new volume in the newVolume variable
        newVolume = (int) volume;
        Debug.Log("Volume: " + newVolume);
    }

    public void SetResolution(int resolution){ // settings panel : puts new resolution in the newResolution variable
        newResolutionIndex = resolution;
        Debug.Log("Resolution: " + newResolutionIndex);
    }

    public void SetWindowMode(int windowMode){ // settings panel : puts new window mode in the newWindowMode variable
        newWindowModeIndex = windowMode;
        Debug.Log("WindowMode: " + newWindowModeIndex);
    }

    public void SaveButton(){ // settings panel : saves the new settings
        ApplyNewSettings();
        SaveSettingsToPlayerPrefs();
        sceneCoordinator.CloseSettings();
    }

    private void ApplyNewSettings(){ // settings panel : sets the new settings to game

        // 0 is for 1920x1080, 1 is for 1600x900
        (int width, int height) = newResolutionIndex == 0 ? (1920, 1080) : (1600, 900); 

        // 0 is for fullscreen, 1 is for windowed
        FullScreenMode mode = newWindowModeIndex == 0 ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed; 

        // set resolution and volume
        Screen.SetResolution(width, height, mode);
        AudioListener.volume = newVolume;
    }

    private void UpdateSettingsPanel(){ // settings panel : updates the settings panel with the current settings
        volumeSlider.value = newVolume;
        resolutionDropdown.value = newResolutionIndex;
        windowModeDropdown.value = newWindowModeIndex;
    }



    public void SaveSettingsToPlayerPrefs(){ // settings panel : saves the new settings to player prefs
        PlayerPrefs.SetInt("volume", newVolume);
        PlayerPrefs.SetInt("resolution", newResolutionIndex);
        PlayerPrefs.SetInt("windowMode", newWindowModeIndex);
        PlayerPrefs.Save();
        Debug.Log("Saved settings: " + newVolume + " " + newResolutionIndex + " " + newWindowModeIndex);
    }

    private void LoadSettingsPlayerPrefs(){ // settings panel : loads the settings from player prefs
        newVolume = PlayerPrefs.GetInt("volume", 100);
        newResolutionIndex = PlayerPrefs.GetInt("resolution", 0);
        newWindowModeIndex = PlayerPrefs.GetInt("windowMode", 0);
        Debug.Log("Loaded settings: " + newVolume + " " + newResolutionIndex + " " + newWindowModeIndex);
    }


    private void OnDestroy() // saves the current settings when the game is closed
    {
        SaveSettingsToPlayerPrefs();
    }

}
