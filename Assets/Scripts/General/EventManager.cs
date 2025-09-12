using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public Dictionary<string, Action> eventListeners = new Dictionary<string, Action>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Register(string eventName, Action listener)
    {
        if (!eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName] = null;
        }
        eventListeners[eventName] += listener;
    }

    public void Unregister(string eventName, Action listener)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName] -= listener;
        }
    }

    public void Trigger(string eventName)
    {
        if (eventListeners.ContainsKey(eventName))
        {
            eventListeners[eventName]?.Invoke();
        }
    }
}
