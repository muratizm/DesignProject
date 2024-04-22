using System.Collections;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Scripting;

public class StoryOperations : MonoBehaviour
{
    public static StoryOperations Instance { get; private set; }
    private StoryStateManager storyStateManager;
    private TasksManager tasksManager;

    public static class StoryState1
    {
        public const string NAME = "StoryState1";
        public const string SS1_OBSTACLE = "SS1_Obstacle";
        public const string SS1_BRANCH = "SS1_Branch";
    }
    [SerializeField] private GameObject ss1_obstacle;
    [SerializeField] private GameObject ss1_branch;

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

    void Start()
    {
        storyStateManager = StoryStateManager.Instance;
        tasksManager = TasksManager.Instance;
    }



    public void DeleteObjects(string name)
    {
        //delete every object that needed in this state
        Destroy(GameObject.Find(name));
    }
    
    
    public void GetRidOfTheObstacle(){
        if(ss1_obstacle != null){
            Debug.Log("Obstacle is falling!");
            ss1_obstacle.GetComponent<Animation>().Play("anim");
        }
    }

    public void BranchFall(){
        if(ss1_branch != null){
            Debug.Log("Branch is falling!");

            Rigidbody rb = ss1_branch.AddComponent<Rigidbody>();
            rb.mass = 100;
        }
    }

    public async void HomeSceneOpenBook(Sprite[] pages)
    {
        SceneCoordinator.Instance.FadeOut();
        await Task.Delay(Constants.Durations.FADEOUT_DURATION_MS);

        ItemOperations.Instance.UseBookItem(pages);
        
        SceneCoordinator.Instance.FadeIn();
        await Task.Delay(Constants.Durations.FADEIN_DURATION_MS);
    }

    public async void PassOut()
    {
        SceneCoordinator.Instance.FadeOut();
        await Task.Delay(Constants.Durations.FADEOUT_DURATION_MS);

        SceneCoordinator.Instance.OpenScene(Constants.Scenes.SCENE_PLAY);
    }

    public void AddTask1(){
        Task1 task1 = gameObject.AddComponent<Task1>();
        task1.taskName = "Task1 Task1 ";
        task1.TimeToAchieve = 70f;
        tasksManager.AddTask(task1);
    }
    

    public void UseOmniverseItem(float maxSize)
    {
        StartCoroutine(ShowEverywhere(maxSize));
    }


    IEnumerator ShowEverywhere(float maxSize)
    {
        Camera minimapCamera = GameObject.Find("MinimapCamera").GetComponent<Camera>();
        float initialSize = minimapCamera.orthographicSize;

        while(minimapCamera.orthographicSize < maxSize)
        {
            minimapCamera.orthographicSize += 1;
            yield return new WaitForSeconds(0.01f);
        }
        while(minimapCamera.orthographicSize > initialSize)
        {
            minimapCamera.orthographicSize -= 1;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

}