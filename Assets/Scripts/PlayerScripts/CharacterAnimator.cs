using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private AnimationControl _animationController;
    [SerializeField] private GameObject _deathParticles;
    [SerializeField] private GameObject _deathSquad;
    public void PlayHarm(int amountOfHpChanged)
    {
        if(amountOfHpChanged < 0)
        _animationController.SetBoolAnimation("isDamaged", 0.3f);
    }
    public void PlayDeath()
    {
        gameObject.GetComponent<MoveByZ>().enabled = false;
        gameObject.GetComponent<CharacterHP>().OnHPZero = ()=>{ };
        Camera.main.GetComponent<MoveByZ>().enabled = false;
        _animationController.SetInfiniteAnimation("isDead");
        GameObject squad = Instantiate(_deathSquad);
        squad.transform.position = new Vector3(transform.position.x, 0.2f , transform.position.z+10);

        AnimationControl[] animators = squad.GetComponentsInChildren<AnimationControl>();
        foreach(AnimationControl animator in animators)
        {
            
            animator.SetInfiniteAnimationWithLogic("isSpotted", 1.35f, () => {  
                animator.gameObject.GetComponent<MoveByZ>().enabled = true;
                animator.gameObject.GetComponent<MoveByZ>()._zTargetPosition = transform.position.z-20;
            });
        }
        IEnumerator deathParticles = SetDeathParticles(3.5f);
        StartCoroutine(deathParticles);

    }
    public void PlayShoot(Action logicToBeExecuted)
    {
        _animationController.SetBoolAnimationWithLogic("isShot", 0.3f, 0.2f, logicToBeExecuted);
    }
    private IEnumerator SetDeathParticles(float delay)
    {
        yield return new WaitForSeconds(delay);
        _deathParticles.SetActive(true);
    }
}
