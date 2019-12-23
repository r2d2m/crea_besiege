using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditionController : MonoBehaviour
{
	[SerializeField] EditionHand hand;
	[SerializeField] InputField inputField;
	[SerializeField] TimedText text;
	Camera mainCam;

	void Start()
	{
		HideInputField();
		this.mainCam = Camera.main;
	}

	private void Update()
	{
		if (!this.HasInputFieldFocus)
		{
			if (this.hand != null && Input.GetMouseButtonDown(0))
			{
				if (EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject())
				{
					RaycastHit hit;
					if (RaycastBlocks(out hit))
					{
						var block = hit.transform.GetComponent<Block>();

						if (block != null)
						{
							if (this.hand.IsSetupable(block, hit.normal))
							{
								this.hand.Setup(block, hit.normal);
							}
						}
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.O))
			{
				ShowLoadInputField();
			}

			if (Input.GetKeyDown(KeyCode.S))
			{
				ShowSaveInputField();
			}
		}
	}

	private bool HasInputFieldFocus
	{
		get => this.inputField.isFocused;
	}

	private void HideInputField()
	{
		this.inputField.onEndEdit.RemoveAllListeners();
		this.inputField.gameObject.SetActive(false);
	}

	private void ShowInputField()
	{
		this.inputField.gameObject.SetActive(true);
		this.inputField.ActivateInputField();
	}

	private void ShowSaveInputField()
	{
		this.inputField.onEndEdit.AddListener((string name) =>
		{
			try
			{
				VehicleIO.Save(Vehicle.Current, name);
			}
			catch (Exception exception)
			{
				this.text.Show(exception.Message, 5f);
			}
			
			HideInputField();
		});

		ShowInputField();
	}

	private void ShowLoadInputField()
	{
		this.inputField.onEndEdit.AddListener((string name) =>
		{
			try
			{
				if (VehicleIO.Exists(name))
				{
					if (Vehicle.Current)
					{
						Destroy(Vehicle.Current.gameObject);
					}

					VehicleIO.Load(name);
				}
				else
				{
					this.text.Show("Could not find file at " + VehicleIO.GetPath(name), 5f);
				}
				
			}
			catch (Exception exception)
			{
				this.text.Show(exception.Message, 5f);
			}

			HideInputField();
		});

		ShowInputField();
	}

	private bool RaycastBlocks(out RaycastHit hit)
	{
		var ray = this.mainCam.ScreenPointToRay(Input.mousePosition);
		return Physics.Raycast(ray, out hit, 1000, Helper.DefaultLayerMask);
	}
}
