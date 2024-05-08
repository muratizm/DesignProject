using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] public UnityEvent onTriggerEnterEvent;

    public virtual void OnTriggerEnter(Collider other)
    {
        // Invoke the UnityEvent
        onTriggerEnterEvent?.Invoke();
    }
}