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

	public void AssertValidData()
	{
		Debug.Assert(this.type != VehicleComponentType.Null);
		Debug.Assert(this.id != uint.MaxValue);
		Debug.Assert(this.localPosition != Helper.MaxVector3);
		Debug.Assert(!this.localRotation.Equals(Helper.MaxQuaternion));
	}

	public string ToJson()
	{
		AssertValidData();
		return JsonUtility.ToJson(this, true);
	}
}

public class VehicleComponent : MonoBehaviour, IJsonSerializable
{
	Vehicle vehicle;
	uint id;

	public virtual void Setup(Vehicle vehicle)
	{
		this.id = vehicle.GenerateID();
		this.vehicle = vehicle;
	}

	public virtual string ToJson()
	{
		return this.Seed.ToJson();
	}

	protected VehicleComponentSeed Seed
	{
		get
		{
			var data = new VehicleComponentSeed();
			data.id = this.id;
			data.localPosition = this.transform.localPosition;
			data.localRotation = this.transform.localRotation;
			data.type = VehicleComponentType.VehicleComponent;

			return data;
		}
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
