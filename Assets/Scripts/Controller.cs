using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    void Start()
    {
		Physics.gravity = new Vector3(0, -9.81f, 0);
	}

    void Update()
    {
        
    }

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

	public static void Create()
	{
		Instantiate(Prefabs.ControllerPrefab);
	}
}
