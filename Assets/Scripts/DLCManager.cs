using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DLCManager : PersistentBehavior<DLCManager>
{
    private PatchDayOneDLC patchDayOne = new PatchDayOneDLC();

    private DLCManager() :
        base(CtorArg)
    { }

    protected override void Awake()
    {
        base.Awake();

        LoadDLCs();
    }

    private void Start()
    {
        
    }

    public static void LoadDLCs()
    {
        Instance.patchDayOne.Load();
    }

    public static PatchDayOneDLC PatchDayOne
    {
        get
        {
            return Instance.patchDayOne;
        }
    }
}