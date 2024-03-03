using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Scripting;

public class StoryOperations : MonoBehaviour
{
    public static StoryOperations Instance { get; private set; }
    [SerializeField] private GameObject obstacle;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("found more than one StoryOperations.");
            Destroy(gameObject);
        }
    }



    public void DeleteObjects(string name)
    {
        //delete every object that needed in this state
        Destroy(GameObject.Find(name));
    }
    
    public void GetRidOfTheObstacle(){
        if(obstacle != null){
             Debug.Log("Obstacle is gone");

            obstacle.GetComponent<Animation>().Play("anim");
        }
    }


}