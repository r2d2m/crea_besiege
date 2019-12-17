using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidBlock : MonoBehaviour
{
	[SerializeField] BoxCollider boxBound;
	public BoxCollider[] links;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

	public Bounds bounds
	{
		get => this.boxBound.bounds;
	}
}
