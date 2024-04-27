using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private GameObject ss1_goldenLeaf;

    
    [SerializeField] private GameObject ss2_rat;

    [SerializeField] private ItemSO ss3_map;

    [SerializeField] private List<ItemSO> rings;

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


    public void RestartGame()
    {
        SceneCoordinator.Instance.OpenScene(Constants.Scenes.SCENE_PLAY);
        GameManager.Instance.ResetPrefs();
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

    public void GoldenLeafFall(){
        if(ss1_goldenLeaf != null){
            Debug.Log("Golden Leaf is falling!");

            Rigidbody rb = ss1_goldenLeaf.AddComponent<Rigidbody>();
        }
    }

    public void DisableRatAction(){
        if(ss2_rat != null){
            Debug.Log("Rat is not interacting!");
            ss2_rat.GetComponentInChildren<ActionTrigger>().CanTrigger = false;
        }
    }

    public void DisableRatDialogue(){
        if(ss2_rat != null){
            Debug.Log("Rat is not talking!");
            ss2_rat.GetComponentInChildren<DialogueTrigger>().CanTrigger = false;
        }
    }

    public void GiveRandomRing(){
        if(rings.Count > 0){
            int randomIndex = Random.Range(0, rings.Count);
            Player.Instance.TakeItem(rings[randomIndex]);
            rings.RemoveAt(randomIndex);
        }
    }

    public void GiveMap(){
        Player.Instance.TakeItem(ss3_map);
    }
    
    public void EnterRatAction(){
        if(ss2_rat != null){
            Debug.Log("Rat is interacting!");
            ActionManager.Instance.EnterActionMode(ss2_rat.GetComponentInChildren<ActionTrigger>().inkJSON, ss2_rat.GetComponentInChildren<BaseAction>());
        }
    }

    public void EnterRatDialogue(){
        if(ss2_rat != null){
            Debug.Log("Rat is talking!");
            DialogueManager.Instance.EnterDialogueMode(ss2_rat.GetComponentInChildren<DialogueTrigger>().inkJSON);
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

    public void AddTask3(){
        Task3 task3 = gameObject.AddComponent<Task3>();
        task3.taskName = "Task3 Task3 ";
        task3.TimeToAchieve = 370f;
        tasksManager.AddTask(task3);
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