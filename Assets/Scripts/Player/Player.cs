using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player player;
    public static Player Instance { get { return player;} private set { return;} }

    void Awake()
    {
        if (player != null)
        {
            Destroy(gameObject);
            return;
        }
        player = this;
        
    }

    void Start()
    {
        
    }




    void Update()
    {
        
    }
}
