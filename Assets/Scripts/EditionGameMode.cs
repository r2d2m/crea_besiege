using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditionGameMode : MonoBehaviour
{
    void Start()
    {
		GameEvents.OnGameStart += OnGameStart;
    }

	private void OnDestroy()
	{
		if (GameEvents.InstanceExists)
		{
			GameEvents.OnGameStart -= OnGameStart;
		}
	}

	private void OnGameStart()
	{
		Controller.Create();
		Destroy(this.gameObject);
	}
}
