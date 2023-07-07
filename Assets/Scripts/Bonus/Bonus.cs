using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bonus : MonoBehaviour
{
    public Action onBonusEnd; //Action after bonus done
    internal GameObject _player; //player object with all attached scripts. Setted by BonusController
    [SerializeField] private string _nameOfBonus; //base name of bonus to get info from PlayerPrefs
    public GameObject Player { get => _player; set => _player = value; }
    [SerializeField] internal string NameOfBonus { get => _nameOfBonus; }
    private IEnumerator _timer;
    float _timeToWork;
    public void BonusFunction() //Call startfunc after delay call end func
    {
        _timeToWork = StartFunction();
        if (_timeToWork > 0)
        {
            _timer = Timer(EndFunction, _timeToWork);
            StartCoroutine(_timer);
        }
        else
        {
            onBonusEnd?.Invoke();
        }
    }
    public bool RestartBonus(Bonus bonusObject)
    {
        if(bonusObject.NameOfBonus == NameOfBonus)
        {
            StopCoroutine(_timer);
            _timer = Timer(EndFunction, _timeToWork);
            StartCoroutine(_timer);
            return true;
        }
        return false;
    }

    internal abstract float StartFunction();
    internal abstract void EndFunction();
    private IEnumerator Timer(Action backFunction, float timer)
    {
        yield return new WaitForSeconds(timer);
        backFunction.Invoke();
    }
}
