using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DebugInputs : MonoBehaviour
{
#if UNITY_EDITOR

	void Start()
    {
		Physics.autoSyncTransforms = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
		{
			var reader = new StreamReader("file.json");
			string json = reader.ReadToEnd();

			Vehicle.CreateFromJson(json);
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			var writer = new StreamWriter("file.json");
			writer.Write(Refs.vehicle.ToJson());
			writer.Close();
		}
	}

#endif
}
