using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class TasksManager : MonoBehaviour
{
    // tasks system is designed with 10 max capacity
    public static TasksManager Instance { get; private set; }
    private GameManager gameManager;


    [Header("Tasks")]
    [SerializeField] private BaseTask[] tasks = new BaseTask[10];
    [SerializeField] private GameObject taskPanel; 
    [SerializeField] private GameObject[] taskSlots;
    



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


    void Update()
    {
        if (gameManager.IsGamePaused) {return;}

        foreach (BaseTask task in tasks)
        {
            //task.UpdateTask(); // should write very optimized code in tasks update methods
        }
    }

    public void AddTask(BaseTask task) 
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            if (tasks[i] == null)
            {
                tasks[i] = task;
                UpdateTaskSlot(i);
                return;
            }
        }
    }

    public void RemoveTask(BaseTask task)
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            if (tasks[i] == task)
            {
                tasks[i] = null;
                taskSlots[i].SetActive(false);
                return;
            }
        }
    }

    private void UpdateTaskSlot(int index)
    {
        // update UI
        taskSlots[index].SetActive(true);
        taskSlots[index].GetComponentInChildren<TextMeshProUGUI>().text = tasks[index].TaskName;
    }









}
