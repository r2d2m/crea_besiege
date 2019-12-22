using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelectionUI : MonoBehaviour
{
	[SerializeField] Button newButton;
	[SerializeField] VehicleSelectionButton[] vehicleButtons;

    void Start()
    {
		this.newButton.onClick.AddListener(() =>
		{
			VehicleEditionScene.ToLoadOnNextStart = null;
			Helper.LoadSingleActiveScene(VehicleEditionScene.Name);
		});

		foreach (var button in this.vehicleButtons)
		{
			button.onDelete += () =>
			{
				LoadUserVehicleNames();
			};
		}

		LoadUserVehicleNames();
	}

    void Update()
    {
        
    }

	private void UnlinkVehicleButtons()
	{
		foreach (var button in this.vehicleButtons)
		{
			button.Unlink();
		}
	}

	private void LoadUserVehicleNames()
	{
		UnlinkVehicleButtons();

		string[] userVehicleNames = VehicleIO.GetUserVehicleNames();

		for (int i = 0; i < userVehicleNames.Length; ++i)
		{
			this.vehicleButtons[i].Link(userVehicleNames[i]);
		}
	}
}
