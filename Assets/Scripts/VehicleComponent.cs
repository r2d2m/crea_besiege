using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum VehicleComponentType
{
	Null,
	VehicleComponent,
	CoreBlock,
	AttachableBlock,
	Wheel,
	Booster
}

public class VehicleComponentSerializableData
{
	public uint id = uint.MaxValue;
	public Vector3 localPosition = Helper.MaxVector3;
	public Quaternion localRotation = Helper.MaxQuaternion;
	public VehicleComponentType type = VehicleComponentType.Null;

	public VehicleComponentSerializableData()
	{

	}

	public VehicleComponentSerializableData(VehicleComponentSerializableData other)
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
		return this.SerializableData.ToJson();
	}

	protected VehicleComponentSerializableData SerializableData
	{
		get
		{
			var data = new VehicleComponentSerializableData();
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
