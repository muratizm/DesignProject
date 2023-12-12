using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelText : MonoBehaviour
{
    Text textFile;
    // Start is called before the first frame update
    void Start()
    {
        textFile = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textFile.text = "Level : " + LevelGenerator.Level.ToString();
    }
}
