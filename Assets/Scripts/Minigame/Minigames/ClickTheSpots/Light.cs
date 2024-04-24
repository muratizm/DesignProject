using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class Light : Spot { 


    public override void Create(ClickTheSpots clickTheSpots, Image backgroundItem)
    {
        base.Create(clickTheSpots, backgroundItem);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Light");
        StartEffect();
    }


    private void StartEffect(){
        StartCoroutine(ChangeAlpha());
    }


    private IEnumerator ChangeAlpha()
    {
        Image image = GetComponent<Image>();
        float duration = 0.5f; // Half of the total duration

        // Increase alpha from 0 to 1
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            Color color = image.color;
            color.a = t / duration;
            image.color = color;
            yield return null;
        }

        // Decrease alpha from 1 to 0
        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            Color color = image.color;
            color.a = 1 - (t / duration);
            image.color = color;
            yield return null;
        }
    }

}