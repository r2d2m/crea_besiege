using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterSeed : VehicleLeafSeed
{
	private const VehicleComponentType Type = VehicleComponentType.Booster;

	public BoosterSeed()
	{
		this.type = Type;
	}

	public BoosterSeed(VehicleLeafSeed parent) : base(parent)
	{
		this.type = Type;
	}
}

[RequireComponent(typeof(Rigidbody))]
public class Booster : VehicleLeaf
{
	[SerializeField] BoxCollider box;

	public float projectionForce = 2f;
	Rigidbody body;

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
		get => new BoosterSeed(base.Seed);
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

	public static BoosterSeed FromJson(string json)
	{
		return JsonUtility.FromJson<BoosterSeed>(json);
	}
}
