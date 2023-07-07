using System.Collections;
using UnityEngine;
using DG.Tweening;

public class DamageObjectScript : MonoBehaviour
{
    [SerializeField] ParticleEffecter _particleSystem;
    [SerializeField] HP _healthPoints;
    private void Awake()
    {
        _healthPoints.OnHPChanged = ShakeObject;
        _healthPoints.OnHPZero += _particleSystem.StartParticle;
       //_healthPoints.OnHPZero+=Destroy;
    }
    private void ShakeObject(int changedHP)
    {
        gameObject?.transform.DOShakeScale(0.5f, 0.5f, 5, 0);
    }
    private void Destroy()
    {
        IEnumerator coroutine = DelayDestroy(0.1f);
        StartCoroutine(coroutine);

    }
    private IEnumerator DelayDestroy(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameObject.Destroy(gameObject);
    }
}
