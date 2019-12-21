using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Common : MonoBehaviour
{
	private void Awake()
	{
		if (!SceneManager.GetSceneByName(Helper.PersistentSceneName).IsValid())
		{
			SceneManager.LoadScene(Helper.PersistentSceneName, LoadSceneMode.Additive);
		}
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }
}
