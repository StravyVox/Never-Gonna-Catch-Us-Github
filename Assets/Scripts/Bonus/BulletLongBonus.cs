using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletLongBonus : Bonus
{
    [SerializeField] private GameObject _newBullet;
    [SerializeField] private GameObject _particleObjectPrefab;
    private GameObject _instantiatedPrefab;
    private List<Action> _delayedActions;
    private GameObject _oldBullet;
    private ShootController _shootScript;
    internal override void EndFunction()
    {
        _shootScript.onShoot = null;
        if (_delayedActions != null)
        {
            foreach (Action action in _delayedActions)
            {

                _shootScript.onShoot += action;
            }
        }
        onBonusEnd?.Invoke();
        if (_instantiatedPrefab != null)
        {
            _instantiatedPrefab.GetComponent<ParticleSystem>().Stop(true);
            GameObject.Destroy(_instantiatedPrefab, 1F);
        }
        if (_newBullet != null)
        {
            _shootScript.Bullet = _oldBullet;
        }
    }

    internal override float StartFunction()
    {
        if (_particleObjectPrefab != null)
        {
            _instantiatedPrefab = Instantiate(_particleObjectPrefab);

            _instantiatedPrefab.transform.parent = Player.transform;
            _instantiatedPrefab.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        _shootScript = Player.GetComponent<ShootController>();
        if (_newBullet != null)
        {
            _oldBullet = _shootScript.Bullet;
            _shootScript.Bullet = _newBullet;
        }
        _shootScript.AmountOfBullets = GlobalVariables.instance.StartAmountOfBullets;
        _delayedActions = _shootScript.onShoot?.GetInvocationList().Cast<Action>().ToList();
        _shootScript.onShoot = () => {
            //Debug.Log("Bonus onShoot activated");
            _shootScript.AmountOfBullets++; };
        float workTime = PlayerPrefs.GetFloat(NameOfBonus, 3f);
        //Debug.Log(_delayedActions);
        return workTime;
    }
}
