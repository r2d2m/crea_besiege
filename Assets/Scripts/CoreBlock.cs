using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreBlock : Block
{
	protected new BlockSerializableData SerializableData
	{
		get
		{
			var data = base.SerializableData;
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

		return this.SerializableData.ToJson();
	}
}
