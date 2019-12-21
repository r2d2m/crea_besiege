using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class AttachmentPrefabs : PersistentBehavior<AttachmentPrefabs>
{
	[SerializeField] private GameObject solidBlockPrefab;
	[SerializeField] private GameObject wheelPrefab;
	[SerializeField] private GameObject boosterPrefab;

    private AttachmentPrefabs() :
        base(CtorArg)
    {}

    protected override void Awake()
    {
		base.Awake();
    }

	public static GameObject SolidBlock
	{
		get => Instance.solidBlockPrefab;
	}

	public static GameObject Wheel
	{
		get => Instance.wheelPrefab;
	}

	public static GameObject Booster
	{
		get => Instance.boosterPrefab;
	}
}