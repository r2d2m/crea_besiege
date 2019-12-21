using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttachable
{
	void Setup(Block block, Vector3 direction);

	GameObject GameObject { get; }
}
