using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatchDayOneUI : MonoBehaviour
{
	[SerializeField] private Button button;

	private void Awake()
	{

	}

	private void Start()
    {
		PatchDayOneDLC dlc = DLCManager.PatchDayOne;
		if (dlc.IsLoaded)
		{
			this.button.onClick.AddListener(() =>
			{
				Refs.controller.SetHand(dlc.SolidBlock);
			});
		}
		else
		{
			this.gameObject.SetActive(false);
		}
    }
}
