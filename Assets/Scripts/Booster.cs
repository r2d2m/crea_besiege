using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Booster : MonoBehaviour, IAttachable
{
	[SerializeField] BoxCollider box;

	public float projectionForce = 2f;

	Rigidbody body;

	private void Awake()
	{
		this.body = GetComponent<Rigidbody>();
	}

	void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
		{
			Use();
		}
    }

	public void Use()
	{
		this.body.AddForce(this.Projection);
	}

	public void Attach(Block block, Vector3 direction)
	{
		this.transform.rotation = Quaternion.FromToRotation(-this.ProjectionDirection, direction);
		Physics.SyncTransforms();

		Vector3 translation = direction.Multiplied(block.Bounds.extents + this.Bounds.extents);

		this.transform.position = block.Bounds.center + translation;

		var joint = this.gameObject.AddComponent<FixedJoint>();
		joint.connectedBody = block.RigidBody;
	}

	public Vector3 ProjectionDirection
	{
		get => -this.transform.up;
	}

	public Vector3 Projection
	{
		get => this.ProjectionDirection * this.projectionForce;
	}

	public GameObject GameObject
	{
		get => this.gameObject;
	}

	public Bounds Bounds
	{
		get => this.box.bounds;
	}
}
