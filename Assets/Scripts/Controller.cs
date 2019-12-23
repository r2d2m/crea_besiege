using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	private void OnGUI()
	{
		if (Refs.vehicle != null)
		{
			Event e = Event.current;
			if (e.isKey && e.keyCode != KeyCode.None)
			{
				Refs.vehicle.PropagateInput(e.keyCode);
			}
		}
	}
}
