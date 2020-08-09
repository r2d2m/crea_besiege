using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatchDayOneDLC
{
    public const string FileName = "patch_day_one";

    private AttachableBlock block = null;
    private bool isLoaded = false;

    private T FetchObject<T>(AssetBundle bundle, string name) where T : Object
    {
        var asset = bundle.LoadAsset<T>(name);
        if (asset == null)
        {
            Debug.LogError("Failed to load object " + name + " of type " + typeof(T).ToString() + " in AssetBundle " + bundle);
        }

        return asset;
    }

    private T FetchComponent<T>(AssetBundle bundle, string name) where T : Component
    {
        var component = FetchObject<GameObject>(bundle, name).GetComponent<T>();
        if (component == null)
        {
            Debug.LogError("Could not find component of type " + typeof(T).ToString() + " in GameObject " + name + " from AssetBundle " + bundle);
        }

        return component;
    }

    private void FetchDatas(AssetBundle bundle)
    {
        this.block = FetchComponent<AttachableBlock>(bundle, "BlockDLC");
    }

    public void Load()
    {
        if (!this.isLoaded)
        {
            string path = Path.Combine(Application.streamingAssetsPath, FileName);

            if (File.Exists(path))
            {
                AssetBundle bundle = AssetBundle.LoadFromFile(path);
                FetchDatas(bundle);

                this.isLoaded = true;
            }
        }
    }

    public bool IsLoaded
    {
        get => this.isLoaded;
    }

    public AttachableBlock Block
    {
        get => this.block;
    }
}
