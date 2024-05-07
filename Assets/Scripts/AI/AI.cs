using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;


public class AI : MonoBehaviour
{   
    private static AI instance;
    public static AI Instance { get { return instance; } }


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }


    private static readonly HttpClient httpClient = new HttpClient();

    public async Task<string> OnApiCall(string msg)
    {
        
        Debug.Log("Message sent to Llama: " + msg);
        var json = CreateJsonMessage(msg);
        
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("http://127.0.0.1:5000/api_1", content);

        if (response.IsSuccessStatusCode)
        {
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Debug.Log("Yanıt: " + jsonResponse);

            var responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);
            Debug.Log("Personality Type: " + responseData.personality_type);

            Debug.Log(responseData.personality_type);

            // Return the personality type
            return responseData.personality_type;
        }
        else
        {
            Debug.LogError("API isteği başarısız oldu: " + response.StatusCode);
            return "API isteği başarısız oldu.";
        }
    }

    public string CreateJsonMessage(string message)
    {
        return $"{{\"message\":\"{message}\"}}";
    }



    // JSON yanıtını temsil edecek bir sınıf
    [System.Serializable]
    public class ResponseData
    {
        public string personality_type;
    }

}

// JSON yanıtını temsil edecek bir sınıf
[System.Serializable]
public class ResponseData
{
    public string personality_type;
}
