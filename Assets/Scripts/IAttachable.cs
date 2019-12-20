using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttachable
{
	void Attach(Block block, Vector3 direction);

	GameObject GameObject { get; }
}
