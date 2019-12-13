using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] Transform camTarget;
	[SerializeField] GameObject goBlock;

	public float mouseEpsilon = 0.01f;
	public float mouseSensitivity = 10f;
	string blockMask = "Block";
	Camera mainCam;
	Vector3 prevMousePos;

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
				var box = hit.transform.GetComponent<BoxCollider>();
				Debug.Assert(box != null);

				var offset = hit.normal;
				offset.x *= box.size.x / 2f;
				offset.y *= box.size.y / 2f;
				offset.z *= box.size.z / 2f;

				var pos = box.transform.position + offset;

				var go = Instantiate(this.goBlock, pos, Quaternion.identity);
				var joint = go.GetComponent<FixedJoint>();

				Debug.Assert(joint != null);

				joint.connectedBody = hit.transform.GetComponent<Rigidbody>();
			}
		}

		if (Input.GetMouseButton(1))
		{
			var mouseDelta = Input.mousePosition - this.prevMousePos;
			if (Mathf.Abs(mouseDelta.x) > this.mouseEpsilon)
			{
				float moveSens = mouseDelta.x > 0 ? 1f : -1f;
				this.mainCam.transform.RotateAround(this.camTarget.position, Vector3.up, Time.deltaTime * this.mouseSensitivity * moveSens);
			}
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			Physics.gravity = new Vector3(0, -9.81f, 0);
		}

		this.prevMousePos = Input.mousePosition;
    }
}
