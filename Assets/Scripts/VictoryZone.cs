using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryZone : MonoBehaviour
{
	public string nextLevel;

    void Start()
    {

    }

    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
		var block = other.gameObject.GetComponent<Block>();

		if (block != null)
		{
			Helper.LoadSingleActiveScene(this.nextLevel);
		}
	}
}
