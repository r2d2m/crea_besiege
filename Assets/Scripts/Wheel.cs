using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
	Rigidbody rb;

	private void Awake()
	{
		this.rb = GetComponent<Rigidbody>();
	}

	void Start()
    {
        
    }

    void Update()
    {
		var axisV = this.transform.right;
		var axisH = this.transform.up;

		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.rb.angularVelocity = axisV * 2f;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.rb.angularVelocity = -axisV * 2f;
		}
	}
}
