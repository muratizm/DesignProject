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
        GameManager.Instance.RestartGame();
    }

    public void DeleteObjects(string name)
    {
        //delete every object that needed in this state
        Destroy(GameObject.Find(name));
    }

    public void GiveRandomRing(){
        if(rings.Count > 0){
            int randomIndex = Random.Range(0, rings.Count);
            Player.Instance.TakeItem(rings[randomIndex]);
            rings.RemoveAt(randomIndex);
        }
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