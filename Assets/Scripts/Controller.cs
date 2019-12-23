using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	private void OnGUI()
	{
		if (Vehicle.Current != null)
		{
			Event e = Event.current;
			if (e.isKey && e.keyCode != KeyCode.None)
			{
				Vehicle.Current.PropagateInput(e.keyCode);
			}
		}
	}
}
