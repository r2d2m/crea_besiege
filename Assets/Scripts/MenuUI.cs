using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
	[SerializeField] Button vehicleSelectionButton;
	[SerializeField] string vehicleSelectionScene;

	[SerializeField] Button levelScelectionButton;
	[SerializeField] string levelSelectionScene;

    void Start()
    {
		this.vehicleSelectionButton.onClick.AddListener(() =>
		{
			Helper.LoadSingleActiveScene(this.vehicleSelectionScene);
		});

		this.levelScelectionButton.onClick.AddListener(() =>
		{
			Helper.LoadSingleActiveScene(this.levelSelectionScene);
		});
	}

    void Update()
    {
        
    }
}
