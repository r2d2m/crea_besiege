using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IDGenerator
{
	uint i;

	public uint Generate()
	{
		return this.i++;
	}
}
