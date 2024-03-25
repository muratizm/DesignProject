using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class MinigameManager : MonoBehaviour
{
    private static MinigameManager _minigameManager;
    public static MinigameManager Instance { get { return _minigameManager; } private set { return; } }
    private GameObject minigamesParent;


    private MiniGame _currentMinigame;
    public MiniGame CurrentMinigame { get { return _currentMinigame; } set {} }
    private bool isWon;
    public bool IsWon { get { return isWon; } set {} }


    public event Action OnMinigameFinished;


    public enum MinigameType
    {
        Bugs,
        ClickRush,
        RememberSpots
    }

    private void Awake()
    {
        if (_minigameManager != null)
        {
            Debug.Log("There is already an instance of MinigameManager in the scene");
            Destroy(gameObject);
            return;   
        }
        _minigameManager = this;
        minigamesParent = GameObject.Find(Constants.Paths.HIERARCHY_MINIGAME_PANEL);
    }




    public void StartMinigame(MinigameManager.MinigameType gameType)
    {
        switch (gameType)
        {
            case MinigameType.Bugs:
                Debug.Log("Starting Bugs minigame");
                StartBugsGame(Constants.Labels.SPOTS_MINIGAME);
                break;
            case MinigameType.ClickRush:
                Debug.Log("Starting ClickRush minigame");
                StartAnyGame(Constants.Labels.CLICKRUSH_MINIGAME);
                break;
            case MinigameType.RememberSpots:
                Debug.Log("Starting Remember Spots minigame");
                StartRememberSpotsGame(Constants.Labels.SPOTS_MINIGAME);
                break;
            default:
                Debug.Log("Invalid minigame type");
                break;
        }
    }

    public void EndMinigame(bool isWon)
    {
        this.isWon = isWon;
        OnMinigameFinished?.Invoke();
    }

    private void StartBugsGame(string label)
    {
        GameObject minigame = null;
        AssetLoader.Instance.LoadAssetAsync<GameObject>(label, (result) =>
        {
            minigame = Instantiate(result, Vector3.zero, Quaternion.identity, minigamesParent.transform);
            minigame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            if(minigame == null) {return;}

            

            ClickTheSpots clickTheSpots = minigame.GetComponent<ClickTheSpots>();
            clickTheSpots.SetType("Bug");
            clickTheSpots.StartGame();
        });
    }


    private void StartRememberSpotsGame(string label)
    {
        GameObject minigame = null;
        AssetLoader.Instance.LoadAssetAsync<GameObject>(label, (result) =>
        {
            minigame = Instantiate(result, Vector3.zero, Quaternion.identity, minigamesParent.transform);
            minigame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            if(minigame == null) {return;}

            ClickTheSpots clickTheSpots = minigame.GetComponent<ClickTheSpots>();
            clickTheSpots.SetType("Light");
            clickTheSpots.StartGame();
        });
    }


    private void StartAnyGame(string label)
    {
        GameObject minigame = null;
        AssetLoader.Instance.LoadAssetAsync<GameObject>(label, (result) =>
        {
            minigame = Instantiate(result, Vector3.zero, Quaternion.identity, minigamesParent.transform);
            minigame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            if(minigame == null) {return;}

            minigame.GetComponent<MiniGame>().StartGame();
        });
    }
}