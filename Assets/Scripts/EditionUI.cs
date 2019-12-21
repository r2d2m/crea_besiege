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
			Refs.controller.SetHand(AttachmentPrefabs.SolidBlock);
		});

		this.wheelButton.onClick.AddListener(() =>
		{
			Refs.controller.SetHand(AttachmentPrefabs.Wheel);
		});

		this.boosterButton.onClick.AddListener(() =>
		{
			Refs.controller.SetHand(AttachmentPrefabs.Booster);
		});
	}

    void Update()
    {
        
    }
}
