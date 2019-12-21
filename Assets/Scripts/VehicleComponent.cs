using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleComponent : MonoBehaviour
{
	Vehicle vehicle;
	uint id;

	public virtual void Setup(Vehicle vehicle)
	{
		this.id = vehicle.GenerateID();
		this.vehicle = vehicle;
	}

	public Vehicle Vehicle
	{
		get => this.vehicle;
	}

	public uint ID
	{
		get => this.id;
	}
}
