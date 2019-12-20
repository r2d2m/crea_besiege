using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
	[SerializeField] float force = 100f;
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
			this.rb.AddTorque(axisV * this.force);
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.rb.AddTorque(-axisV * this.force);
		}
	}
}
