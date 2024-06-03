using UnityEngine;
using System.Net.Http;
using System.Threading.Tasks;
using System.Diagnostics;
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
        
        UnityEngine.Debug.Log("Message sent to Llama: " + msg);
        var json = CreateJsonMessage(msg);
        
        var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync("http://127.0.0.1:5000/api_1", content);

        if (response.IsSuccessStatusCode)
        {
            
            var jsonResponse = await response.Content.ReadAsStringAsync();
            UnityEngine.Debug.Log("Yanıt: " + jsonResponse);

            var responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);



            // Return the personality type
            return "I would select " + responseData.choice +
             ".\nBecause: " + responseData.explanation; 
        }
        else
        {
            UnityEngine.Debug.LogError("API isteği başarısız oldu: " + response.StatusCode);
            return "In this case, I can't decide what to choose. Hmm I guess I'll leave it to you.";
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
        public string choice;
        public string explanation;
    }

}
