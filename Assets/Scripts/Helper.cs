using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
	public const string PersistentSceneName = "Persistent";
	public static readonly int DefaultLayerMask = (~0) ^ LayerMask.GetMask("Ignored");
	public static readonly int BlockLayerMask = LayerMask.GetMask("Block");

	public static Vector3 Multiplied(this Vector3 self, Vector3 vec)
	{
		return new Vector3(self.x * vec.x, self.y * vec.y, self.z * vec.z);
	}
}
