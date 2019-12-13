using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	[SerializeField] GameObject goBlock;

	string blockMask = "Block";
	Camera mainCam;

    void Start()
    {
		this.mainCam = Camera.main;   
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

				Instantiate(this.goBlock, pos, Quaternion.identity);
			}
		}
    }
}
