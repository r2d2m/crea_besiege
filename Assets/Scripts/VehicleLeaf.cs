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

	public new void AssertValidData()
	{
		base.AssertValidData();

		Debug.Assert(this.linkedId != uint.MaxValue);
	}

	public new string ToJson()
	{
		AssertValidData();
		return JsonUtility.ToJson(this, true);
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
		base.Setup(block.Vehicle);

		this.linkedBlock = block;
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