using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Action<GameObject> Hit;//send hitted object to every script attached
    
    [SerializeField] private HP _healthPoints;
    [SerializeField] private TimeToLive _ttl;
    [SerializeField] private CollissionControl _collissionControl;
    [SerializeField] private TriggerControl _triggerControl;
    [SerializeField] private int _damage;
    [SerializeField] private float _Speed;
    [SerializeField] private Vector3 _moveDirection;

    public int Damage { get => _damage; set => _damage = value; }

    private void Awake()
    {
        _triggerControl.OnTrigger = OnBulletHit;
        _collissionControl.OnCollission = OnBulletHit;
        _ttl.OnTTLZero = DestroyBullet;
        _healthPoints.OnHPZero = DestroyBullet;
    }
    private void FixedUpdate()
    {
        this.transform.Translate(_moveDirection * _Speed * Time.deltaTime);
    }
    private void DestroyBullet()
    {
        Destroy(this.gameObject);
    }
    public virtual void OnBulletHit(GameObject collissionedObject)
    {
       // Debug.Log("OnBulletHit");
        Hit?.Invoke(collissionedObject);
        try
        {
           collissionedObject.GetComponent<HP>().HealthPoints -= _damage;
            _healthPoints.HealthPoints -= 1;
        }
        catch(Exception e)
        {
           // Debug.Log("Couldn't find HP");
           Debug.Log(e);
        }
    }
}
