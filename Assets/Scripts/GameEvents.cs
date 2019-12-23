using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameEvents : PersistentBehavior<GameEvents>
{
	private Callback onGameStart = () => { };

	private GameEvents() :
		base(CtorArg)
	{ }

	protected override void Awake()
	{
		base.Awake();
	}

	public static Callback OnGameStart
	{
		get => Instance.onGameStart;
		set => Instance.onGameStart = value;
	}
}