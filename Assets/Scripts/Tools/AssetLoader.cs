using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    private static AssetLoader _assetLoader;

    public static AssetLoader Instance { get { return _assetLoader; } private set { return; } }

    public void LoadAssetAsync<T>(string address, System.Action<T> onComplete)
    {
        Addressables.LoadAssetAsync<T>(address).Completed += (AsyncOperationHandle<T> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                onComplete?.Invoke(handle.Result);
            }
            else
            {
                Debug.LogError($"Failed to load asset at address: {address}");
                onComplete?.Invoke(default);
            }
        };
    }
}