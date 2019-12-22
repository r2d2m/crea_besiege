using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleEditionOptions
{
	private bool createDefaultVehicleOnNextStart = false;
	private string vehicleToLoadOnNextStart = null;

	private void Reset()
	{
		this.createDefaultVehicleOnNextStart = false;
		this.vehicleToLoadOnNextStart = null;
	}

	public bool CreateDefaultVehicleOnNextStart
	{
		get => this.createDefaultVehicleOnNextStart;
		set
		{
			Reset();
			this.createDefaultVehicleOnNextStart = value;
		}
	}

	public string VehicleToLoadOnNextStart
	{
		get => this.vehicleToLoadOnNextStart;
		set
		{
			Reset();
			this.vehicleToLoadOnNextStart = value;
		}
	}
}

public class VehicleEditionScene : MonoBehaviour
{
	public const string Name = "VehicleEdition";

	public static VehicleEditionOptions Options = new VehicleEditionOptions();

	void Start()
    {
		if (Options != null)
		{
			if (Options.VehicleToLoadOnNextStart != null)
			{
				VehicleLoader.Load(Options.VehicleToLoadOnNextStart);
				Options.VehicleToLoadOnNextStart = null;
			}
			if (Options.CreateDefaultVehicleOnNextStart)
			{
				Instantiate(Prefabs.DefaultVehicle);
				Options.CreateDefaultVehicleOnNextStart = false;
			}
		}
    }

    void Update()
    {
        
    }
}
