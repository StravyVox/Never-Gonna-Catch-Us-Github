using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollissionControl : MonoBehaviour
{

    public Action<GameObject> OnCollission;
    [SerializeField] private List<string> _tags;


    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("CollissionEnter on ColCOntrol on object: " + gameObject.name);
        foreach(string tag in _tags)
        {
            if(tag == collision.gameObject.tag)
            {
                OnCollission?.Invoke(collision.gameObject);
            }
        }
    }
    
}
