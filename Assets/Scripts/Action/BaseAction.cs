using System;
using UnityEngine;

[Serializable]
public class BaseAction : MonoBehaviour

{
    public virtual void EnterAction(ActionManager story){}
    public virtual void UpdateAction(ActionManager story){}
}
