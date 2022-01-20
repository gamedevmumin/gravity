using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateOwner : MonoBehaviour
{
    public abstract void LoadState(bool isActive);
    public abstract bool CheckID(string id);
    public abstract InteractableInfo GetInfo();
}
