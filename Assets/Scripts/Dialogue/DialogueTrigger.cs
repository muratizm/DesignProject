using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool playerInRange;
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

    void Awake()
    {
        playerInRange = false;

    }

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // Eğer oyuncu etkileşim bölgesine girdiyse, diyalogu başlat
            DialogueManager.Instance.EnterDialogueMode(inkJSON);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void StartDialogue()
    {
        if (playerInRange)
        {
            // Burada diyalogu göstermek, bir arayüz açmak veya başka bir etkileşim gerçekleştirmek için gerekli kodu ekleyebilirsiniz.
            Debug.Log(inkJSON.text);
        }
    }


}
