﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSeed : VehicleLeafSeed
{
	private const VehicleComponentType Type = VehicleComponentType.Wheel;

	public Vector3 rotationAxis = Helper.MaxVector3;

	public WheelSeed()
	{
		this.type = Type;
	}

	public WheelSeed(WheelSeed other)
	{
		this.type = other.type;
		this.rotationAxis = other.rotationAxis;
	}

	public WheelSeed(VehicleLeafSeed parent) : base(parent)
	{
		this.type = Type;
	}

	public static new WheelSeed FromJson(string json)
	{
		var seed = JsonUtility.FromJson<WheelSeed>(json);

		if (!seed.IsDataValid)
		{
			throw new Exception("Invalid data in json file. Json : " + json);
		}

		return seed;
	}
}

[RequireComponent(typeof(Rigidbody))]
public class Wheel : VehicleLeaf, IAttachable
{
	[SerializeField] float breakForce = 1000f;
	[SerializeField] float rotationForce = 2f;
	Rigidbody body;
	HingeJoint joint;

	private void Awake()
	{
		this.body = GetComponent<Rigidbody>();
	}

	private void Start()
    {
        
    }

	private void Update()
    {
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.body.angularVelocity = this.RotationAxis * this.rotationForce;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.body.angularVelocity = -this.RotationAxis * this.rotationForce;
		}
	}

	private Vector3 GetOrientation(Block block, Vector3 direction)
	{
		var orientation = direction;

		if (Vector3.Angle(-direction, block.transform.forward) < 0.05f
			|| Vector3.Angle(-direction, block.transform.up) < 0.05f
			|| Vector3.Angle(-direction, block.transform.right) < 0.05f)
		{
			orientation = -orientation;
		}

		return orientation;
	}

	private Vector3 ComputeSetupPosition(Block block, Vector3 direction)
	{
		var translation = direction.Multiplied(block.Bounds.size);
		return block.Bounds.center + translation;
	}

	private Quaternion ComputeSetupRotation(Block block, Vector3 direction)
	{
		var orientation = GetOrientation(block, direction);
		return Quaternion.FromToRotation(this.RotationAxis, orientation);
	}

	private HingeJoint Join(Block block, Vector3 rotationAxis)
	{
		this.joint = this.gameObject.AddComponent<HingeJoint>();
		this.joint.breakForce = this.breakForce;
		this.joint.connectedBody = block.RigidBody;
		this.joint.axis = rotationAxis;

		return this.joint;
	}

	private HingeJoint Join(Block block)
	{
		return Join(block, this.LocalRotationAxis);
	}

	protected new WheelSeed Seed
	{
		get
		{
			Debug.Assert(this.joint != null);

			var seed = new WheelSeed(base.Seed);
			seed.rotationAxis = this.joint.axis;

			return seed;
		}
	}

	public bool IsSetupable(Block block, Vector3 direction)
	{
		Vector3 position = ComputeSetupPosition(block, direction);
		Quaternion rotation = ComputeSetupRotation(block, direction);

		return true;
	}

	public void Setup(Block block, Vector3 direction)
	{
		this.LinkedBlock = block;

		this.transform.position = ComputeSetupPosition(block, direction);
		this.transform.rotation = ComputeSetupRotation(block, direction);

		Join(block);
	}

	public override void Setup(string json)
	{
		base.Setup(json);

		var seed = WheelSeed.FromJson(json);

		Join(this.LinkedBlock, seed.rotationAxis);
	}

	public override string ToJson()
	{
		// Not calling base class method is intentional

		return this.Seed.ToJson();
	}

	public VehicleComponent VehicleComponent
	{
		get => this;
	}

	public Vector3 RotationAxis
	{
		get => this.transform.up;
	}
	
	public Vector3 LocalRotationAxis
	{
		get => this.transform.worldToLocalMatrix * this.RotationAxis;
	}
}
