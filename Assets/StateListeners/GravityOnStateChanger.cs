using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * class that represents object that reacts on change of some state
 */
public class GravityOnStateChanger : MonoBehaviour, IStateListener
{
    [SerializeField] private Vector2 gravityOnTrue;
    [SerializeField] private Vector2 gravityOnFalse;
    [SerializeField] private Room.Room room;
    
    /**
     * it takes state and changes gravity according to it
     * @param state - state to react to
     */
    public void ReactOnStateChange(bool state)
    {
        if (state)
        {
            room.ChangeRoomGravity(gravityOnTrue);
        }
        else
        {
            room.ChangeRoomGravity(gravityOnFalse);
        }
    }
}
