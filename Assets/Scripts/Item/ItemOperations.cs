using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Scripting;

public class ItemOperations : MonoBehaviour
{
    public static ItemOperations Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("found more than one StoryOperations.");
            Destroy(gameObject);
        }
    }


    public void UseBookItem(Sprite[] pages)
    {
        // show book
        SceneCoordinator.Instance.OpenBookPanel(pages);
    }
    
    public void UseOmniverseItem(float maxSize)
    {
        StartCoroutine(ShowEverywhere(maxSize));
    }

    IEnumerator ShowEverywhere(float maxSize)
    {
        Camera minimapCamera = GameObject.Find("MinimapCamera").GetComponent<Camera>();
        float initialSize = minimapCamera.orthographicSize;

        while(minimapCamera.orthographicSize < maxSize)
        {
            minimapCamera.orthographicSize += 1;
            yield return new WaitForSeconds(0.01f);
        }
        while(minimapCamera.orthographicSize > initialSize)
        {
            minimapCamera.orthographicSize -= 1;
            yield return new WaitForSeconds(0.01f);
        }
        yield return null;
    }

}