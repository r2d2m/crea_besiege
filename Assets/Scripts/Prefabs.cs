using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Prefabs : PersistentBehavior<Prefabs>
{
	[SerializeField] private CoreBlock coreBlockPrefab;
	[SerializeField] private AttachableBlock attachableBlockPrefab;
	[SerializeField] private Wheel wheelPrefab;
	[SerializeField] private Booster boosterPrefab;
	[SerializeField] private Vehicle emptyVehiclePrefab;
	[SerializeField] private Vehicle defaultVehiclePrefab;
	[SerializeField] private VehicleGameMode vehicleGameModePrefab;

    private Prefabs() :
        base(CtorArg)
    {}

    protected override void Awake()
    {
		base.Awake();
    }

	public static CoreBlock CoreBlock
	{
		get => Instance.coreBlockPrefab;
	}

	public static AttachableBlock AttachableBlock
	{
		get => Instance.attachableBlockPrefab;
	}

	public static Wheel Wheel
	{
		get => Instance.wheelPrefab;
	}

	public static Booster Booster
	{
		get => Instance.boosterPrefab;
	}

	public static Vehicle EmptyVehicle
	{
		get => Instance.emptyVehiclePrefab;
	}

	public static Vehicle DefaultVehicle
	{
		get => Instance.defaultVehiclePrefab;
	}

	public static VehicleGameMode VehicleGameMode
	{
		get => Instance.vehicleGameModePrefab;
	}
}