using UnityEngine;

public class Task1 : BaseTask
{
    InventoryManager _inventoryManager;
    public override void EnterTask()
    {
        base.EnterTask();
        _inventoryManager = InventoryManager.Instance;
        _inventoryManager.InventoryChanged += InventoryManager_InventoryChanged;
    }

    public override void UpdateTask()
    {
        // this method should be very optimized because it will be called every frame
        // and it will be called for every task in the game
        // so, be careful

        // learn the most optimized way to check for somethings every frame

        base.UpdateTask(); // runs the timer, checks if game is paused, etc


        // now, we can check for the input to achieve the task


    }


    private void InventoryManager_InventoryChanged(object sender, System.EventArgs e)
    {
        Debug.Log("Inventory Changed Notification Received from InventoryManager to Task1"); 
        CheckInventory(_inventoryManager.GetInventory());
        Debug.Log("Inventory Changed");
        
    }


    private void CheckInventory(ItemSO[] items){
        Debug.Log("Checking Inventory");
        int ringCounter = 0;
        foreach (ItemSO item in items)
        {
            if(item.ItemType == ItemSO.Type.Ring){
                ringCounter++;
            }
        }
        if(ringCounter >= 3){   
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