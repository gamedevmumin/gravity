using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStateListener
{
    public void ReactOnStateChange(bool state);
}
