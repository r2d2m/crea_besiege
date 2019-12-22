using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEditionScene : MonoBehaviour
{
	public const string Name = "VehicleEdition";

	public static string ToLoadOnNextStart = null;

	void Start()
    {
		if (ToLoadOnNextStart != null)
		{
			VehicleIO.Load(ToLoadOnNextStart);
			ToLoadOnNextStart = null;
		}
		else
		{
			Vehicle.CreateDefault();
		}
    }

    void Update()
    {
        
    }
}
