using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnStart : MonoBehaviour
{
    public bool no = false;

    void Start()
    {
        if (!this.no)
        {
            this.gameObject.SetActive(false);
        }
    }
}
