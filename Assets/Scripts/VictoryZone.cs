using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
	public string nextLevel = null;

    void Start()
    {
	}

    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		Debug.Assert(!string.IsNullOrEmpty(this.nextLevel), "NextLevel were not assigned in VictoryZone");
		
		var block = other.gameObject.GetComponent<Block>();

		if (block != null)
		{
			Helper.LoadSingleActiveScene(this.nextLevel);
		}
	}
}
