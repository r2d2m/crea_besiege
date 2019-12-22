using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct IDGenerator
{
	uint seed;
	uint i;

	public IDGenerator(uint seed = 0)
	{
		this.seed = seed;
		this.i = this.seed;
	}

	public uint Generate()
	{
		return this.i++;
	}

	public uint Seed
	{
		get => this.seed;
	}

	public uint Snapshot
	{
		get => this.i;
	}
}
