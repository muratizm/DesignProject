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
    [SerializeField] public BaseTask[] tasks = new BaseTask[10];
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
        UpdateTaskSlots();
    }   


    void Update()
    {
        if (gameManager.IsGamePaused) {return;}

        foreach (BaseTask task in tasks)
        {
            if (task == null) {continue;}
            task.UpdateTask(); // should write very optimized code in tasks update methods
        }
    }

    public void AddTask(BaseTask task) 
    {
        task.EnterTask();
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

    public void RemoveTask(BaseTask task) // by Task
    {
        int index = FindIndex(task);
        RemoveTask(index);
    }

    public void RemoveTask(int index) // by index
    {
        if (index == -1) {return;} // if task is not found, return

        tasks[index] = null; // remove the task from the array

        for(int i = index; i < tasks.Length - 1; i++) // shift the array to left
        {
            tasks[i] = tasks[i + 1];
            tasks[i + 1] = null;
            UpdateTaskSlot(i);
        }
        UpdateTaskSlot(tasks.Length - 1); 

    }

    public void UpdateTaskSlot(int index) // by index
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
            taskSlots[index].GetComponentInChildren<TextMeshProUGUI>().text = tasks[index].TaskName + " -- Less then " + (tasks[index].LeftMinutes+1) + "minutes left.";
        }
    }

    private void UpdateTaskSlots()
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            UpdateTaskSlot(i);
        }
    }

    public void UpdateTaskSlot(BaseTask task) // by Task
    {
        int index = FindIndex(task);
        if (index == -1) {Debug.Log("Couldn't update the slot, because could not find it!"); return;}
        UpdateTaskSlot(index);
    }


    public int FindIndex(BaseTask task)
    {
        for (int i = 0; i < tasks.Length; i++)
        {
            if (tasks[i] == task)
            {
                return i;
            }
        }
        return -1;
    }









}
