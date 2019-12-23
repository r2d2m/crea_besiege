using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEvents : PersistentBehavior<GameEvents>
{
	private Callback onVehicleControl = () => { };

	private GameEvents() :
		base(CtorArg)
	{ }

	protected override void Awake()
	{
		base.Awake();
	}

	public static Callback OnVehicleControl
	{
		get => Instance.onVehicleControl;
		set => Instance.onVehicleControl = value;
	}
}