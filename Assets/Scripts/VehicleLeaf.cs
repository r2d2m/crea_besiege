using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleLeafSeed : VehicleComponentSeed
{
	public uint linkedId = uint.MaxValue;

	public VehicleLeafSeed()
	{

	}

	public VehicleLeafSeed(VehicleLeafSeed other) : base(other)
	{
		this.linkedId = other.linkedId;
	}

	public VehicleLeafSeed(VehicleComponentSeed parent) : base(parent)
	{

	}

	public new bool IsDataValid
	{
		get => this.linkedId != uint.MaxValue && base.IsDataValid;
	}

	public new string ToJson()
	{
		Debug.Assert(this.IsDataValid);
		return JsonUtility.ToJson(this, true);
	}

	public static new VehicleLeafSeed FromJson(string json)
	{
		return JsonUtility.FromJson<VehicleLeafSeed>(json);
	}
}

public class VehicleLeaf : VehicleComponent, IAttachable
{
	Block linkedBlock;

	protected new VehicleLeafSeed Seed
	{
		get
		{
			var data = new VehicleLeafSeed(base.Seed);
			data.linkedId = this.linkedBlock.ID;

			return data;
		}
	}

	public virtual void Setup(Block block, Vector3 direction)
	{
		this.linkedBlock = block;
	}

	public override void Setup(string json)
	{
		base.Setup(json);
	}

	public override string ToJson()
	{
		// Not calling base class method is intentional

		return this.Seed.ToJson();
	}

	public VehicleComponent VehicleComponent
	{
		get => this;
	}

	public Block LinkedBlock
	{
		get => this.linkedBlock;
	}
}