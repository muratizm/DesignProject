using System;
using UnityEngine;

[Serializable]
public class BaseAction : MonoBehaviour

{
    public virtual void EnterAction(){}
    public virtual void UpdateAction(){}
    public virtual void ExitAction(){}
}
