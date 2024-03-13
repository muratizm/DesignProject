using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class KillTheBugs : MonoBehaviour
{
    [SerializeField] private Image item;
    [SerializeField] private Image[] bugs;

    private int totalBugs;
    private int deadBugs = 0;

    private Sprite itemSprite; 
    private Sprite bugSprite;

    [SerializeField] private float timeToClean = 2f;

    private void Start()
    {
        GameManager.Instance.IsGamePaused = true;
        
        // Initialization code goes here
        itemSprite = Resources.Load<Sprite>("item");
        bugSprite = Resources.Load<Sprite>("bug");
        Debug.Log("itemSprite: " + itemSprite);
        Debug.Log("bugSprite: " + bugSprite);

        totalBugs = bugs.Length;

        item.sprite = itemSprite;
        GenerateBugs();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }


    private void Update()
    {
        RandomWalk();


    }

    private void GenerateBugs()
    {
        for (int i = 0; i < totalBugs; i++)
        {
            bugs[i].gameObject.SetActive(true);
            bugs[i].sprite = bugSprite;
        }
    }

    private void RandomWalk(){
        // change bugs position
        for (int i = 0; i < totalBugs; i++)
        {
            bugs[i].transform.position += new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0f) * Time.deltaTime;
        }
    }


    public void OnClickBug(GameObject bug)
    {
        Debug.Log("OnClickBug");
        Debug.Log("bug is dead");
        deadBugs++;
        if (deadBugs == totalBugs)
        {
            FinishGame();
        }

        // Disable the bug GameObject
        bug.SetActive(false);
    }




    private void FinishGame()
    {
        Debug.Log("Game finished");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}