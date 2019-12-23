using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditionGameMode : MonoBehaviour
{
    void Start()
    {
		GameEvents.OnVehicleControl += OnVehicleControl;
    }

	private void OnDestroy()
	{
		if (GameEvents.InstanceExists)
		{
			GameEvents.OnVehicleControl -= OnVehicleControl;
		}
	}

	private void OnVehicleControl()
	{
		VehicleGameMode.Create();
		Destroy(this.gameObject);
	}
}
