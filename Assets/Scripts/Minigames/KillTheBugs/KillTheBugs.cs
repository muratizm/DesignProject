using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class KillTheBugs : MiniGame
{
    [SerializeField] private Image backgroundItem;
    [SerializeField] private Sprite itemSprite; 
    [SerializeField] private Sprite bugSprite;
    List<Bug> bugs = new List<Bug>();


    public override void StartGame(){

        InitializeBackgroundItem();
        GenerateBugs(5);

        timeToFinish = 10;
        base.StartGame();
    }


    private void GenerateBugs(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject bugObject = new GameObject("Bug");
            bugObject.transform.SetParent(gameObject.transform);
            Bug bug = bugObject.AddComponent<Bug>();
            bug.Create(this, bugSprite, backgroundItem);
            bugs.Add(bug);
        }
    }


    // This method initializes the background item with aspect ratio of the sprite
    private void InitializeBackgroundItem()
    { 
        backgroundItem = gameObject.transform.Find("BackgroundItem").GetComponent<Image>();
        backgroundItem.sprite = itemSprite;

        // Get the RectTransform of the GameObject
        RectTransform rectBackground = backgroundItem.GetComponent<RectTransform>();

        // Calculate the aspect ratio of the sprite
        float spriteAspectRatio = (float)itemSprite.texture.width / itemSprite.texture.height;


        float newHeight = rectBackground.sizeDelta.y; // new height is the same as the original height
        float newWidth = newHeight * spriteAspectRatio; // adjust the width according to the aspect ratio

        // Apply the new width and height
        rectBackground.sizeDelta = new Vector2(newWidth, newHeight);
    }


    public void OnClickBug(Bug bug)
    {
        bugs.Remove(bug);
        if (bugs.Count == 0)
        {
            ExitGame(true);
        }
    }

}