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
        timer.SetTimer(timeToRecover);
        timer.OnTimerComplete += () => _firstPersonController.IsInjured = false;
        StartCoroutine(timer.TimerCoroutine());
        
        _firstPersonController.IsInjured = true;
    }

    public void TakeItem(Item item)
    {
        if(item.IsBuggy)
        {
            _minigameManager.StartMinigame(MinigameManager.MinigameType.Bugs);

            _minigameManager.OnMinigameFinished += () => HandleMinigameFinished(item);
        }
        else
        {
            _inventoryManager.AddItem(item);
        }
    }

    void HandleMinigameFinished(Item item)
    {
        _minigameManager.OnMinigameFinished -= () => HandleMinigameFinished(item);

        if (_minigameManager.IsWon)
        {
            _inventoryManager.AddItem(item);
        }
    }

}
