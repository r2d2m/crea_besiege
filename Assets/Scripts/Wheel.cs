using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
	[SerializeField] float breakForce = 1000f;
	[SerializeField] float force = 2f;
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
			this.rb.angularVelocity = this.rotationAxis * this.force;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.rb.angularVelocity = -this.rotationAxis * this.force;
		}
	}

	public void JoinToBody(Rigidbody rigidbody)
	{
		var hingeJoint = this.gameObject.AddComponent<HingeJoint>();
		hingeJoint.breakForce = this.breakForce;
		hingeJoint.connectedBody = rigidbody;
		hingeJoint.axis = this.localRotationAxis;
	}

	public Vector3 rotationAxis
	{
		get => this.transform.up;
	}
	
	public Vector3 localRotationAxis
	{
		get => this.transform.worldToLocalMatrix * this.rotationAxis;
	}
}
