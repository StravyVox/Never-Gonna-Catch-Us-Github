using System.Collections;
using System; 
using System.Collections.Generic;
using UnityEngine;

public class MoveByZ : MonoBehaviour
{
    public Action onTarget;
    [SerializeField] public float _zTargetPosition;
    [SerializeField] private Boolean _isForward = true;
    private Vector3 endPosition;
    private bool _ended;
    private void Start()
    {
        endPosition = new Vector3(transform.position.x, transform.position.y, _zTargetPosition+20);
        _ended = false;
    }
    void FixedUpdate()
    {

        transform.position = Vector3.MoveTowards(transform.position, endPosition, GlobalVariables.instance.MovementSpeed * 0.02f);
        if (_isForward)
        {
            if (transform.position == endPosition || transform.position.z > _zTargetPosition)
            {
                    onTarget?.Invoke();
                    _ended = true;
                //Destroy(this);
                //GameObject.Destroy(this.gameObject);

            }
        }
        else
        {

            if (transform.position == endPosition || transform.position.z < _zTargetPosition)
            {
                if (!_ended)
                {
                    onTarget?.Invoke();
                    _ended = true;
                }
                //Destroy(this);
                //GameObject.Destroy(this.gameObject);

            }
        }
    }
}
