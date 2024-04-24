using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Chest : MonoBehaviour
{
    private ItemObject itemObject;
    private bool _isPlayerInRange = false;
    private bool _isChestOpened = false;
    private Animator _chestAnimator;

    private void Start()
    {
        _chestAnimator = GetComponent<Animator>();
        itemObject = gameObject.GetComponentInChildren<ItemObject>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tags.PLAYER_TAG))
        {
            _isPlayerInRange = true;
        }
        {
            _isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(Constants.Tags.PLAYER_TAG))
        {
            _isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (_isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed");
            if (_isChestOpened)
            {
                Debug.Log("Chest was opened for now");
                if(itemObject != null)
                {
                    Player.Instance.TakeItem(itemObject.Item);
                    Destroy(itemObject.gameObject);
                    itemObject = null;
                }
                else{
                    Debug.Log("Chest is closing");
                    CloseChest();   
                }
            }
            else
            {
                OpenChest();
            }
        }
    }

    private void OpenChest()
    {
        _chestAnimator.Play("ChestOpening");
        AudioManager.Instance.PlaySFX(Constants.Paths.Sounds.SFX.CHEST_OPEN);
        _isChestOpened = true;
    }

    private void CloseChest()
    {
        _chestAnimator.Play("ChestClosing");
        AudioManager.Instance.PlaySFX(Constants.Paths.Sounds.SFX.CHEST_CLOSE);
        _isChestOpened = false;
    }


}
