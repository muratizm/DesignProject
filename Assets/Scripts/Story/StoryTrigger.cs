using UnityEngine;

public abstract class StoryTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;
    private bool hasTriggered = false;
    public bool HasTriggered { get => hasTriggered; set => hasTriggered = value; }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasTriggered)
        {
            // if you dont want to check hasTriggered, you can remove it from the if statement
            // but be careful with the ~curstate in the ink story
            // you must reset it because old value from older choices will be stored
            TriggerResult();
            hasTriggered = true;
        }
    }

    public abstract void TriggerResult();

}
