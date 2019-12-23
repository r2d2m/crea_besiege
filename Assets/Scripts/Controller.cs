using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	KeyCode[] keyCodes = new KeyCode[]
	{
		KeyCode.Z,
		KeyCode.S,
		KeyCode.Space
	};

	private void Update()
	{
		if (Vehicle.Current != null)
		{
			foreach (var key in this.keyCodes)
			{
				if (Input.GetKey(key))
				{
					Vehicle.Current.PropagateInput(key);
				}
			}
		}
	}
}
