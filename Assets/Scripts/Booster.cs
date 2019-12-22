using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSeed : VehicleComponentSeed
{
	public uint linkedId = uint.MaxValue;

	public BoosterSeed()
	{

	}

	public BoosterSeed(VehicleComponentSeed parent) : base(parent)
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
public class Booster : VehicleComponent, IAttachable
{
	[SerializeField] BoxCollider box;

	public float projectionForce = 2f;

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
        if (Input.GetKey(KeyCode.Space))
		{
			Use();
		}
    }

	protected new BoosterSeed Seed
	{
		get
		{
			var data = new BoosterSeed(base.Seed);
			data.type = VehicleComponentType.Booster;
			data.linkedId = this.linkedBlock.ID;

			return data;
		}
	}

	private void Position(Block block, Vector3 direction)
	{
		this.transform.rotation = Quaternion.FromToRotation(-this.ProjectionDirection, direction);
		Physics.SyncTransforms();

		Vector3 translation = direction.Multiplied(block.Bounds.extents + this.Bounds.extents);

		this.transform.position = block.Bounds.center + translation;
	}

	private void Connect(Block block)
	{
		var joint = this.gameObject.AddComponent<FixedJoint>();
		joint.connectedBody = block.RigidBody;

		this.linkedBlock = block;
	}

	public void Use()
	{
		this.body.AddForce(this.Projection);
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
