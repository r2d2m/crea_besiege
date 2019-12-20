using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
	[SerializeField] Transform origin;

	public float mouseEpsilon = 0.01f;
	public float mouseSensitivity = 200f;

	Camera mainCam;
	Vector3 prevMousePos;
	IAttachable hand;

	private void Awake()
	{

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

	public void SetHand(GameObject hand)
	{
		this.hand = hand.GetComponent<IAttachable>();
	}

	void Start()
    {
		this.mainCam = Camera.main;
		Physics.gravity = Vector3.zero;
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
