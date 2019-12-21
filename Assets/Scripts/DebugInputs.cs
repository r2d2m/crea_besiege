using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DebugInputs : MonoBehaviour
{
#if UNITY_EDITOR

	void Start()
    {
		
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
		{
			Helper.LoadSingleActiveScene("World1");
		}

		if (Input.GetKeyDown(KeyCode.P))
		{
			var writer = new StreamWriter("file.txt");
			writer.Write(Refs.vehicle.ToJson());
			writer.Close();
		}
    }

#endif
}
