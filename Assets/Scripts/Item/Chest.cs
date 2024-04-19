using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private bool _isPlayerInRange = false;
    private bool _isChestOpened = false;
    private Animator _chestAnimator;

    private void Start()
    {
        _chestAnimator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            if (!_isChestOpened)
            {
                Debug.Log("Chest is not opened");
                OpenChest();
            }
            else
            {
                CloseChest();
            }
        }
    }

    private void OpenChest()
    {
        _chestAnimator.Play("ChestOpening");
        _isChestOpened = true;
    }

    private void CloseChest()
    {
        _chestAnimator.Play("ChestClosing");
        _isChestOpened = false;
    }


}
