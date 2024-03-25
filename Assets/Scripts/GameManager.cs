using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
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
        
        SceneCoordinator.Instance.LockCursor();
    }


    void Start(){
        // initialize variables
        sceneCoordinator = SceneCoordinator.Instance;
        player = GameObject.FindGameObjectWithTag("Player");

        // load game
        LoadGame();

    }

    void Update(){
        // test new feature
        if(Input.GetKeyDown(KeyCode.N)){
            Debug.Log("New Feature pressed");
            MinigameManager.Instance.StartMinigame(MinigameManager.MinigameType.ClickRush);
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

    public void SaveGame(){ // called when the player presses the save button in the pause panel
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

    public void LoadGame(){
        Debug.Log("Game Loaded");
        // load position, health, items, money, current story, all past decisions, etc.
        player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPositionX"), PlayerPrefs.GetFloat("PlayerPositionY"), PlayerPrefs.GetFloat("PlayerPositionZ"));
        
        InventoryManager.Instance.LoadInventory();

        //currentStory = PlayerPrefs.GetString("CurrentStory");
    }



    void OnDestroy(){
    }

}

