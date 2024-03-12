using System;
using UnityEngine;

[Serializable]
public abstract class BaseTask : MonoBehaviour
{
    private string taskName;

    public string TaskName
    {
        get { return taskName; }
        private set { taskName = value; }
    }

    public virtual void EnterTask(){}
    public virtual void UpdateTask(){}
    public virtual void AchieveTask(){}
}