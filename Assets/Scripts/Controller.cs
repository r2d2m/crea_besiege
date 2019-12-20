using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] Transform origin;
	[SerializeField] GameObject block;
	[SerializeField] GameObject wheel;

	public float mouseEpsilon = 0.01f;
	public float mouseSensitivity = 200f;

	string blockMask = "Block";
	Camera mainCam;
	Vector3 prevMousePos;

	void Link(SolidBlock a, SolidBlock b)
	{
		a.JoinToBody(b.GetComponent<Rigidbody>());
		b.JoinToBody(a.GetComponent<Rigidbody>());
	}

	Vector3 ComputePlacementPosition(SolidBlock block, RaycastHit hit)
	{
		var size = block.Bounds.size;

		var offset = hit.normal;
		offset.x = offset.x * size.x;
		offset.y = offset.y * size.y;
		offset.z = offset.z * size.z;

		return block.Bounds.center + offset;
	}

	Vector3 GetWheelOrientation(SolidBlock block, Vector3 normal)
	{
		var orientation = normal;

		if (Vector3.Angle(-normal, block.transform.forward) < 0.05f
			|| Vector3.Angle(-normal, block.transform.up) < 0.05f
			|| Vector3.Angle(-normal, block.transform.right) < 0.05f)
		{
			orientation = -orientation;
		}

		return orientation;
	}

	void JoinBlock(SolidBlock block, RaycastHit hit)
	{
		var newPos = ComputePlacementPosition(block, hit);
		var newGo = Instantiate(this.block, newPos, Quaternion.identity);
		var newBlock = newGo.GetComponent<SolidBlock>();

		foreach (BoxCollider box in newBlock.links)
		{
			var colliders = Physics.OverlapBox(box.bounds.center, box.bounds.extents, Quaternion.identity, LayerMask.GetMask(this.blockMask));

			foreach (Collider c in colliders)
			{
				if (c.gameObject != newGo)
				{
					Link(c.gameObject.GetComponent<SolidBlock>(), newBlock);
				}
			}
		}
	}

	void JoinWheel(SolidBlock block, RaycastHit hit)
	{
		var newPos = ComputePlacementPosition(block, hit);
		var orientation = GetWheelOrientation(block, hit.normal);

		var newGo = Instantiate(this.wheel);
		newGo.transform.position = newPos;
		newGo.transform.rotation = Quaternion.FromToRotation(newGo.transform.up, orientation);

		var wheel = newGo.GetComponent<Wheel>();
		wheel.JoinToBody(block.GetComponent<Rigidbody>());
	}

	bool RaycastSolidBlock(out RaycastHit hit)
	{
		var ray = this.mainCam.ScreenPointToRay(Input.mousePosition);

		return Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask(this.blockMask));
	}

    void Start()
    {
		this.mainCam = Camera.main;
		Physics.gravity = Vector3.zero;
    }

    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			RaycastHit hit;
			if (RaycastSolidBlock(out hit))
			{
				var block = hit.transform.GetComponent<SolidBlock>();

				if (block != null)
				{
					JoinBlock(block, hit);
				}
			}
		}

		if (Input.GetMouseButtonDown(2))
		{
			RaycastHit hit;
			if (RaycastSolidBlock(out hit))
			{
				var block = hit.transform.GetComponent<SolidBlock>();

				if (block != null)
				{
					JoinWheel(block, hit);
				}
			}
		}

		if (Input.GetMouseButton(1))
		{
			var mouseDelta = Input.mousePosition - this.prevMousePos;
			if (Mathf.Abs(mouseDelta.x) > this.mouseEpsilon)
			{
				float moveSens = mouseDelta.x > 0 ? 1f : -1f;
				this.mainCam.transform.RotateAround(this.origin.position, Vector3.up, Time.deltaTime * this.mouseSensitivity * moveSens);
			}
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			Physics.gravity = new Vector3(0, -9.81f, 0);
		}

		this.prevMousePos = Input.mousePosition;
    }
}
