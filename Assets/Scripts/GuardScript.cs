using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour
{
    [SerializeField] private AnimationControl _animationController;
    [SerializeField] private TriggerControl _triggerController;
    void Start()
    {
        _triggerController.OnTrigger = StartMovingTowardsPlayer;
    }
    void StartMovingTowardsPlayer(GameObject player)
    {
        _animationController.SetInfiniteAnimationWithLogic("isSpotted", 0.65f, () => { gameObject.GetComponent<MoveByZ>().enabled = true; } );
        IEnumerator coroutine = TurnOff();
        StartCoroutine(coroutine);
    }
   
    IEnumerator TurnOff()
    {
        gameObject.GetComponent<HP>().OnHPZero += ()=>gameObject.GetComponent<MoveByZ>().enabled = false;
        yield return new WaitForSeconds(2f);
        gameObject.GetComponent<MoveByZ>().enabled = false;

        _animationController.SetInfiniteAnimation("isSpotted", false);
        
    }
}
