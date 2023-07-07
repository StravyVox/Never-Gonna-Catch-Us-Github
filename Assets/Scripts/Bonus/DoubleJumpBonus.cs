using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoubleJumpBonus : Bonus
{
    [SerializeField] private GameObject particleObjectPrefab;
    private GameObject _instantiatedPrefab;

    private int _baseAmountOfJumps;
    internal override void EndFunction()
    {
        GlobalVariables.instance.AmountOfJumps=_baseAmountOfJumps;
        Player.GetComponent<MovementController>().amountOfJumps = GlobalVariables.instance.AmountOfJumps;
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
        _baseAmountOfJumps = GlobalVariables.instance.AmountOfJumps;
        GlobalVariables.instance.AmountOfJumps=2;
        Player.GetComponent<MovementController>().amountOfJumps = GlobalVariables.instance.AmountOfJumps;
        float workTime = PlayerPrefs.GetFloat(NameOfBonus, 3);
        
        return workTime;
    }
}
