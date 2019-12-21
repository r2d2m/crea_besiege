using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditionUI : MonoBehaviour
{
	[SerializeField] private Button solidBlockButton;
	[SerializeField] private Button wheelButton;
	[SerializeField] private Button boosterButton;

	[SerializeField] private GameObject solidBlockPrefab;
	[SerializeField] private GameObject wheelPrefab;
	[SerializeField] private GameObject boosterPrefab;

    void Start()
    {
		this.solidBlockButton.onClick.AddListener(() =>
		{
			Refs.controller.SetHand(this.solidBlockPrefab);
		});

		this.wheelButton.onClick.AddListener(() =>
		{
			Refs.controller.SetHand(this.wheelPrefab);
		});

		this.boosterButton.onClick.AddListener(() =>
		{
			Refs.controller.SetHand(this.boosterPrefab);
		});
	}

    void Update()
    {
        
    }
}
