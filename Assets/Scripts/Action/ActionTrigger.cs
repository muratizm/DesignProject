using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    private bool playerInRange;
    private BaseAction action;


    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    void Awake()
    {
        playerInRange = false;
        action = GetComponent<BaseAction>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            ActionManager.Instance.EnterActionMode(inkJSON, action);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


}
