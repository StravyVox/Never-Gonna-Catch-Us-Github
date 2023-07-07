using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrigger : TriggerControl
{
    public override void OnTriggerEnter(Collider collission)
    {
            if (_tags.Contains(collission.gameObject.tag))
            {
                OnTrigger?.Invoke(collission.gameObject);
            }
    }
}
