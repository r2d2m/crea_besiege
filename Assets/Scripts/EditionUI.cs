using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditionUI : MonoBehaviour
{
	[SerializeField] private EditionHand hand;
	[SerializeField] private Button solidBlockButton;
	[SerializeField] private Button wheelButton;
	[SerializeField] private Button boosterButton;

    void Start()
    {
		this.solidBlockButton.onClick.AddListener(() =>
		{
			this.hand.Pick(Prefabs.AttachableBlock);
		});

		this.wheelButton.onClick.AddListener(() =>
		{
			this.hand.Pick(Prefabs.Wheel);
		});

		this.boosterButton.onClick.AddListener(() =>
		{
			this.hand.Pick(Prefabs.Booster);
		});
	}

    void Update()
    {
        
    }
}
