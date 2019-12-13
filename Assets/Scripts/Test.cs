using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
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
        if (Input.GetKey(KeyCode.UpArrow))
		{
			this.rb.angularVelocity = Vector3.right * 100f;
		}
    }
}
