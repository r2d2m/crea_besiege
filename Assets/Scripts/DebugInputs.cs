using System.Collections;
using System.Collections.Generic;
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
    }

#endif
}
