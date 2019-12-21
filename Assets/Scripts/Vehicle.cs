using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
	private void Awake()
	{
		Refs.vehicle = this;
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	public void CreateAttachment(IAttachable attachablePrefab, Block block, Vector3 direction)
	{
		var newAttachable = Instantiate(attachablePrefab.GameObject, this.transform).GetComponent<IAttachable>();

		newAttachable.Attach(block, direction);
	}
}
