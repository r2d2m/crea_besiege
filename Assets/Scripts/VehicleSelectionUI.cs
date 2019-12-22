using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehicleSelectionUI : MonoBehaviour
{
	[SerializeField] Button newButton;

    void Start()
    {
		this.newButton.onClick.AddListener(() =>
		{
			VehicleEditionScene.CreateDefaultVehicleOnNextStart = true;
			Helper.LoadSingleActiveScene(VehicleEditionScene.Name);
		});
    }

    void Update()
    {
        
    }
}
