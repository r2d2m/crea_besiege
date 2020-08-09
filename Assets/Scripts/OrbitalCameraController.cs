using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCameraController : MonoBehaviour
{
    public string mouseXAxis = "Mouse X";
    public string mouseYAxis = "Mouse Y";

    OrbitalTransform orbitalTransform;

    private void Start()
    {
        this.orbitalTransform = Camera.main.GetComponent<OrbitalTransform>();
    }

    private void LateUpdate()
    {
        if (this.orbitalTransform != null)
        {
            if (this.orbitalTransform.Target == null && Vehicle.Current != null)
            {
                this.orbitalTransform.Target = Vehicle.Current.CoreBlock.transform;
            }

            if (this.orbitalTransform.Target != null && Input.GetMouseButton(1))
            {
                float horizontal = Input.GetAxis(this.mouseXAxis);
                float vertical = -Input.GetAxis(this.mouseYAxis);

                this.orbitalTransform.Rotate(horizontal, vertical);
            }
        }
    }
}
