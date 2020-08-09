using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditionHand : MonoBehaviour
{
    private IAttachable hand;

    void Update()
    {

    }

    public void Pick(IAttachable attachable)
    {
        if (this.hand != null)
        {
            Destroy(this.hand.VehicleComponent.gameObject);
        }

        VehicleComponent vehicleComponent = Instantiate(attachable.VehicleComponent, Helper.OutOfMapVector3, Quaternion.identity, this.transform);

        this.hand = vehicleComponent as IAttachable;
    }

    public bool IsSetupable(Block block, Vector3 direction)
    {
        return this.hand != null && this.hand.IsSetupable(block, direction);
    }

    public void Setup(Block block, Vector3 direction)
    {
        Vehicle.Current.CreateAttachment(this.hand, block, direction);
    }

    public IAttachable Attachable
    {
        get => this.hand;
    }
}
