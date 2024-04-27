using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryState2 : StoryBaseState
{
    private StoryStateManager _storyStateManager;
    private ItemSO[] _inventoryBeforeRat;
    private TaskSO taskItem;

    private bool _isRatActionTriggered = false;
    private bool _isTaskTriggered = false;

    [SerializeField] private ItemSO ss2_map;
    [SerializeField] private GameObject ss2_rat;

    void Start(){

    }
    
    public override void EnterState()
    {   
        Debug.Log("Entering StoryState2");
        _storyStateManager = StoryStateManager.Instance;
        _inventoryBeforeRat = InventoryManager.Instance.GetInventory();
        Debug.Log("name: " + gameObject.name);
        DisableRatAction();
        DisableRatDialogue();
    
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


        if(_isRatActionTriggered && _isTaskTriggered)
        {
            bool gaveMoney = false;
            string value = ((Ink.Runtime.StringValue) _storyStateManager.GetStoryState("curstate")).value;
            if(value == "gave_gold")
            {
                gaveMoney = InventoryManager.Instance.RemoveSpecificTypeFromInventory(ItemSO.Type.Gold);
            }
            else if(value == "gave_crystal")
            {
                InventoryManager inventoryManager = InventoryManager.Instance;
                gaveMoney = inventoryManager.RemoveCrystal(inventoryManager.Crystal);
            }

                if(gaveMoney)
                {
                    Debug.Log("Gave money to rat.");
                    GiveMap();
                }
                else
                {
                    Debug.Log("Did not give money to rat.");
                }
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
            AddTask3();
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
            EnterRatAction();            
        }

    }

    public void DisableRatAction(){
        if(ss2_rat != null){
            Debug.Log("Rat is not interacting!");
            ss2_rat.GetComponentInChildren<ActionTrigger>().CanTrigger = false;
        }
        else{
            Debug.Log("Rat is null!");
        }
    }

    public void DisableRatDialogue(){
        if(ss2_rat != null){
            Debug.Log("Rat is not talking!");
            ss2_rat.GetComponentInChildren<DialogueTrigger>().CanTrigger = false;
        }else{
            Debug.Log("Rat is null!");
        }
    }




    public void EnterRatAction(){
        if(ss2_rat != null){
            Debug.Log("Rat is interacting!");
            ActionManager.Instance.EnterActionMode(ss2_rat.GetComponentInChildren<ActionTrigger>().inkJSON, ss2_rat.GetComponentInChildren<BaseAction>());
        }
    }


    
    public void GiveMap(){
        Player.Instance.TakeItem(ss2_map);
    }
    
    public void AddTask3(){
        Task3 task3 = gameObject.AddComponent<Task3>();
        task3.taskName = "Task3 Task3 ";
        task3.TimeToAchieve = 370f;
        TasksManager.Instance.AddTask(task3);
    }
}
