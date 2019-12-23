using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class VehicleHeader
{
	public uint snapshot = uint.MaxValue;

	public bool IsDataValid
	{
		get => this.snapshot != uint.MaxValue;
	}

	public string ToJson()
	{
		return JsonUtility.ToJson(this, true);
	}

	public static VehicleHeader FromJson(string json)
	{
		var header = JsonUtility.FromJson<VehicleHeader>(json);
		if (!header.IsDataValid)
		{
			throw new Exception("Invalid data in json file. Json : " + json);
		}

		return header;
	}
}

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
		var newGameObject = Instantiate(component.gameObject, this.transform);
		Destroy(newGameObject.GetComponent<DeactivateOnStart>());

		var newComponent = newGameObject.GetComponent<VehicleComponent>();


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

	private Dictionary<uint, string> AddChilds(string[] jsonComponents)
	{
		var jsonMap = new Dictionary<uint, string>();

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

	private VehicleHeader Header
	{
		get
		{
			var header = new VehicleHeader();
			header.snapshot = this.idGenerator.Snapshot;

			return header;
		}
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

	public T GetChildFromIDNothrow<T>(uint id) where T : VehicleComponent
	{
		return GetChildFromID(id) as T;
	}

	public T GetChildFromID<T>(uint id) where T : VehicleComponent
	{
		var child = GetChildFromIDNothrow<T>(id);
		if (!child)
		{
			throw new Exception("Failed to cast object of type " + child.GetType() + " to " + typeof(T));
		}

		return child;
	}

	public string ToJson()
	{
		const string Separator = "\n\n/\n\n";

		string json = this.Header.ToJson() + Separator;

		foreach (var pair in this.components)
		{
			json += pair.Value.ToJson() + Separator;
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
		string[] splitJson = json.Split('/');
		if (splitJson.Length == 0)
		{
			throw new Exception("Unable to read file. Json : " + json);
		}

		string headerJson = splitJson[0];
		var header = VehicleHeader.FromJson(headerJson);

		var vehicle = CreateEmpty();

		vehicle.idGenerator = new IDGenerator(header.snapshot);

		if (splitJson.Length > 1)
		{
			var jsonComponents = new string[splitJson.Length - 1];
			Array.Copy(splitJson, 1, jsonComponents, 0, jsonComponents.Length);

			Dictionary<uint, string> jsonMap = vehicle.AddChilds(jsonComponents);
			vehicle.SetupChilds(jsonMap);
		}

		return vehicle;
	}

	public static Vehicle CreateEmpty()
	{
		return Instantiate(Prefabs.EmptyVehicle);
	}

	public static Vehicle CreateDefault()
	{
		return Instantiate(Prefabs.DefaultVehicle);
	}
}
