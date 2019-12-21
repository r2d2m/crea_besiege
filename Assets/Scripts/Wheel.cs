using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Wheel : MonoBehaviour, IAttachable
{
	[SerializeField] float breakForce = 1000f;
	[SerializeField] float rotationForce = 2f;
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
		if (Input.GetKey(KeyCode.UpArrow))
		{
			this.body.angularVelocity = this.RotationAxis * this.rotationForce;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			this.body.angularVelocity = -this.RotationAxis * this.rotationForce;
		}
	}

	Vector3 GetOrientation(Block block, Vector3 direction)
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

	public void Connect(Block block)
	{
		var joint = this.gameObject.AddComponent<HingeJoint>();
		joint.breakForce = this.breakForce;
		joint.connectedBody = block.RigidBody;
		joint.axis = this.LocalRotationAxis;
	}

	public void Attach(Block block, Vector3 direction)
	{
		var orientation = GetOrientation(block, direction);
		var translation = direction.Multiplied(block.Bounds.size);

		var newPosition = block.Bounds.center + translation;
		var newRotation = Quaternion.FromToRotation(this.RotationAxis, orientation);

		this.transform.position = newPosition;
		this.transform.rotation = newRotation;

		Connect(block);
	}

	public GameObject GameObject
	{
		get => this.gameObject;
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
