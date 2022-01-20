using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * class representing game event listener
 */
public class GameEventListener : MonoBehaviour
{

    public GameEvent Event;
    public UnityEvent Response;

    private void OnEnable()
    {
        Event.RegisterListener(this);
    }

    private void OnDisable()
    {
        Event.UnregisterListener(this);
    }

    /**
     * calls Invoke method on Response field
     */
    public void OnEventRaised()
    {
        Response.Invoke();
    }
}
