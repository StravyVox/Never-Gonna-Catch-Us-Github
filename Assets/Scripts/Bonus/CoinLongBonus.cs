using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinLongBonus : Bonus
{
    [SerializeField] private GameObject particleObjectPrefab;
    private GameObject _instantiatedPrefab;

    private List<Action<GameObject>> _delayedActions;

    private ContactedObjectsController _contactScript;
    internal override void EndFunction()
    {
        _contactScript.onCoinAction = null;
        foreach (Action<GameObject> action in _delayedActions)
        {

            _contactScript.onCoinAction += action;
        }
        onBonusEnd?.Invoke();
        if (_instantiatedPrefab != null)
        {
            _instantiatedPrefab.GetComponent<ParticleSystem>().Stop(true);
            GameObject.Destroy(_instantiatedPrefab, 1F);
        }
    }

    internal override float StartFunction()
    {
        if (particleObjectPrefab != null)
        {
            _instantiatedPrefab = Instantiate(particleObjectPrefab);

            _instantiatedPrefab.transform.parent = Player.transform;
            _instantiatedPrefab.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        _contactScript = Player.GetComponent<ContactedObjectsController>();
        _delayedActions = _contactScript.onCoinAction.GetInvocationList().Cast<Action<GameObject>>().ToList();
        _contactScript.onCoinAction = (gameobj) => { GlobalVariables.instance.AmountOfMoney += 2; };
        float workTime = PlayerPrefs.GetFloat(NameOfBonus, 3);
        Debug.Log(_delayedActions);
        return workTime;
    }
}
