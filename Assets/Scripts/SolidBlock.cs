using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidBlock : MonoBehaviour
{
	BoxCollider boxBound;
	public BoxCollider[] links;

	private void Awake()
	{
		this.boxBound = GetComponent<BoxCollider>();
	}

	void Start()
    {
        
    }

    void Update()
    {
        
    }

	public BoxCollider Box
	{
		get => this.boxBound;
	}

	public Bounds Bounds
	{
		get => this.boxBound.bounds;
	}
}
