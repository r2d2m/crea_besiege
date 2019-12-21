using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AttachmentPrefabs : PersistentBehavior<AttachmentPrefabs>
{
	[SerializeField] private CoreBlock coreBlockPrefab;
	[SerializeField] private SolidBlock solidBlockPrefab;
	[SerializeField] private Wheel wheelPrefab;
	[SerializeField] private Booster boosterPrefab;

    private AttachmentPrefabs() :
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

	public static SolidBlock SolidBlock
	{
		get => Instance.solidBlockPrefab;
	}

	public static Wheel Wheel
	{
		get => Instance.wheelPrefab;
	}

	public static Booster Booster
	{
		get => Instance.boosterPrefab;
	}
}