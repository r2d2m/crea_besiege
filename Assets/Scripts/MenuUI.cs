using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
	[SerializeField] Button vehicleSelectionButton;
	[SerializeField] Button levelScelectionButton;
	[SerializeField] Text infoText;

    void Start()
    {
		if (VehicleSpawner.Pick != null)
		{
			this.levelScelectionButton.interactable = true;
			this.infoText.text = "Vehicle picked : " + VehicleSpawner.Pick;
		}
		else
		{
			this.infoText.text = "You must pick a vehicle to start a level";
		}
	}

    void Update()
    {
        
    }
}
