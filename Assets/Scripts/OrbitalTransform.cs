using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalTransform : MonoBehaviour
{
	private Transform target;
	private Vector3 translation;

	private void Update()
	{
		if (this.Target != null)
		{
			Replace();	
		}
	}

	private void UpdateTranslation(Vector3 targetPosition)
	{
		this.translation = this.transform.position - targetPosition;
	}

	public void Replace()
	{
		this.transform.position = this.target.position + this.translation;
	}

	public void Rotate(float horizontal, float vertical)
	{
		this.transform.RotateAround(this.target.position, Vector3.up, horizontal);
		this.transform.RotateAround(this.target.position, this.transform.right, vertical);

		Physics.SyncTransforms();

		this.translation = this.transform.position - this.target.position;
	}

	public Transform Target
	{
		get => this.target;
		set
		{
			if (value != null)
			{
				this.target = value;
				UpdateTranslation(value.position);
			}
		}
	}
}
