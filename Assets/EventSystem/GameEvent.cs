using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * class representing GameEvent
 */
[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new List<GameEventListener>();

    /**
     * calls OnEventRaised method of all listeners
     */
    public void Raise()
    {
        for(int i = listeners.Count -1; i>=0; i-- )
        {
            listeners[i].OnEventRaised();
        }
    }

    /**
     * adds listener to list of listeners
     */
    public void RegisterListener(GameEventListener listener)
    {
        listeners.Add(listener);
    }

    /**
     * removes listener from list of listeners
     */
    public void UnregisterListener(GameEventListener listener)
    {
        listeners.Remove(listener);
    }
}
