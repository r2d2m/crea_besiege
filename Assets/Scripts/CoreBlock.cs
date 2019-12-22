using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBlockSeed : BlockSeed
{
	public CoreBlockSeed()
	{
	}

	public CoreBlockSeed(BlockSeed parent) : base(parent)
	{
	}
}

public class CoreBlock : Block
{
	protected new CoreBlockSeed Seed
	{
		get
		{
			var data = new CoreBlockSeed(base.Seed);
			data.type = VehicleComponentType.CoreBlock;

			return data;
		}
	}

	public override void Setup(Vehicle vehicle)
	{
		base.Setup(vehicle);
	}

	public override string ToJson()
	{
		// Not calling base class method is intentional

		return this.Seed.ToJson();
	}

	public static CoreBlockSeed FromJson(string json)
	{
		return JsonUtility.FromJson<CoreBlockSeed>(json);
	}
}
