using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void EditionQuitEvent();

public sealed class GameEvents : PersistentBehavior<GameEvents>
{
    private EditionQuitEvent onEditionQuit = () => { };

    private GameEvents() :
        base(CtorArg)
    { }

    protected override void Awake()
    {
        base.Awake();
    }

    public static EditionQuitEvent OnEditionQuit
    {
        get => Instance.onEditionQuit;
        set => Instance.onEditionQuit = value;
    }
}