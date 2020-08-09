using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleLeafSeed : VehicleComponentSeed
{
    public uint linkedId = uint.MaxValue;

    public VehicleLeafSeed()
    {

    }

    public VehicleLeafSeed(VehicleLeafSeed other) : base(other)
    {
        this.linkedId = other.linkedId;
    }

    public VehicleLeafSeed(VehicleComponentSeed parent) : base(parent)
    {

    }

    public new bool IsDataValid
    {
        get => this.linkedId != uint.MaxValue && base.IsDataValid;
    }

    public new string ToJson()
    {
        Debug.Assert(this.IsDataValid);
        return JsonUtility.ToJson(this, true);
    }

    public static new VehicleLeafSeed FromJson(string json)
    {
        var seed = JsonUtility.FromJson<VehicleLeafSeed>(json);

        if (!seed.IsDataValid)
        {
            throw new Exception("Invalid data in json file. Json : " + json);
        }

        return seed;
    }
}

public class VehicleLeaf : VehicleComponent
{
    Block linkedBlock;

    protected new VehicleLeafSeed Seed
    {
        get
        {
            var data = new VehicleLeafSeed(base.Seed);
            data.linkedId = this.linkedBlock.ID;

            return data;
        }
    }

    public override void Setup(string json)
    {
        base.Setup(json);

        var seed = VehicleLeafSeed.FromJson(json);

        var block = this.Vehicle.GetChildFromID<Block>(seed.linkedId);

        this.linkedBlock = block;
    }

    public override string ToJson()
    {
        // Not calling base class method is intentional

        return this.Seed.ToJson();
    }

    public Block LinkedBlock
    {
        get => this.linkedBlock;
        protected set => this.linkedBlock = value;
    }
}