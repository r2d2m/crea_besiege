using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	public string mouseXAxis = "Mouse X";
	public string mouseYAxis = "Mouse Y";

	Camera mainCam;
	OrbitalTransform orbitalTransform;
	Vector3 prevMousePos;
	IAttachable hand;

	private void Awake()
	{
		Refs.controller = this;
	}

	void Start()
	{
		this.mainCam = Camera.main;
		this.orbitalTransform = this.mainCam.GetComponent<OrbitalTransform>();
		Physics.gravity = Vector3.zero;
	}

	private void FixedUpdate()
	{
		if (this.orbitalTransform != null && Input.GetMouseButton(1))
		{
			float horizontal = Input.GetAxis(this.mouseXAxis);
			float vertical = -Input.GetAxis(this.mouseYAxis);

			this.orbitalTransform.Rotate(horizontal, vertical);
		}
	}

	void Update()
	{
		if (this.hand != null && Input.GetMouseButtonDown(0))
		{
			if (EventSystem.current != null && !EventSystem.current.IsPointerOverGameObject())
			{
				RaycastHit hit;
				if (RaycastBlocks(out hit))
				{
					var block = hit.transform.GetComponent<Block>();

					if (block != null)
					{
						CreateAttached(this.hand, block, hit.normal);
					}
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.K))
		{
			Physics.gravity = new Vector3(0, -9.81f, 0);
		}

		this.prevMousePos = Input.mousePosition;
	}

	void CreateAttached(IAttachable attachablePrefab, Block block, Vector3 direction)
	{
		var newAttachable = Instantiate(attachablePrefab.GameObject).GetComponent<IAttachable>();

		newAttachable.Attach(block, direction);
	}

	bool RaycastBlocks(out RaycastHit hit)
	{
		var ray = this.mainCam.ScreenPointToRay(Input.mousePosition);
		return Physics.Raycast(ray, out hit, 1000, Helper.DefaultLayerMask);
	}

	public void SetHand(GameObject gameObject)
	{
		var attachable = gameObject.GetComponent<IAttachable>();
		if (attachable == null)
		{
			Debug.LogError(gameObject + " does not contains IAttachable component");
			return;
		}

		this.hand = attachable;
	}
}
