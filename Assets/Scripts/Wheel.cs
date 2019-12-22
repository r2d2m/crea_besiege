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
		return JsonUtility.FromJson<WheelSeed>(json);
	}
}

[RequireComponent(typeof(Rigidbody))]
public class Wheel : VehicleLeaf
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

	private void Position(Block block, Vector3 direction)
	{
		var orientation = GetOrientation(block, direction);
		var translation = direction.Multiplied(block.Bounds.size);

		var newPosition = block.Bounds.center + translation;
		var newRotation = Quaternion.FromToRotation(this.RotationAxis, orientation);

		this.transform.position = newPosition;
		this.transform.rotation = newRotation;
	}

	private HingeJoint Connect(Block block, Vector3 rotationAxis)
	{
		var joint = this.gameObject.AddComponent<HingeJoint>();
		joint.breakForce = this.breakForce;
		joint.connectedBody = block.RigidBody;
		joint.axis = rotationAxis;

		this.joint = joint;
		return joint;
	}

	private HingeJoint Connect(Block block)
	{
		return Connect(block, this.LocalRotationAxis);
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

	public override void Setup(Block block, Vector3 direction)
	{
		base.Setup(block, direction);

		Position(block, direction);

		Connect(block);
	}

	public override void Setup(string json)
	{
		base.Setup(json);

		var seed = WheelSeed.FromJson(json);
		if (!seed.IsDataValid)
		{
			throw new Exception("Invalid data in json file. Json : " + json);
		}

		var block = this.Vehicle.GetChildFromID(seed.linkedId) as Block;
		if (block == null)
		{
			throw new Exception("Corrupted json file. Trying to link a Wheel to a VehicleComponent that is not a Block. Json : " + json);
		}

		Connect(block, seed.rotationAxis);
	}

	public override string ToJson()
	{
		// Not calling base class method is intentional

		return this.Seed.ToJson();
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
