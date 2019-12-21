using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Block : VehicleComponent
{
	[SerializeField] float breakForce = 500f;
	[SerializeField] BoxCollider[] linkageBoxes;

	BoxCollider box;
	Rigidbody rigidBody;

	protected virtual void Awake()
	{
		this.box = GetComponent<BoxCollider>();
		this.rigidBody = GetComponent<Rigidbody>();
	}

	protected virtual void Start()
    {
        
    }

	protected virtual void Update()
    {
        
    }

	public void Connect(Block block)
	{
		var joint = this.gameObject.AddComponent<FixedJoint>();
		joint.breakForce = this.breakForce;
		joint.connectedBody = block.RigidBody;
	}

	public GameObject GameObject
	{
		get => this.gameObject;
	}

	public BoxCollider Box
	{
		get => this.box;
	}

	public Bounds Bounds
	{
		get => this.box.bounds;
	}

	public BoxCollider[] LinkageBoxes
	{
		get => this.linkageBoxes;
	}

	public Rigidbody RigidBody
	{
		get => this.rigidBody;
	}
}
