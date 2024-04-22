using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class AboutPanel : MonoBehaviour
{
    private TextMeshProUGUI creditsText;
        
        
    TextAsset creditsTextAsset;
    private string textFilePath = "AboutText";
    private bool textFinished = false;


    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            if(textFinished){
                textFinished = false;
                creditsText.text = "";
                gameObject.SetActive(false);
            }
            else{
                StopAllCoroutines(); // Stop the gradual display
                creditsText.text = creditsTextAsset.text; // Display the entire text
                textFinished = true;
            }
        }
    }

    IEnumerator LoadAndDisplayCredits()
    {


        if (creditsTextAsset == null)
        {
            Debug.LogError("Credits text file not found in Resources folder!");
            yield break; // Stop the coroutine
        }

        // Display the text letter by letter
        string credits = creditsTextAsset.text;
        creditsText.text = ""; // Clear the text display
        foreach (char letter in credits) 
        {
            creditsText.text += letter; 
            yield return new WaitForSeconds(Constants.Durations.WAIT_BETWEEN_LETTERS);
        }
        textFinished = true;
    }

    public void Activate(){
        gameObject.SetActive(true);
        
        if(!creditsTextAsset){
            // Load the text file from the Resources folder
            creditsTextAsset = Resources.Load<TextAsset>(textFilePath);
            creditsText = GetComponentInChildren<TextMeshProUGUI>();
        }


        StartCoroutine(LoadAndDisplayCredits());
    }

}
