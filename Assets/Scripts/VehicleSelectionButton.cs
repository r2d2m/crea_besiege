using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VehicleSelectionButton : MonoBehaviour
{
	public const string EmptyName = "<Empty>";

	[SerializeField] private PushButton deleteButton;
	[SerializeField] private PushButton pickButton;
	[SerializeField] private PushButton editButton;
	[SerializeField] private Text textComponent;

	public Callback onDelete = () => { };

	private void Start()
    {
		this.deleteButton.AddListener(this.onDelete);
	}

    private void Update()
    {
        
    }

	private void ResetListeners()
	{
		this.pickButton.RemoveListeners();
		this.editButton.RemoveListeners();
		this.deleteButton.RemoveListeners();

		this.deleteButton.AddListener(this.onDelete);
	}

	public void Link(string vehicleName)
	{
		if (VehicleIO.Exists(vehicleName))
		{
			ResetListeners();

			Debug.LogError("LINKING");

			this.pickButton.AddListener(() =>
			{
				Debug.LogError("PICK");
				VehicleSpawner.Pick = vehicleName;
			});

			this.editButton.AddListener(() =>
			{
				Debug.LogError("EDIT");
				VehicleEditionScene.ToLoadOnNextStart = vehicleName;
				Helper.LoadSingleActiveScene(VehicleEditionScene.Name);
			});

			this.deleteButton.AddListener(() =>
			{
				try
				{
					Debug.LogError("REMOVE");
					VehicleIO.Remove(vehicleName);
					VehicleSpawner.Pick = null;
					this.onDelete();
				}
				catch (Exception exception)
				{
					Debug.LogError(exception.Message);
				}
			});

			this.textComponent.text = vehicleName;
		}
	}

	public void Unlink()
	{
		ResetListeners();
		this.textComponent.text = EmptyName;
	}

	public string Name
	{
		get => this.textComponent.text;
	}

	public bool IsEmpty
	{
		get => this.Name == EmptyName;
	}
}
