using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class InputController : MonoBehaviour
{
    public Action SwipeUpAction;
    public Action<Vector2> SwipeXAction;
    public Action SwipeDownAction;
    public Action<Vector2> TouchAction;

    [SerializeField] private int ignoredPixels = 40;
    private bool _Swiped;
    private bool _isTouch;


    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    _isTouch = true;
                    _Swiped = false;
                    break;
                case TouchPhase.Moved:
                    _isTouch = false;
                    if (!_Swiped)
                    {   
                        if (touch.deltaPosition.y >= ignoredPixels && !_Swiped)
                        {
                            _Swiped = true;
                            SwipeUpAction?.Invoke();
                        }
                        else if (touch.deltaPosition.y <= -ignoredPixels && !_Swiped)
                        {
                            _Swiped = true;
                            SwipeDownAction?.Invoke();
                        }
                       else if ((touch.deltaPosition.x <= -ignoredPixels || touch.deltaPosition.x > ignoredPixels)!)
                        {
                            _Swiped = true;
                            SwipeXAction?.Invoke(touch.deltaPosition);
                        }
                    }
                    break;
                case TouchPhase.Ended:
                    if (_isTouch)
                    {
                        TouchAction?.Invoke(touch.position);
                    }
                    break;
            }
        }
    }
}
