using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Helper
{
	public const string PersistentSceneName = "Persistent";

	public static readonly int DefaultLayerMask = (~0) ^ LayerMask.GetMask("Ignored");

	public static readonly int BlockLayerMask = LayerMask.GetMask("Block");

	public static readonly Vector3 MaxVector3 = Vector3.one * float.MaxValue;

	public static readonly Vector3 OutOfMapVector3 = Vector3.one * 1000000f;

	public static readonly Quaternion MaxQuaternion = new Quaternion(float.MaxValue, float.MaxValue, float.MaxValue, float.MaxValue);

	public static Vector3 Multiplied(this Vector3 self, Vector3 vec)
	{
		return new Vector3(self.x * vec.x, self.y * vec.y, self.z * vec.z);
	}

	public static void LoadAsActiveScene(string name)
	{
		AsyncOperation loadOp = SceneManager.LoadSceneAsync(name, LoadSceneMode.Additive);

		loadOp.completed += (dummy) =>
		{
			SceneManager.SetActiveScene(SceneManager.GetSceneByName(name));
		};
	}

	public static void LoadSingleActiveScene(string name)
	{
		Scene activeScene = SceneManager.GetActiveScene();

		if (activeScene.name != PersistentSceneName)
		{
			AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(activeScene);

			unloadOp.completed += (dummy) =>
			{
				LoadAsActiveScene(name);
			};
		}
		else
		{
			LoadAsActiveScene(name);
		}
	}
}
