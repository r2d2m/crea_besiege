using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour, IJsonSerializable
{
	[SerializeField] private bool createCoreBlock;

	private IDGenerator idGenerator;
	private List<VehicleComponent> components = new List<VehicleComponent>();

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

	VehicleComponent InstantiateComponent(VehicleComponent component)
	{
		var newComponent = Instantiate(component.gameObject, this.transform).GetComponent<VehicleComponent>();
		this.components.Add(newComponent);

		return newComponent;
	}

	CoreBlock CreateCoreBlock()
	{
		CoreBlock core = InstantiateComponent(AttachmentPrefabs.CoreBlock).GetComponent<CoreBlock>();
		core.Setup(this);

		return core;
	}

	public string ToJson()
	{
		string json = "";

		foreach (VehicleComponent component in this.components)
		{
			json += component.ToJson() + "\n\n/\n\n";
		}

		int lastSlashIndex = json.LastIndexOf('/');
		json = json.Remove(lastSlashIndex);

		return json;
	}

	public void CreateAttachment(IAttachable attachable, Block block, Vector3 direction)
	{
		var newAttachable = InstantiateComponent(attachable.VehicleComponent).GetComponent<IAttachable>();

		newAttachable.Setup(block, direction);
	}

	public uint GenerateID()
	{
		return this.idGenerator.Generate();
	}
}
