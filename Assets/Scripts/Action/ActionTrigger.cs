using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class ActionTrigger : MonoBehaviour
{
    private BaseAction action;


    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    void Awake()
    {
        action = GetComponent<BaseAction>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ActionManager.Instance.EnterActionMode(inkJSON, action);
        }
    }

    private void OnTriggerExit(Collider other)
    {
    }


}
