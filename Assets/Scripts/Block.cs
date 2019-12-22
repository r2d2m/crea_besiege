using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSeed : VehicleComponentSeed
{
	public List<uint> linkedIds = null;

	public BlockSeed()
	{

	}

	public BlockSeed(VehicleComponentSeed parent) : base(parent)
	{

	}

	public new void AssertValidData()
	{
		base.AssertValidData();

		Debug.Assert(this.linkedIds != null);
	}

	public new string ToJson()
	{
		AssertValidData();
		return JsonUtility.ToJson(this, true);
	}
}

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Block : VehicleComponent
{
	[SerializeField] float breakForce = 500f;
	[SerializeField] BoxCollider[] linkageBoxes;

	BoxCollider box;
	Rigidbody rigidBody;
	List<Block> linkedBlocks = new List<Block>();

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

	protected new BlockSeed Seed
	{
		get
		{
			var data = new BlockSeed(base.Seed);
			data.linkedIds = new List<uint>();

			for (int i = 0; i < this.linkedBlocks.Count; ++i)
			{
				data.linkedIds.Add(this.linkedBlocks[i].ID);
			}

			return data;
		}
	}

	public override string ToJson()
	{
		// Not calling base class method is intentional

		return this.Seed.ToJson();
	}

	public void Connect(Block block)
	{
		var joint = this.gameObject.AddComponent<FixedJoint>();
		joint.breakForce = this.breakForce;
		joint.connectedBody = block.RigidBody;

		this.linkedBlocks.Add(block);
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
