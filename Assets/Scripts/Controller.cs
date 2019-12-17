using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] Transform origin;
	[SerializeField] GameObject block;

	public float mouseEpsilon = 0.01f;
	public float mouseSensitivity = 200f;

	string blockMask = "Block";
	Camera mainCam;
	Vector3 prevMousePos;

	void Link(GameObject a, GameObject b)
	{
		var aJoint = a.AddComponent<FixedJoint>();
		var bJoint = b.AddComponent<FixedJoint>();

		aJoint.connectedBody = b.GetComponent<Rigidbody>();
		bJoint.connectedBody = a.GetComponent<Rigidbody>();
	}

	void JoinBlock(SolidBlock block, RaycastHit hit)
	{
		var size = block.bounds.size;

		var offset = hit.normal;
		offset.x = offset.x * size.x;
		offset.y = offset.y * size.y;
		offset.z = offset.z * size.z;

		var newPos = block.bounds.center + offset;

		var newGo = Instantiate(this.block, newPos, Quaternion.identity);
		var newBlock = newGo.GetComponent<SolidBlock>();

		foreach (BoxCollider box in newBlock.links)
		{
			var colliders = Physics.OverlapBox(box.bounds.center, box.bounds.extents, Quaternion.identity, LayerMask.GetMask(this.blockMask));

			foreach (Collider c in colliders)
			{
				if (c.gameObject != newGo)
				{
					Link(c.gameObject, newGo);
				}
			}
		}
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
			var ray = this.mainCam.ScreenPointToRay(Input.mousePosition);

			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask(this.blockMask)))
			{
				var block = hit.transform.GetComponent<SolidBlock>();

				if (block != null)
				{
					JoinBlock(block, hit);
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
