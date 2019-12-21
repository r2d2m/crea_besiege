using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatchDayOneDLC
{
	private GameObject solidBlock = null;
	private bool isLoaded = false;

	private void FetchDatas(AssetBundle bundle)
	{
		this.solidBlock = bundle.LoadAsset<GameObject>("SolidBlockDLC");
	}

	public void Load()
	{
		if (!this.isLoaded)
		{
			AssetBundle bundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "patch_day_one"));
			if (bundle != null)
			{
				FetchDatas(bundle);

				this.isLoaded = true;
			}
		}
	}

	public bool IsLoaded
	{
		get => this.isLoaded;
	}

	public GameObject SolidBlock
	{
		get => this.solidBlock;
	}
}
