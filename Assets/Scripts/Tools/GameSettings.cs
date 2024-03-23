using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

// This class is a singleton that holds the game settings and level data
public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }



    // game settings
    private int _a; 
    private int _b; 


    public int A { get { return _a; } }
    public int B { get { return _b; } }


    private const int _defaultA = 5;
    private const int _defaultB = 7;


 
    //other settings
    private int _other;

    public int Other { get { return _other; } }



    //[Header("Visual Settings")]   



    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadLevelProgress();
        LoadGameData();



    }


    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



    private void SaveLevelProgress()
    {
        PlayerPrefs.SetInt("a", _a);
        PlayerPrefs.SetInt("b", _b );
    }


    private void LoadLevelProgress()
    {
        _a = PlayerPrefs.GetInt("a", _defaultA);
        _b = PlayerPrefs.GetInt("b", _defaultB);
    }



    private void SaveGameData()
    {
        PlayerPrefs.SetInt("a", _a);
        PlayerPrefs.SetInt("b", _b);

    }

    private void LoadGameData()
    {
        _a = PlayerPrefs.GetInt("a", _defaultA);
        _b = PlayerPrefs.GetInt("b", _defaultB);
    }


    public void ReturnToMainMenu()
    {
        SaveLevelProgress();
        SceneManager.LoadScene("Title");
    }

    


    public void LoadDataFromEditor(int a, int b)
    {
        _a = a;
        _b = b;
        ReloadScene();
    }


    public void ReturnToDefaultSettings()
    {
        _a = _defaultA;
        _b = _defaultB;
        Debug.Log("Settings returned to default");
        ReloadScene();
        SaveGameData();
    }


}