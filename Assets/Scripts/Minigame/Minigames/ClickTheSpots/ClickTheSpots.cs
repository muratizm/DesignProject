using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ClickTheSpots : MiniGame
{
    [SerializeField] private Image backgroundItem;
    [SerializeField] private Sprite itemSprite;
    List<Spot> spots = new List<Spot>();
    private string type;


    public override void StartGame(){

        InitializeBackgroundItem();

        if (type == "Bug"){
            GenerateSpots<Bug>(5);
        }
        else if (type == "Light"){
            GenerateSpots<Light>(5);
        }
        else {
            Debug.LogError("Invalid type");
        }

        timeToFinish = Constants.Durations.TIME_TO_FINISH_CLICKTHESPOTS;
        base.StartGame();
    }


    public void SetType(string type){
        this.type = type;
    }




    private void GenerateSpots<T>(int number) where T : Spot
    {
        for (int i = 0; i < number; i++)
        {
            GameObject spotObject = new GameObject(typeof(T).Name);
            spotObject.transform.SetParent(gameObject.transform);
            T spot = spotObject.AddComponent<T>();
            spot.Create(this, backgroundItem);
            spots.Add(spot);
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
    


    public void OnClickSpot(Spot spot)
    {
        spots.Remove(spot);
        if (spots.Count == 0)
        {
            ExitGame(true);
        }
    }

}


public class SpotGenerator<T> where T : Spot
{
    private ClickTheSpots clickTheSpots;
    private Sprite spotSprite;
    private Image backgroundItem;

    public SpotGenerator(ClickTheSpots clickTheSpots, Sprite spotSprite, Image backgroundItem)
    {
        this.clickTheSpots = clickTheSpots;
        this.spotSprite = spotSprite;
        this.backgroundItem = backgroundItem;
    }

    public void Generate(int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject spotObject = new GameObject(typeof(T).Name);
            spotObject.transform.SetParent(clickTheSpots.transform);
            T spot = spotObject.AddComponent<T>();
            spot.Create(clickTheSpots, this.backgroundItem);
        }
    }
}