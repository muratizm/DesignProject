using UnityEngine;
using System.Collections.Generic;
using System;


public static class AssetLibrary
{
    //cannot use a generic type parameter T in a static class or as a static field
    //this is why we use UnityEngine.Object
    public static Dictionary<string, UnityEngine.Object> assetLibrary = new Dictionary<string, UnityEngine.Object>();

    public static T GetAsset<T>(string key) where T : UnityEngine.Object
    {
        if (assetLibrary.ContainsKey(key))
        {
            return assetLibrary[key] as T;
        }
        else
        {
            T asset = Resources.Load<T>(key);
            if (asset == null)
            {
                throw new Exception($"Asset with key {key} not found");
            }
            assetLibrary.Add(key, asset);
            return asset;
        }
    }

    public static Sprite GetSprite(string key)
    {
        return GetAsset<Sprite>(key);
    }

    public static AudioClip GetAudioClip(string key)
    {
        return GetAsset<AudioClip>(key);
    }

    public static Material GetMaterial(string key)
    {
        return GetAsset<Material>(key);
    }

}
