using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VehicleComponentType
{
	Null = -1,
	VehicleComponent,
	CoreBlock,
	AttachableBlock,
	Wheel,
	Booster
}

public class VehicleComponentSeed
{
	private const VehicleComponentType Type = VehicleComponentType.VehicleComponent;

	public uint id = uint.MaxValue;
	public Vector3 localPosition = Helper.MaxVector3;
	public Quaternion localRotation = Helper.MaxQuaternion;
	public VehicleComponentType type = VehicleComponentType.Null;

	public VehicleComponentSeed()
	{
		this.type = Type;
	}

	public VehicleComponentSeed(VehicleComponentSeed other)
	{
		this.id = other.id;
		this.localPosition = other.localPosition;
		this.localRotation = other.localRotation;
		this.type = other.type;
	}

	public bool IsDataValid
	{
		get => this.type != VehicleComponentType.Null
			&& this.id != uint.MaxValue
			&& this.localPosition != Helper.MaxVector3
			&& !this.localRotation.Equals(Helper.MaxQuaternion);
	}

	public string ToJson()
	{
		Debug.Assert(this.IsDataValid);
		return JsonUtility.ToJson(this, true);
	}

	public static VehicleComponentSeed FromJson(string json)
	{
		var seed = JsonUtility.FromJson<VehicleComponentSeed>(json);

		if (!seed.IsDataValid)
		{
			throw new Exception("Invalid data in json file : " + json);
		}

		return seed;
	}
}

public class VehicleComponent : MonoBehaviour, IJsonSerializable
{
	Vehicle vehicle = null;
	uint id = uint.MaxValue;

	private bool IsIdentitySet
	{
		get => this.id != uint.MaxValue && this.vehicle != null;
	}

	protected VehicleComponentSeed Seed
	{
		get
		{
			var data = new VehicleComponentSeed();
			data.id = this.id;
			data.localPosition = this.transform.localPosition;
			data.localRotation = this.transform.localRotation;

			return data;
		}
	}

	public void SetIdentity(Vehicle vehicle, uint id)
	{
		this.id = id;
		this.vehicle = vehicle;
	}

	public virtual void Setup(string json)
	{
		Debug.Assert(this.IsIdentitySet);

		var seed = VehicleComponentSeed.FromJson(json);

		Debug.Assert(this.id == seed.id, "ID mismatch");

		this.transform.localPosition = seed.localPosition;
		this.transform.localRotation = seed.localRotation;
	}

	public virtual string ToJson()
	{
		return this.Seed.ToJson();
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
