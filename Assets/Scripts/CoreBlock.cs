using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBlockSeed : BlockSeed
{
	private const VehicleComponentType Type = VehicleComponentType.CoreBlock;

	public CoreBlockSeed()
	{
		this.type = Type;
	}

	public CoreBlockSeed(BlockSeed parent) : base(parent)
	{
		this.type = Type;
	}

	public static new CoreBlockSeed FromJson(string json)
	{
		return JsonUtility.FromJson<CoreBlockSeed>(json);
	}
}

public class CoreBlock : Block
{
	protected new CoreBlockSeed Seed
	{
		get => new CoreBlockSeed(base.Seed);
	}

	public override string ToJson()
	{
		// Not calling base class method is intentional

		return this.Seed.ToJson();
	}

	public override void Setup(string json)
	{
		base.Setup(json);
	}
}
