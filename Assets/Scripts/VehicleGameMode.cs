using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleGameMode : MonoBehaviour
{
    void Start()
    {
		Physics.gravity = new Vector3(0, -9.81f, 0);
	}

    void Update()
    {
        
    }

	public static VehicleGameMode Create()
	{
		return Instantiate(Prefabs.VehicleGameMode);
	}
}
