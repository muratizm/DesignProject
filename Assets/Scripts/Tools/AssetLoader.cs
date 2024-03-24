using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    private static AssetLoader _assetLoader;

    public static AssetLoader Instance { get { return _assetLoader; } private set { return; } }

    void Awake()
    {
        if (_assetLoader != null)
        {
            Debug.Log("There is already an instance of AssetLoader in the scene");
            Destroy(gameObject);
            return;
        }
        _assetLoader = this;
    }

    public void LoadAssetAsync<T>(string label, System.Action<T> onComplete)
    {
        Addressables.LoadAssetAsync<T>(label).Completed += (AsyncOperationHandle<T> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onComplete?.Invoke(handle.Result);
            }
            else
            {
                Debug.LogError($"Failed to load asset at address: {label}");
                onComplete?.Invoke(default);
            }
        };
    }
}