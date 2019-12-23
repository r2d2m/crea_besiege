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
		var seed = JsonUtility.FromJson<BoosterSeed>(json);

		if (!seed.IsDataValid)
		{
			throw new Exception("Invalid data in json file. Json : " + json);
		}

		return seed;
	}
}

[RequireComponent(typeof(Rigidbody))]
public class Booster : VehicleLeaf, IAttachable
{
	private const KeyCode BoundKey = KeyCode.Space;

	[SerializeField] BoxCollider box;

	public float projectionForce = 2f;
	Rigidbody body;
	FixedJoint joint;

	private void Awake()
	{
		this.body = GetComponent<Rigidbody>();
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

	private Collider[] OverlapBox(Vector3 position, Quaternion rotation)
	{
		return Physics.OverlapBox(position, this.Bounds.extents, rotation, Helper.DefaultLayerMask);
	}

	private Quaternion ComputeSetupRotation(Block block, Vector3 direction)
	{
		return Quaternion.FromToRotation(-this.ProjectionDirection, direction);
	}

	private Vector3 ComputeSetupPosition(Block block, Vector3 direction)
	{
		const float gap = 0.001f;

		this.box.transform.rotation = ComputeSetupRotation(block, direction);
		Physics.SyncTransforms();

		Vector3 translation = direction.Multiplied(block.Bounds.extents + this.Bounds.extents);

		this.box.transform.rotation = Quaternion.identity;
		Physics.SyncTransforms();

		return block.Bounds.center + translation + direction * gap;
	}

	private FixedJoint Join(Block block)
	{
		this.joint = this.gameObject.AddComponent<FixedJoint>();
		this.joint.connectedBody = block.RigidBody;

		return this.joint;
	}

	private FixedJoint Join(Block block, Vector3 connectedAnchor)
	{
		this.joint = Join(block);
		this.joint.connectedAnchor = connectedAnchor;

		return this.joint;
	}

	private void OnInputReceived(KeyCode key)
	{
		if (key == BoundKey)
		{
			this.body.AddForce(this.Projection);
		}
	}

	public void SubscribeToInputEvents()
	{
		this.Vehicle.onInputReceived += OnInputReceived;
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

		var seed = BoosterSeed.FromJson(json);
		
		Join(this.LinkedBlock, seed.connectedAnchor);
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
