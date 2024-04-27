using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LlamaAPI : MonoBehaviour
{
    public string llamaApiKey = "LL-tIiz5yAz3O9FCbO7yIKtC4heZhKKih3pZjrNQPekUlNwdFTHfLUSf6d5cIaZ12yI";
    public string model = "llama-13b-chat";
    public string prompt = "Identify the personality type based on the given message. Your prediction must match one of these personality types: ['Brave', 'Friendly', 'Helpful', 'Logical', 'Warrior'] Respond with only the matched personality type in the specified format ('personality type') without saying anything else like emojis. You have to return only the personality type as output. Message: {message}";

    private void Start()
    {
        Debug.Log("Making Llama API call...");
        StartCoroutine(MakeLlamaAPICall());
    }

    IEnumerator MakeLlamaAPICall()
    {
        string url = "https://api.llama-api.com/chat/completions";
        string jsonPayload = "{\"model\": \"" + model + "\", \"messages\": [{\"role\": \"system\", \"content\": \"You are a friendly chatbot.\"}, {\"role\": \"user\", \"content\": \"" + prompt + "\"}]}";

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonPayload))
        {
            www.SetRequestHeader("Content-Type", "application/json");
            www.SetRequestHeader("Authorization", "Bearer " + llamaApiKey);

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string responseText = www.downloadHandler.text;
                Debug.Log("Response: " + responseText);
                // Parse the response and extract the personality type
                string personalityType = ParseResponse(responseText);
                Debug.Log("Personality Type: " + personalityType);
            }
            else
            {
                Debug.LogError("Error: " + www.error);
            }
        }
    }

    private string ParseResponse(string responseText)
    {
        Debug.Log("Response: " + responseText);
        // Parse the JSON response and extract the personality type
        // Implement your parsing logic here
        return "Personality Type"; // Replace with actual parsed personality type
    }
}
