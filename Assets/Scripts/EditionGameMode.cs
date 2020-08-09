using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditionGameMode : MonoBehaviour
{
    Vector3 defaultGravity;
    private void Start()
    {
        this.defaultGravity = Physics.gravity;
        Physics.gravity = Vector3.zero;

        GameEvents.OnEditionQuit += OnEditionQuit;
    }

    private void OnDestroy()
    {
        if (GameEvents.InstanceExists)
        {
            GameEvents.OnEditionQuit -= OnEditionQuit;
        }

        Physics.gravity = this.defaultGravity;
    }

    private void OnEditionQuit()
    {
        VehicleGameMode.Create();
        Destroy(this.gameObject);
    }
}
