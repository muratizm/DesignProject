using System;
using UnityEngine;

[Serializable]
public abstract class BaseTask : MonoBehaviour
{
    public string taskName;
    public string TaskName {get { return taskName; } private set { taskName = value; }}
    

    private float timeToAchieve;
    private float timeLeft;




    public virtual void EnterTask(){
        timeLeft = timeToAchieve;
        Debug.Log("Entering Task: " + TaskName);
    }


    public virtual void UpdateTask(){
        if(GameManager.Instance.IsGamePaused) return; // if game is paused, dont do anything

        if(timeLeft <= 0) {ExitTask();} // if time is up, exit the task
        timeLeft -= Time.deltaTime;
    }


    public virtual void AchieveTask(){
        Debug.Log("Task: " + TaskName + " achieved");
        TasksManager.Instance.RemoveTask(this);
    }
    public virtual void ExitTask(){
        Debug.Log("You failed " + TaskName + "! Exiting...");
    }
}