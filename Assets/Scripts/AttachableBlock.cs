using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AttachableBlockSeed : BlockSeed
{
	private const VehicleComponentType Type = VehicleComponentType.AttachableBlock;

	public AttachableBlockSeed()
	{
		this.type = Type;
	}

	public AttachableBlockSeed(BlockSeed parent) : base(parent)
	{
		this.type = Type;
	}

	public static new AttachableBlockSeed FromJson(string json)
	{
		return JsonUtility.FromJson<AttachableBlockSeed>(json);
	}
}

public class AttachableBlock : Block, IAttachable
{
	[SerializeField] bool isDlc = false;

	protected override void Awake()
	{
		base.Awake();
	}

	protected override void Start()
    {
		base.Start();
    }

	protected override void Update()
    {
		base.Update();
    }

	private Vector3 ComputeSetupPosition(Block block, Vector3 direction)
	{
		const float gap = 0.001f;

		Vector3 translation = direction.Multiplied(block.Bounds.extents + this.Bounds.extents);
		return block.Bounds.center + translation + direction * gap;
	}

	private Collider[] OverlapBox(Vector3 position)
	{
		return Physics.OverlapBox(position, this.Bounds.extents, Quaternion.identity, Helper.DefaultLayerMask);
	}

	protected new AttachableBlockSeed Seed
	{
		get
		{
			var seed = new AttachableBlockSeed(base.Seed);
			if (this.isDlc)
			{
				seed.type = VehicleComponentType.BlockDLC;
			}

			return seed;
		}
	}

	public bool IsSetupable(Block block, Vector3 direction)
	{
		Vector3 position = ComputeSetupPosition(block, direction);
		Collider[] colliders = OverlapBox(position);

		return colliders.Length == 0;
	}

	public virtual void Setup(Block block, Vector3 direction)
	{
		Debug.Assert(IsSetupable(block, direction));

		this.transform.position = ComputeSetupPosition(block, direction);

		foreach (BoxCollider box in this.LinkageBoxes)
		{
			Collider[] colliders = Physics.OverlapBox(box.bounds.center, box.bounds.extents, Quaternion.identity, Helper.BlockLayerMask);

			foreach (Collider collider in colliders)
			{
				if (collider.gameObject != this.gameObject)
				{
					var hitBlock = collider.gameObject.GetComponent<Block>();
					if (hitBlock != null)
					{
						InterConnect(hitBlock, this);
					}
				}
			}
		}
	}

	public override void Setup(string json)
	{
		base.Setup(json);
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
	
	public static void InterConnect(Block a, Block b)
	{
		a.Connect(b);
		b.Connect(a);
	}
}
