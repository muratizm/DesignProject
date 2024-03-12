using UnityEngine;

public class Task1 : BaseTask
{
    public override void EnterTask()
    {
        // Implementation for entering Task1
        Debug.Log("Entering Task1");
    }

    public override void UpdateTask()
    {
        // this method should be very optimized because it will be called every frame
        // and it will be called for every task in the game
        // so, be careful

        // learn the most optimized way to check for somethings every frame

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AchieveTask();
        }
        Debug.Log("Updating Task1");
    }

    public override void AchieveTask()
    {
        // Implementation for achieving Task1
        Debug.Log("Task1 achieved");
    }
}