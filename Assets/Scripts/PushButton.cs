using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class PushButton : MonoBehaviour
{
	private Button button;
	private Callback onClick = () => { };

	private void Awake()
	{
		this.button = GetComponent<Button>();
		
		this.onClick = () =>
		{
			if (EventSystem.current != null)
			{
				EventSystem.current.SetSelectedGameObject(null);
			}
		};
	}

	public void AddListener(Callback callback)
	{
		this.button.onClick.AddListener(() =>
		{
			callback();
		});
	}

	public void RemoveListeners()
	{
		this.button.onClick.RemoveAllListeners();
		AddListener(this.onClick);
	}

	public Button RawButton
	{
		get => this.button;
	}

	public Button.ButtonClickedEvent OnClickRaw
	{
		get => this.button.onClick;
	}
}
