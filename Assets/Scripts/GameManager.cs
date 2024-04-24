using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance  { get; private set; }
    private GameObject player;
    private SceneCoordinator sceneCoordinator;
    public bool IsGamePaused;





    private GameManager(){}

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning("Found more than one GameManager. Destroying the duplicate.");
            Destroy(gameObject);
        }
        Instance = this;
        
    }


    void Start(){
        // initialize variables
        sceneCoordinator = SceneCoordinator.Instance;
        player = GameObject.FindGameObjectWithTag(Constants.Tags.PLAYER_TAG);
        SceneCoordinator.Instance.LockCursor();

        if(SceneManager.GetActiveScene().name != Constants.Scenes.SCENE_HOME){

            // load game
            LoadPrefs();
        }

    }

    void Update(){
        
        // test new feature
        if(Input.GetKeyDown(KeyCode.N)){
            Debug.Log("New Feature pressed");
            StoryOperations.Instance.RestartGame();
        }


        // check if escape is pressed no matter what
        if(Input.GetKeyDown(KeyCode.Escape)){
            sceneCoordinator.PressedEscape();
        }

        
        if(Input.GetKeyDown(KeyCode.Tab)){ 
            // tasks open only if the game is not paused
            sceneCoordinator.OnPressedTAB();
        }



        // if game is paused, dont do anything else than checking for escape
        if(IsGamePaused){ return; }

        // if game is not paused, do the following
        // check for other inputs



        

        if(Input.GetKeyDown(KeyCode.K)){
            // inventory open only if the game is not paused
            TasksManager.Instance.RemoveTask(TasksManager.Instance.tasks[0]);
        }

    }

    public void SavePrefs(){ // called when the player presses the save button in the pause panel
        Debug.Log("Game Saved");

        // player related data

        // save position, health, items, money, current story, all past decisions, etc.
        PlayerPrefs.SetFloat("PlayerPositionX", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerPositionY", player.transform.position.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", player.transform.position.z);
        
        InventoryManager.Instance.SaveInventory();
        //PlayerPrefs.SetString("CurrentStory", currentStory);

        // no need to save settings, as they are saved when the player presses the save button in the settings panel


        
        PlayerPrefs.Save();
    }

    public void LoadPrefs(){
        Debug.Log("Game Loaded");
        // load position, health, items, money, current story, all past decisions, etc.
        player.transform.position = new Vector3(
            PlayerPrefs.GetFloat("PlayerPositionX", Constants.Positions.PLAYER_START_DEFAULT_X),
            PlayerPrefs.GetFloat("PlayerPositionY", Constants.Positions.PLAYER_START_DEFAULT_Y),
            PlayerPrefs.GetFloat("PlayerPositionZ", Constants.Positions.PLAYER_START_DEFAULT_Z));
        
        InventoryManager.Instance.LoadInventory();
    }

    public void ResetPrefs(){
        Debug.Log("Game Reset");
        // reset position, health, items, money, current story, all past decisions, etc.
        player.transform.position = new Vector3(
            Constants.Positions.PLAYER_START_DEFAULT_X,
            Constants.Positions.PLAYER_START_DEFAULT_Y,
            Constants.Positions.PLAYER_START_DEFAULT_Z);
        
        InventoryManager.Instance.ResetInventory();
    }


    void OnDestroy(){
    }

}

