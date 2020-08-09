using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockSeed : VehicleComponentSeed
{
    [Serializable]
    public class Link
    {
        public uint id = uint.MaxValue;
        public Vector3 connectedAnchor = Helper.MaxVector3;

        public Link()
        { }

        public Link(uint id, Vector3 connectedAnchor)
        {
            this.id = id;
            this.connectedAnchor = connectedAnchor;
        }

        public bool IsValidData
        {
            get => this.id != uint.MaxValue 
                && this.connectedAnchor != Helper.MaxVector3;
        }
    }

    public List<Link> links = null;

    public BlockSeed()
    {

    }

    public BlockSeed(BlockSeed other) : base(other)
    {
        this.links = new List<Link>(other.links);
    }

    public BlockSeed(VehicleComponentSeed parent) : base(parent)
    {

    }

    public new bool IsDataValid
    {
        get => this.links != null
            && base.IsDataValid;
    }

    public new string ToJson()
    {
        Debug.Assert(this.IsDataValid);
        return JsonUtility.ToJson(this, true);
    }

    public static new BlockSeed FromJson(string json)
    {
        var seed = JsonUtility.FromJson<BlockSeed>(json);

        if (!seed.IsDataValid)
        {
            throw new Exception("Invalid data in json file : " + json);
        }

        return seed;
    }
}

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public class Block : VehicleComponent
{
    class Link
    {
        public Block block = null;
        public FixedJoint joint = null;

        public Link()
        { }

        public Link(Block block, FixedJoint joint)
        {
            this.block = block;
            this.joint = joint;
        }
    }

    [SerializeField] float breakForce = 500f;
    [SerializeField] BoxCollider[] linkageBoxes;

    BoxCollider box;
    Rigidbody rigidBody;
    List<Link> links = new List<Link>();

    protected virtual void Awake()
    {
        this.box = GetComponent<BoxCollider>();
        this.rigidBody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected new BlockSeed Seed
    {
        get
        {
            Debug.Assert(this.links != null);

            var seed = new BlockSeed(base.Seed);
            seed.links = new List<BlockSeed.Link>(this.links.Count);

            for (int i = 0; i < this.links.Count; ++i)
            {
                var seedLink = new BlockSeed.Link(this.links[i].block.ID, this.links[i].joint.connectedAnchor);

                Debug.Assert(seedLink.IsValidData);
                seed.links.Add(seedLink);
            }

            return seed;
        }
    }

    public override string ToJson()
    {
        // Not calling base class method is intentional

        return this.Seed.ToJson();
    }

    public override void Setup(string json)
    {
        base.Setup(json);

        var seed = BlockSeed.FromJson(json);

        var blocksToLink = new List<Block>(seed.links.Count);

        foreach (BlockSeed.Link seedLink in seed.links)
        {
            blocksToLink.Add(this.Vehicle.GetChildFromID<Block>(seedLink.id));
        }

        for (int i = 0; i < blocksToLink.Count; ++i)
        {
            Connect(blocksToLink[i], seed.links[i].connectedAnchor);
        }
    }

    public FixedJoint Connect(Block block)
    {
        var joint = this.gameObject.AddComponent<FixedJoint>();
        joint.breakForce = this.breakForce;
        joint.connectedBody = block.RigidBody;

        var link = new Link(block, joint);
        this.links.Add(link);

        return joint;
    }

    public FixedJoint Connect(Block block, Vector3 connectedAnchor)
    {
        var joint = Connect(block);
        joint.autoConfigureConnectedAnchor = false;
        joint.connectedAnchor = connectedAnchor;

        return joint;
    }

    public GameObject GameObject
    {
        get => this.gameObject;
    }

    public BoxCollider Box
    {
        get => this.box;
    }

    public Bounds Bounds
    {
        get => this.box.bounds;
    }

    public BoxCollider[] LinkageBoxes
    {
        get => this.linkageBoxes;
    }

    public Rigidbody RigidBody
    {
        get => this.rigidBody;
    }
}
