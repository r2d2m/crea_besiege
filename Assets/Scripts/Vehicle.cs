using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour, IJsonSerializable
{
	[SerializeField] private bool createCoreBlock;

	private IDGenerator idGenerator;
	private Dictionary<uint, VehicleComponent> components = new Dictionary<uint, VehicleComponent>();

	private void Awake()
	{
		Refs.vehicle = this;
	}

	private void Start()
    {
        if (this.createCoreBlock)
		{
			CreateCoreBlock();
		}
    }

	private void Update()
    {

    }

	private uint GenerateID()
	{
		return this.idGenerator.Generate();
	}

	private VehicleComponent AddChild(VehicleComponent component, uint id)
	{
		var newComponent = Instantiate(component.gameObject, this.transform).GetComponent<VehicleComponent>();

		this.components.Add(id, newComponent);

		newComponent.SetIdentity(this, id);

		return newComponent;
	}

	private VehicleComponent AddChild(VehicleComponent component)
	{
		return AddChild(component, GenerateID());
	}

	private VehicleComponent AddChild(VehicleComponentType type, uint id)
	{
		switch (type)
		{
			case VehicleComponentType.CoreBlock: return AddChild(Prefabs.CoreBlock, id);
			
			case VehicleComponentType.AttachableBlock: return AddChild(Prefabs.AttachableBlock, id);

			case VehicleComponentType.Wheel: return AddChild(Prefabs.Wheel, id);

			case VehicleComponentType.Booster: return AddChild(Prefabs.Booster, id);

			default: Debug.LogError("Invalid type"); break;
		}

		return null;
	}

	private VehicleComponent AddChild(VehicleComponentType type)
	{
		return AddChild(type, GenerateID());
	}

	private VehicleComponent AddChild(string jsonComponent)
	{
		var seed = VehicleComponentSeed.FromJson(jsonComponent);
		return AddChild(seed.type, seed.id);
	}

	private Dictionary<uint, string> AddChilds(string json)
	{
		var jsonMap = new Dictionary<uint, string>();

		string[] jsonComponents = json.Split('/');

		foreach (string jsonComponent in jsonComponents)
		{
			uint id = AddChild(jsonComponent).ID;
			jsonMap.Add(id, jsonComponent);
		}

		return jsonMap;
	}

	private void SetupChild(uint id, string json)
	{
		VehicleComponent component = null;
		if (this.components.TryGetValue(id, out component))
		{
			component.Setup(json);
		}
		else
		{
			Debug.LogError("No VehicleComponent found with id " + id);
		}
	}

	private void SetupChilds(Dictionary<uint, string> jsonMap)
	{
		foreach (var pair in jsonMap)
		{
			VehicleComponent component = null;
			if (this.components.TryGetValue(pair.Key, out component))
			{
				component.Setup(pair.Value);
			}
			else
			{
				Debug.LogError("No VehicleComponent found with id " + pair.Key);
			}
		}
	}

	private CoreBlock CreateCoreBlock()
	{
		return AddChild(Prefabs.CoreBlock).GetComponent<CoreBlock>();
	}

	public VehicleComponent GetChildFromID(uint id)
	{
		VehicleComponent component;
		if (this.components.TryGetValue(id, out component))
		{
			return component;
		}

		return null;
	}

	public string ToJson()
	{
		string json = "";

		foreach (var pair in this.components)
		{
			json += pair.Value.ToJson() + "\n\n/\n\n";
		}

		int lastSlashIndex = json.LastIndexOf('/');
		json = json.Remove(lastSlashIndex);

		return json;
	}

	public void CreateAttachment(IAttachable attachable, Block block, Vector3 direction)
	{
		var newAttachable = AddChild(attachable.VehicleComponent).GetComponent<IAttachable>();

		newAttachable.Setup(block, direction);
	}

	public static Vehicle CreateFromJson(string json)
	{
		var vehicle = Instantiate(Prefabs.EmptyVehicle);

		Dictionary<uint, string> jsonMap = vehicle.AddChilds(json);
		vehicle.SetupChilds(jsonMap);

		return vehicle;
	}
}
