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
        Memory,
        Math
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
                StartSelectedGame(Constants.Labels.BUGS_MINIGAME);
                break;
            case MinigameType.Memory:
                Debug.Log("Starting Memory minigame");
                break;
            case MinigameType.Math:
                Debug.Log("Starting Math minigame");
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

    public void StartSelectedGame(string label)
    {
        GameObject bugsMinigame = null;

        Addressables.LoadAssetsAsync<GameObject>(label, null).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                foreach (var gameObject in handle.Result)
                {
                    bugsMinigame = Instantiate(gameObject, Vector3.zero, Quaternion.identity, minigamesParent.transform);
                    bugsMinigame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

                    if(bugsMinigame == null) {return;}

                    bugsMinigame.GetComponent<MiniGame>().StartGame();
                    // 792FIXME: burda <killthebugs> olmamalı minigame olmalı ama şuan başka işim var
                    // 793FIXME: hata çıkabilri diye şimdi bakmıyorum sonra bak buraya
                }
            }
            else
            {
                Debug.LogError("Failed to load minigame from addressables");
            }
        };
    }
}