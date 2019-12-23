using System;
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
	private const KeyCode ForwardKey = KeyCode.Z;
	private const KeyCode BackwardKey = KeyCode.S;

	[SerializeField] float breakForce = 1000f;
	[SerializeField] float rotationForce = 2f;
	[SerializeField] MeshCollider meshCollider; 
	Rigidbody body;
	HingeJoint joint;

	private void Awake()
	{
		this.body = GetComponent<Rigidbody>();
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
		const float gap = 0.2f;

		this.meshCollider.transform.rotation = ComputeSetupRotation(block, direction);

		Vector3 translation = direction.Multiplied(block.Bounds.extents + this.Bounds.extents);

		this.meshCollider.transform.rotation = Quaternion.identity;

		return block.Bounds.center + translation + direction * gap;
	}

	private Quaternion ComputeSetupRotation(Block block, Vector3 direction)
	{
		var orientation = GetOrientation(block, direction);
		return Quaternion.FromToRotation(this.RotationAxis, orientation);
	}

	private Collider[] OverlapBox(Vector3 position, Quaternion rotation)
	{
		return Physics.OverlapBox(position, this.Bounds.extents, rotation, Helper.DefaultLayerMask);
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

	private void OnInputReceived(KeyCode key)
	{
		if (key == ForwardKey)
		{
			this.body.angularVelocity = this.RotationAxis * this.rotationForce;
		}
		else if (key == BackwardKey)
		{
			this.body.angularVelocity = -this.RotationAxis * this.rotationForce;
		}
	}

	public void SubscribeToInputEvents()
	{
		this.Vehicle.onInputReceived += OnInputReceived;
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

		Collider[] colliders = OverlapBox(position, rotation);

		return colliders.Length == 0;
	}

	public void Setup(Block block, Vector3 direction)
	{
		this.LinkedBlock = block;

		this.transform.position = ComputeSetupPosition(block, direction);
		this.transform.rotation = ComputeSetupRotation(block, direction);

		Join(block);
		SubscribeToInputEvents();
	}

	public override void Setup(string json)
	{
		base.Setup(json);

		var seed = WheelSeed.FromJson(json);

		Join(this.LinkedBlock, seed.rotationAxis);
		SubscribeToInputEvents();
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

	public Bounds Bounds
	{
		get => this.meshCollider.bounds;
	}
}
