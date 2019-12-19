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
		var axis = this.transform.right;

        if (Input.GetKey(KeyCode.UpArrow))
		{
			this.rb.angularVelocity = axis * 2f;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.rb.angularVelocity = -axis * 2f;
		}
	}
}
