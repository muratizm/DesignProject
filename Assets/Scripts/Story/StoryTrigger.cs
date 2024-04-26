using UnityEngine;

public abstract class StoryTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;
    private bool canTrigger = true;
    public bool CanTrigger { get => canTrigger; set => canTrigger = value; }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER_TAG) && true)
        {
            // if you dont want to check hasTriggered, you can remove it from the if statement
            // but be careful with the ~curstate in the ink story
            // you must reset it because old value from older choices will be stored
            TriggerResult();
            canTrigger = false;
        }
    }

    public abstract void TriggerResult();

}
