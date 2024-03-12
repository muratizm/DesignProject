using System;
using UnityEngine;

[Serializable]
public abstract class BaseTask : MonoBehaviour
{
    public string taskName;
    public string TaskName {get { return taskName; } private set { taskName = value; }}
    

    private float timeToAchieve;
    public float TimeToAchieve {get { return timeToAchieve; } set { timeToAchieve = value; }}
    private float timeLeft;
    public float TimeLeft {get { return timeLeft; } private set { timeLeft = value; }}
    private int leftMinutes;
    public int LeftMinutes {get { return leftMinutes; } private set { leftMinutes = value; }}




    public virtual void EnterTask(){
        timeLeft = timeToAchieve;
        Debug.Log("Entering Task: " + TaskName);
    }


    public virtual void UpdateTask(){
        if(GameManager.Instance.IsGamePaused) return; // if game is paused, dont do anything

        leftMinutes = (int)timeLeft / 60;

        if(timeLeft <= 0) {ExitTask();} // if time is up, exit the task
        if((int) (timeLeft / 60) != leftMinutes) 
        {
            TasksManager.Instance.UpdateTaskSlot(this);
            leftMinutes = (int)timeLeft / 60;
        } 
        timeLeft -= Time.deltaTime;
    }


    public virtual void AchieveTask(){
        TasksManager.Instance.RemoveTask(this);
        Debug.Log("Task: " + TaskName + " achieved");
    }

    public virtual void ExitTask(){
        Debug.Log("You failed " + TaskName + "! Exiting...");
        TasksManager.Instance.RemoveTask(this);
    }
}