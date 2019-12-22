using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleSpawner : MonoBehaviour
{
	public static string Pick = null;

    void Start()
    {
        if (Pick != null)
		{
			Vehicle.CreateFromJson(Pick);
		}
    }

    void Update()
    {
        
    }
}
