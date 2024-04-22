using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class AI : MonoBehaviour
{
    private static AI instance;
    public static AI Instance { get { return instance; } }

    public int AskAI(Story story)
    {
        int randomNumber = Random.Range(1, 4);
        return randomNumber;
    }
}
