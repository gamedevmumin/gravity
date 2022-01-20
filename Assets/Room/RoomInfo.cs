using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * class representing info about room, its id and current state of gravity in t
 */
[System.Serializable]
public class RoomInfo
{
    [SerializeField] public string Id;
    [SerializeField] public Vector2 Gravity;
}
