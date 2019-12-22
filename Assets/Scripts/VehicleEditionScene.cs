using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEditionScene : MonoBehaviour
{
	public const string Name = "VehicleEdition";

	public static bool CreateDefaultVehicleOnNextStart = false;

    void Start()
    {
        if (CreateDefaultVehicleOnNextStart)
		{
			Instantiate(Prefabs.DefaultVehicle);
			CreateDefaultVehicleOnNextStart = false;
		}
    }

    void Update()
    {
        
    }
}
