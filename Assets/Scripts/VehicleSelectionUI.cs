using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelectionUI : MonoBehaviour
{
	[SerializeField] Button newButton;
	[SerializeField] Button[] vehicleButtons;

    void Start()
    {
		this.newButton.onClick.AddListener(() =>
		{
			VehicleEditionScene.Options.CreateDefaultVehicleOnNextStart = true;
			Helper.LoadSingleActiveScene(VehicleEditionScene.Name);
		});

		string[] userVehicleNames = VehicleLoader.GetUserVehicleNames();

		for (int i = 0; i < userVehicleNames.Length; ++i)
		{
			Button button = this.vehicleButtons[i];
			var textComponent = button.GetComponentInChildren<Text>();

			if (textComponent != null)
			{
				textComponent.text = userVehicleNames[i];
			}

			string name = userVehicleNames[i].Clone() as string;
			this.vehicleButtons[i].onClick.AddListener(() =>
			{
				VehicleEditionScene.Options.VehicleToLoadOnNextStart = name;
				Helper.LoadSingleActiveScene(VehicleEditionScene.Name);
			});
		}
    }

    void Update()
    {
        
    }
}
