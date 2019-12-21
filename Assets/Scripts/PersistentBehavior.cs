using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// PersistentBehavior class to inherit, it is recommanded to seal the derived class
// - The constructor has to be defined explicitly to construct the base PersistentBehavior class from the argument CtorArg. 
// The type of this argument (CtorArgType) is defined in the Singleton class and is protected,
// it ensures that the derived class (here "MySingleton") has an explicitly defined constructor and avoid the default one which is public and allows things like :
// MySingleton singleton = new MySingleton();
// which is certainly not what you want.
// - PersistentBehavior<T> must use the Awake() function to reference itself, 
//	so don't forget to call the Awake() function of PersistentBehavior<T> with base.Awake() when overriding it.
//
// See code example here :

//  public sealed class MySingleton : PersistentBehavior<MySingleton>
//  {
//      private MySingleton() :
//          base(CtorArg)
//      {}
//
//      protected override void Awake()
//      {
//			base.Awake();
/* Your code here */
//      }
//  }

public class PersistentBehavior<T> : MonoBehaviour
    where T : PersistentBehavior<T>
{
	protected struct CtorArgType { }

	

	private static T InstancePtr;

	protected static readonly CtorArgType CtorArg;

	protected PersistentBehavior(CtorArgType arg)
	{

	}

	protected virtual void Awake()
	{
		if (InstancePtr == null)
		{
			InstancePtr = (T)this;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

    protected static T Instance
    {
        get
        {
			string typeName = "PersistentBehavior<" + typeof(T).ToString() + ">";

			Debug.Assert(InstancePtr != null, 
				typeName + " is not initialized");

			if (InstancePtr != null)
			{
				Debug.Assert(InstancePtr.gameObject.scene.name == Helper.PersistentSceneName, 
					typeName + " has not been initialized on the scene " + Helper.PersistentSceneName);
			}

            return InstancePtr;
        }
    }

    protected static GameObject GoInstance
    {
        get
        {
            return Instance.gameObject;
        }
    }
}
