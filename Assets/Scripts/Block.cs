using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Block : MonoBehaviour
{
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
