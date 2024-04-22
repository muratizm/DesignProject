using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Player : MonoBehaviour
{
    private static Player _player;
    public static Player Instance { get { return _player;} private set { return;} }

    private InventoryManager _inventoryManager;
    private FirstPersonController _firstPersonController;
    private MinigameManager _minigameManager;
    Timer timer;


    void Awake()
    {
        if (_player != null)
        {
            Destroy(gameObject);
            return;
        }
        _player = this;
    }

    void Start()
    {
        _minigameManager = MinigameManager.Instance;
        _inventoryManager = InventoryManager.Instance;
        _firstPersonController = FirstPersonController.Instance;
    }

    public void Injure(float timeToRecover)
    {
        timer = new Timer();
        timer.OnTimerComplete += () => _firstPersonController.IsInjured = false;
        timer.SetTimer(timeToRecover);
        timer.StartTimer();
        
        _firstPersonController.IsInjured = true;
    }

    public void TakeItem(ItemSO item)
    {
        Debug.Log("Player took item: " + item.name);
        if(item.IsBuggy)
        {
            _minigameManager.StartMinigame(MinigameManager.MinigameType.Bugs);
            Debug.Log("Minigame started1");
            _minigameManager.OnMinigameFinished += () => HandleMinigameFinished(item);
        }
        else if(item.IsMemories){
            _minigameManager.StartMinigame(MinigameManager.MinigameType.RememberSpots);
            Debug.Log("Minigame started2");
            _minigameManager.OnMinigameFinished += () => HandleMinigameFinished(item);
        }
        else
        {
            Debug.Log("Item added to inventory");
            _inventoryManager.AddItem(item);
        }
    }

    void HandleMinigameFinished(ItemSO item)
    {
        _minigameManager.ClearSubscribers();
        if (_minigameManager.IsWon)
        {
            Debug.Log("Minigame won");
            _inventoryManager.AddItem(item);
        }
    }

}
