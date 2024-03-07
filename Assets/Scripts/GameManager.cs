using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance ?? (instance = new GameManager());


    [SerializeField] GameObject aboutPanel;

    private GameManager(){}

    void Awake(){
        
    }

    void Start(){
    }


}
