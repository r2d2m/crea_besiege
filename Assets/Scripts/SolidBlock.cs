using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidBlock : Block, IAttachable
{
	[SerializeField] float breakForce = 500f;

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

	public void Connect(Block block)
	{
		var joint = this.gameObject.AddComponent<FixedJoint>();
		joint.breakForce = this.breakForce;
		joint.connectedBody = block.RigidBody;
	}

	public void Attach(Block block, Vector3 direction)
	{
		Vector3 translation = direction.Multiplied(block.Bounds.extents + this.Bounds.extents);

		this.transform.position = block.Bounds.center + translation;
		Physics.SyncTransforms();

		foreach (BoxCollider box in this.LinkageBoxes)
		{
			Collider[] colliders = Physics.OverlapBox(box.bounds.center, box.bounds.extents, Quaternion.identity, Helper.BlockLayerMask);

			foreach (Collider collider in colliders)
			{
				if (collider.gameObject != this.gameObject)
				{
					var solidBlock = collider.gameObject.GetComponent<SolidBlock>();
					if (solidBlock != null)
					{
						InterConnect(solidBlock, this);
					}
				}
			}
		}
	}

	public GameObject GameObject
	{
		get => this.gameObject;
	}
	
	public static void InterConnect(SolidBlock a, SolidBlock b)
	{
		a.Connect(b);
		b.Connect(a);
	}
}
