using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditionController : MonoBehaviour
{
	public string mouseXAxis = "Mouse X";
	public string mouseYAxis = "Mouse Y";

	[SerializeField] InputField inputField;
	[SerializeField] TimedText text;
	Camera mainCam;
	OrbitalTransform orbitalTransform;
	IAttachable hand;

	private void Awake()
	{
		Refs.editionController = this;
	}

	void Start()
	{
		HideInputField();
		this.mainCam = Camera.main;
		this.orbitalTransform = this.mainCam.GetComponent<OrbitalTransform>();
		Physics.gravity = Vector3.zero;
	}

	private void FixedUpdate()
	{
		if (this.orbitalTransform != null && Input.GetMouseButton(1))
		{
			float horizontal = Input.GetAxis(this.mouseXAxis);
			float vertical = -Input.GetAxis(this.mouseYAxis);

			this.orbitalTransform.Rotate(horizontal, vertical);
		}
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
							Refs.vehicle.CreateAttachment(this.hand, block, hit.normal);
						}
					}
				}
			}

			if (Input.GetKeyDown(KeyCode.K))
			{
				Physics.gravity = new Vector3(0, -9.81f, 0);
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
			VehicleLoader.Save(Refs.vehicle, name);
			HideInputField();
		});

		ShowInputField();
	}

	private void ShowLoadInputField()
	{
		this.inputField.onEndEdit.AddListener((string name) =>
		{
			if (Refs.vehicle)
			{
				Destroy(Refs.vehicle.gameObject);
			}

			try
			{
				VehicleLoader.Load(name);
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

	public void SetHand(IAttachable attachable)
	{
		this.hand = attachable;
	}
}
