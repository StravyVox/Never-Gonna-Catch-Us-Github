using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerControl : MonoBehaviour
{
    public Action<GameObject> OnTrigger;
    [SerializeField] internal List<string> _tags;

    public virtual void OnTriggerEnter(Collider collision)
    {
       // Debug.Log("Triggered on "+ transform.gameObject.tag);
        if (_tags.Contains(collision.gameObject.tag)) { 
                OnTrigger?.Invoke(collision.gameObject);
           }
       
    }
}
