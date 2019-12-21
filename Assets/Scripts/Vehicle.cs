using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
	[SerializeField] private bool createCoreBlock;

	private IDGenerator idGenerator;

	private void Awake()
	{
		Refs.vehicle = this;
	}

	void Start()
    {
        if (this.createCoreBlock)
		{
			CreateCoreBlock();
		}
    }

    void Update()
    {
        
    }

	GameObject InstantiateChild(GameObject go)
	{
		return Instantiate(go, this.transform);
	}

	CoreBlock CreateCoreBlock()
	{
		CoreBlock core = InstantiateChild(AttachmentPrefabs.CoreBlock.gameObject).GetComponent<CoreBlock>();
		core.Setup(this);

		return core;
	}

	public void CreateAttachment(IAttachable attachable, Block block, Vector3 direction)
	{
		var newAttachable = InstantiateChild(attachable.GameObject).GetComponent<IAttachable>();

		newAttachable.Setup(block, direction);
	}

	public uint GenerateID()
	{
		return this.idGenerator.Generate();
	}
}
