using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditionGameMode : MonoBehaviour
{
	private void Start()
	{
		GameEvents.OnEditionQuit += OnEditionQuit;
	}

	private void OnDestroy()
	{
		if (GameEvents.InstanceExists)
		{
			GameEvents.OnEditionQuit -= OnEditionQuit;
		}
	}

	private void OnEditionQuit()
	{
		VehicleGameMode.Create();
		Destroy(this.gameObject);
	}
}
