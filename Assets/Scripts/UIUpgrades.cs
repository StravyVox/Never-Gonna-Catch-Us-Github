using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIUpgrades : MonoBehaviour
{
    [SerializeField] string _nameOfStat;
    [SerializeField] float _baseLevelOfStat;
    [SerializeField] float _maximumLevelOfStat;
    [SerializeField] float _stepOfUpgrade;

    [SerializeField] int _firstPayPrice;
    [SerializeField] int _percentByStep;
    [SerializeField] UIProgressBarScript _progressBar;
    [SerializeField] TextMeshProUGUI _priceUI;
    [SerializeField] bool _isPlayerPrefs = true;
    public int _currentPayPrice;
    float _currentAmount;

    private void Start()
    {
        UpdateCurrentAmount();
        _progressBar.MaxAmount = _maximumLevelOfStat;
        _progressBar.CurrentAmount = _currentAmount;
        updatePayPrice();
    }
    private void UpdateCurrentAmount()
    {
        if (_isPlayerPrefs)
        {
            _currentAmount = PlayerPrefs.GetFloat(_nameOfStat, _baseLevelOfStat);

        }
        else
        {
            _currentAmount = GlobalVariables.instance.GetByString(_nameOfStat);
            if (_currentAmount == 0)
            {
                _currentAmount = _baseLevelOfStat;
            }
        }
    }
    private void updatePayPrice()
    {
        float amount = _currentAmount;
        amount -= _baseLevelOfStat;
        int currentstep = (int)(amount / _stepOfUpgrade);
        _currentPayPrice = _firstPayPrice;
        for(int i = 0; i < currentstep; i++) { 
            _currentPayPrice += ((_currentPayPrice/100)*_percentByStep);
        }
        if (_currentAmount >= _maximumLevelOfStat)
        {
            _priceUI.text = "MAX";
        }
        else
        {
            _priceUI.text = _currentPayPrice.ToString();
        }
    }
    public void UpgradeStat()
    {
        Debug.Log("UpgradeStat()");
        float result;
        if (_isPlayerPrefs)
        {
            result = PlayerPrefs.GetFloat(_nameOfStat, _baseLevelOfStat);
        }
        else
        {
            result = GlobalVariables.instance.GetByString( _nameOfStat);
        }
        if(result < _maximumLevelOfStat && _currentPayPrice <= GlobalVariables.instance.AmountOfMoney)
        {
            Debug.Log(GlobalVariables.instance.AmountOfMoney);
            Debug.Log(result);
            result += _stepOfUpgrade;
            if(result> _maximumLevelOfStat)
            {
                result = _maximumLevelOfStat;
            }
            if (_isPlayerPrefs)
            {
                PlayerPrefs.SetFloat(_nameOfStat, result);
                PlayerPrefs.Save();
            }
            else
            {
                GlobalVariables.instance.SetByString(_nameOfStat, (int)result);
            }
            GlobalVariables.instance.AmountOfMoney -= _currentPayPrice;
            UpdateCurrentAmount();
            _progressBar.CurrentAmount = _currentAmount;
            updatePayPrice();
        }

    }
}
