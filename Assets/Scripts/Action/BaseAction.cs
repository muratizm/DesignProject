using System;
using UnityEngine;

[Serializable]
public abstract class BaseAction : MonoBehaviour

{
    public virtual void EnterAction(){}
    public virtual void UpdateAction(){}
    public virtual void ExitAction(){}
}
