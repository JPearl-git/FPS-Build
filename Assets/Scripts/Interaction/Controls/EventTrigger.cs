using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public List<Event> events = new List<Event>();
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
        {
            for (int i = 0; i < events.Count; i++)
                events[i].Activate();
        }

        Destroy(gameObject);
    }
}
