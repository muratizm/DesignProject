using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThoughtTrigger : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJSON;

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Eğer oyuncu etkileşim bölgesine girdiyse, diyalogu başlat
            ThoughtManager.Instance.EnterThoughtBubble(inkJSON);
        }
    }

}
