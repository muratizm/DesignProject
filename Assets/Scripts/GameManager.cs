using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private Camera playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        playerCamera = FindObjectOfType<Camera>();
        Debug.Log("aa" +  playerCamera);
    }

    // Update is called once per frame
    void Update()
    {
        // bunu update'ten çıkarıp atıyorum poke_name' değiştirildiği zaman güncelle dersen ne olur:
        // oyunu kapatıp açtığımızda bütün story global variable'ları playerprefsten geri yüklüyor. 
        string str = ((Ink.Runtime.StringValue) DialogueManager.GetInstance().GetStoryState("poke_name")).value;
        switch (str)
        {
            case "ahar":
                RenderSettings.skybox.SetColor("_Tint", Color.red);
                playerCamera.backgroundColor = Color.red;
                break;
            case "bhar":
                RenderSettings.skybox.SetColor("_Tint", Color.blue);
                playerCamera.backgroundColor = Color.blue;
                break;
            case "char":
                RenderSettings.skybox.SetColor("_Tint", Color.yellow);
                playerCamera.backgroundColor = Color.yellow;
                break;
            default:
                Debug.LogWarning("zort");
                break;
        }
    }
}
