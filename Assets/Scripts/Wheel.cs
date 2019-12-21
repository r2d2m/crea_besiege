using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSerializableData : VehicleComponentSerializableData
{
	public uint linkedId = uint.MaxValue;

	public WheelSerializableData()
	{

	}

	public WheelSerializableData(VehicleComponentSerializableData parent) : base(parent)
	{
		
	}

	public new void AssertValidData()
	{
		base.AssertValidData();

		Debug.Assert(this.linkedId != uint.MaxValue);
	}

	public new string ToJson()
	{
		AssertValidData();
		return JsonUtility.ToJson(this, true);
	}
}

[RequireComponent(typeof(Rigidbody))]
public class Wheel : VehicleComponent, IAttachable
{
	[SerializeField] float breakForce = 1000f;
	[SerializeField] float rotationForce = 2f;
	Rigidbody body;
	Block linkedBlock;

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

	private void Connect(Block block)
	{
		var joint = this.gameObject.AddComponent<HingeJoint>();
		joint.breakForce = this.breakForce;
		joint.connectedBody = block.RigidBody;
		joint.axis = this.LocalRotationAxis;

		this.linkedBlock = block;
	}

	protected new WheelSerializableData SerializableData
	{
		get
		{
			var data = new WheelSerializableData(base.SerializableData);
			data.type = VehicleComponentType.Wheel;
			data.linkedId = this.linkedBlock.ID;

			return data;
		}
	}

	public void Setup(Block block, Vector3 direction)
	{
		base.Setup(block.Vehicle);

		Position(block, direction);

		Connect(block);
	}

	public override string ToJson()
	{
		// Not calling base class method is intentional

		return this.SerializableData.ToJson();
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
