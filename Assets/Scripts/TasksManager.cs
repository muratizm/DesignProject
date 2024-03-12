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

    public void RemoveTask(BaseTask task) // remove the task from the array (shift the array to left)
    {
        int index = -1;

        for (int i = 0; i < tasks.Length; i++) // find the index of the task
        {
            if (tasks[i] == task)
            {
                index = i;
                break;
            }
        }

        if (index == -1) {return;} // if task is not found, return

        tasks[index] = null; // remove the task from the array

        for(int i = index; i < tasks.Length - 1; i++) // shift the array to left
        {
            if (tasks[i + 1] == null) {break;}
            tasks[i] = tasks[i + 1];
            tasks[i + 1] = null;
            UpdateTaskSlot(i);
        }
        UpdateTaskSlot(tasks.Length - 1); 

    }

    private void UpdateTaskSlot(int index)
    {
        // update UI
        if (tasks[index] == null)
        {
            taskSlots[index].SetActive(false);
            return;
        }
        else
        {
            taskSlots[index].SetActive(true);
            taskSlots[index].GetComponentInChildren<TextMeshProUGUI>().text = tasks[index].TaskName;
        }

    }









}
