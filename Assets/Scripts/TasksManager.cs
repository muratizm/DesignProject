using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TasksManager : MonoBehaviour
{
    public static TasksManager Instance { get; private set; }
    private GameManager gameManager;


    [Header("Tasks")]
    [SerializeField] private string[] tasks; // convert string to task object later 792



    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


    }

    void Start()
    {
        gameManager = GameManager.Instance;
    }   




}
