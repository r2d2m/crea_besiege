using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalTransform : MonoBehaviour
{
	public Transform target;

	public void Rotate(float horizontal, float vertical)
	{
		this.transform.RotateAround(this.target.position, Vector3.up, horizontal);
		this.transform.RotateAround(this.target.position, this.transform.right, vertical);
	}
}
