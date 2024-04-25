using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryState2 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    [SerializeField ] private GameObject _rat;
    private ItemSO[] _inventoryBeforeRat;
    private TaskSO taskItem;

    private bool _isRatActionTriggered = false;
    private bool _isTaskTriggered = false;
    
    public override void EnterState()
    {   
        Debug.Log("Entering StoryState2");
        _storyStateManager = StoryStateManager.Instance;
        _inventoryBeforeRat = InventoryManager.Instance.GetInventory();
        StoryOperations.Instance.DisableRatAction();
        StoryOperations.Instance.DisableRatDialogue();
    
    }

    public override void ExitState()
    {
        Debug.Log("Exiting StoryState2");
    }

    public override void UpdateState()
    {
        if(!_isTaskTriggered)
        {
            TaskTrigger();
            _isTaskTriggered = true;
        }

        if(!_isRatActionTriggered)
        {
            RatActionTrigger();
            _isRatActionTriggered = true;
        }

    }


    private void TaskTrigger()
    {
        taskItem= InventoryManager.Instance.FindItem(ItemSO.Type.Task) as TaskSO;
        if (taskItem == null) { 
            Debug.Log("Task item is null.");
            return; 
        }
        if (taskItem.TaskTag == "Task3")
        {
            Debug.Log("Task3 added.");
            StoryOperations.Instance.AddTask3();
        }
        else    
        {
            Debug.Log("Task3 not added.");
        }

    }

    private void RatActionTrigger()
    {
        if(taskItem == null) { 
            Debug.Log("Task item is null.");
            return; 
        }
        
        if (Array.Exists(_inventoryBeforeRat, item => item != null && item.ItemTag == taskItem.ItemTag))
        {
            Debug.Log("The task item was there before the rat interaction.");
        }
        else
        {
            Debug.Log("The task item was not there before the rat interaction.");
            // yeni item geldi yani actiontrigger'ı aktif etcez
            StoryOperations.Instance.EnterRatAction();            
        }

    }
}
