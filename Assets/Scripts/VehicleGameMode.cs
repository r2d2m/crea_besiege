using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleGameMode : MonoBehaviour
{
	public static VehicleGameMode Create()
	{
		return Instantiate(Prefabs.VehicleGameMode);
	}
}
