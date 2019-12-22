using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PushButton))]
public class SceneLoaderButton : MonoBehaviour
{
	[SerializeField] private string sceneName;

	private PushButton button;

	private void Awake()
	{
		this.button = GetComponent<PushButton>();
	}

	private void Start()
    {
		this.button.AddListener(() =>
		{
			Helper.LoadSingleActiveScene(this.sceneName);
		});
    }
}
