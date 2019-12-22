using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditionUI : MonoBehaviour
{
	[SerializeField] private Button solidBlockButton;
	[SerializeField] private Button wheelButton;
	[SerializeField] private Button boosterButton;

    void Start()
    {
		this.solidBlockButton.onClick.AddListener(() =>
		{
			Refs.editionController.SetHand(Prefabs.AttachableBlock);
		});

		this.wheelButton.onClick.AddListener(() =>
		{
			Refs.editionController.SetHand(Prefabs.Wheel);
		});

		this.boosterButton.onClick.AddListener(() =>
		{
			Refs.editionController.SetHand(Prefabs.Booster);
		});
	}

    void Update()
    {
        
    }
}
