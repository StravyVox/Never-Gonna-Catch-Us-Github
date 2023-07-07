using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToLive : MonoBehaviour
{
    public Action OnTTLZero;
    public float TTL;

    // Update is called once per frame
    void FixedUpdate()
    {
        if( TTL>0)
        {
            TTL-= 0.02f;
            if( TTL<=0 )
            OnTTLZero?.Invoke();    
        }
    }

}
