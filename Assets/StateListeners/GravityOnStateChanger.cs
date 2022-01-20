using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityOnStateChanger : MonoBehaviour, IStateListener
{
    [SerializeField] private Vector2 gravityOnTrue;
    [SerializeField] private Vector2 gravityOnFalse;
    [SerializeField] private Room.Room room;
    
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
