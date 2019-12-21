using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PatchDayOneDLC
{
	private SolidBlock solidBlock = null;
	private bool isLoaded = false;

	private void FetchDatas(AssetBundle bundle)
	{
		this.solidBlock = bundle.LoadAsset<SolidBlock>("SolidBlockDLC");
	}

	public void Load()
	{
		if (!this.isLoaded)
		{
			string path = Path.Combine(Application.streamingAssetsPath, "patch_day_one");

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

	public SolidBlock SolidBlock
	{
		get => this.solidBlock;
	}
}
