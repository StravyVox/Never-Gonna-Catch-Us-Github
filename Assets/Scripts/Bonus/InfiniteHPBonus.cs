using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InfiniteHPBonus : Bonus
{
    [SerializeField] private GameObject particleObjectPrefab;
    private GameObject _instantiatedPrefab;
    private List<Action<GameObject>> _delayedActions;
    private ContactedObjectsController _contactScript;
    internal override void EndFunction()
    {
        _contactScript.onDamageObjectAction = null;
        foreach (Action<GameObject> action in _delayedActions) {

            _contactScript.onDamageObjectAction += action;
        }
        onBonusEnd?.Invoke();
        if(_instantiatedPrefab != null)
        {
            _instantiatedPrefab.GetComponent<ParticleSystem>().Stop(true);
            GameObject.Destroy(_instantiatedPrefab, 1F);
        }
    }

    internal override float StartFunction()
    {
        if( particleObjectPrefab != null )
        {
            _instantiatedPrefab = Instantiate(particleObjectPrefab);
            
            _instantiatedPrefab.transform.parent = Player.transform;
            _instantiatedPrefab.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        _contactScript = Player.GetComponent<ContactedObjectsController>();
        _delayedActions = _contactScript.onDamageObjectAction.GetInvocationList().Cast<Action<GameObject>>().ToList();
        Player.GetComponent<CharacterHP>().HealthPoints = GlobalVariables.instance.MaxHP;
        float workTime = PlayerPrefs.GetFloat(NameOfBonus, 3);
        _contactScript.onDamageObjectAction = null;
        Debug.Log("Infinite HP bonus activated");
        Debug.Log(_delayedActions);
        return workTime;
    }
}
    // Start is called before the first frame update
