using System.Collections;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    [SerializeField]private Animator _animator;
    private bool _isReturningWorking;
    private void Awake()
    {
        _isReturningWorking = false;
    }
    public void SetInfiniteAnimation(string boolName, bool state = true)
    {
        _animator.SetBool(boolName, state);
    }
    public void SetInfiniteAnimationWithLogic(string boolName, float delayForLogic, Action Logic)
    {
        _animator.SetBool(boolName, true);
        IEnumerator logicCoroutine = DelayForLogic(delayForLogic, Logic);
        StartCoroutine(logicCoroutine);
    }
    public void SetBoolAnimation(string boolName, float animationDuration)
    {
        _animator.SetBool(boolName, true);
        IEnumerator coroutine = ReturnAnimation(_animator, boolName, animationDuration);
        if (_isReturningWorking)
        {
            //Debug.Log("Stopping coroutine");
            StopCoroutine(coroutine);
            StartCoroutine(coroutine);
        }
        else
        {
            StartCoroutine(coroutine);
        }
    }
    public void SetBoolAnimationWithLogic(string boolName, float animationDuration, float delayForLogic, Action Logic)
    {
        _animator.SetBool(boolName, true);
        IEnumerator coroutine = ReturnAnimation(_animator, boolName, animationDuration);
        IEnumerator logicCoroutine = DelayForLogic(delayForLogic, Logic);
        if (_isReturningWorking)
        {
            //Debug.Log("Stopping coroutine");
            StopCoroutine(coroutine);
        }
        else
        {
            StartCoroutine(coroutine);
        }
        StartCoroutine(logicCoroutine);
    }
    private IEnumerator DelayForLogic(float amountOfTime, Action logic)
    {
        yield return new WaitForSeconds(amountOfTime);
        logic?.Invoke();
    }
    private IEnumerator ReturnAnimation(Animator animator,string variable, float amountOfTime)
    {
        _isReturningWorking = true;
        yield return new WaitForSeconds(amountOfTime);
        animator.SetBool(variable, false);
        _isReturningWorking = false;
    }
}
