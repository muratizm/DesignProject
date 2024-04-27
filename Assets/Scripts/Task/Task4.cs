using UnityEngine;

public class Task4 : BaseTask
{
    public override void EnterTask()
    {
        base.EnterTask();
    }

    public override void UpdateTask()
    {
        // this method should be very optimized because it will be called every frame
        // and it will be called for every task in the game
        // so, be careful

        // learn the most optimized way to check for somethings every frame

        base.UpdateTask(); // runs the timer, checks if game is paused, etc

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AchieveTask();
        }
    }

    public override void AchieveTask()
    {
        base.AchieveTask();
    }

    public override void ExitTask()
    {
        base.ExitTask();
    }
}