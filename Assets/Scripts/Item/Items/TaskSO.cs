using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "New Task", menuName = "Inventory/Item/Task")]
public class TaskSO : PaperSO
{
    [SerializeField] private string _taskTag;
    public string TaskTag => _taskTag;
    public override void Use()
    {
        base.Use();
    }

}