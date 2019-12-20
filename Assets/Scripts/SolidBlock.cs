using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidBlock : MonoBehaviour
{
	[SerializeField] float breakForce = 500f;
	public BoxCollider[] links;
	BoxCollider boxBound;

	private void Awake()
	{
		this.boxBound = GetComponent<BoxCollider>();
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	public BoxCollider Box
	{
		get => this.boxBound;
	}

	public Bounds Bounds
	{
		get => this.boxBound.bounds;
	}

	public void JoinToBody(Rigidbody body)
	{
		var thisJoint = this.gameObject.AddComponent<FixedJoint>();
		thisJoint.breakForce = this.breakForce;
		thisJoint.connectedBody = body;
	}
}
