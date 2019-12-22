using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSeed : VehicleLeafSeed
{
	private const VehicleComponentType Type = VehicleComponentType.Booster;

	public Vector3 connectedAnchor = Helper.MaxVector3;

	public BoosterSeed()
	{
		this.type = Type;
	}

	public BoosterSeed(BoosterSeed other)
	{
		this.type = Type;
		this.connectedAnchor = other.connectedAnchor;
	}

	public BoosterSeed(VehicleLeafSeed parent) : base(parent)
	{
		this.type = Type;
	}

	public new bool IsDataValid
	{
		get => this.connectedAnchor != Helper.MaxVector3
			&& base.IsDataValid;
	}

	public static new BoosterSeed FromJson(string json)
	{
		return JsonUtility.FromJson<BoosterSeed>(json);
	}
}

[RequireComponent(typeof(Rigidbody))]
public class Booster : VehicleLeaf
{
	[SerializeField] BoxCollider box;

	public float projectionForce = 2f;
	Rigidbody body;
	FixedJoint joint;

	private void Awake()
	{
		this.body = GetComponent<Rigidbody>();
	}

	private void Start()
    {
        
    }

	private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
		{
			Use();
		}
    }

	protected new BoosterSeed Seed
	{
		get
		{
			Debug.Assert(this.joint != null);

			var seed = new BoosterSeed(base.Seed);
			seed.connectedAnchor = this.joint.connectedAnchor;

			return seed;
		}
	}

	private void Position(Block block, Vector3 direction)
	{
		this.transform.rotation = Quaternion.FromToRotation(-this.ProjectionDirection, direction);
		Physics.SyncTransforms();

		Vector3 translation = direction.Multiplied(block.Bounds.extents + this.Bounds.extents);

		this.transform.position = block.Bounds.center + translation;
	}

	private FixedJoint Connect(Block block)
	{
		var joint = this.gameObject.AddComponent<FixedJoint>();
		joint.connectedBody = block.RigidBody;

		this.joint = joint;

		return joint;
	}

	private FixedJoint Connect(Block block, Vector3 connectedAnchor)
	{
		FixedJoint joint = Connect(block);
		joint.connectedAnchor = connectedAnchor;

		return joint;
	}

	public void Use()
	{
		this.body.AddForce(this.Projection);
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

		var seed = BoosterSeed.FromJson(json);
		if (!seed.IsDataValid)
		{
			throw new Exception("Invalid data in json file. Json : " + json);
		}

		var block = this.Vehicle.GetChildFromID(seed.linkedId) as Block;
		if (block == null)
		{
			throw new Exception("Corrupted json file. Trying to link a Wheel to a VehicleComponent that is not a Block. Json : " + json);
		}

		Connect(block, seed.connectedAnchor);
	}

	public override string ToJson()
	{
		// Not calling base class method is intentional

		return this.Seed.ToJson();
	}

	public Vector3 ProjectionDirection
	{
		get => -this.transform.up;
	}

	public Vector3 Projection
	{
		get => this.ProjectionDirection * this.projectionForce;
	}

	public Bounds Bounds
	{
		get => this.box.bounds;
	}
}
