using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Global Variables", menuName = "ScriptableObjects/Global Variables", order =1)]
public class GlobalVariablesObj : ScriptableObject
{
    public float _movementSpeed;
    public int _amountOfJumps;
    public float _playerSpeed;
    public int _maxHP;
    public int _startAmountOfBullets;
    public int _scoreCoefficient;
    public int _maxScore;
    public int _currentScore;
    public int _amountOfMoney;
    public int _settings;
    public float _audiosettings;

    public void SaveToPlayerPrefs()
    {
        PlayerPrefs.SetInt("AmountOfJumps", _amountOfJumps);
        PlayerPrefs.SetInt("MaxHP", _maxHP);
        PlayerPrefs.SetInt("StartAmountOfBullets ", _startAmountOfBullets);
        PlayerPrefs.SetInt("ScoreCoefficcient",_scoreCoefficient);
        PlayerPrefs.SetInt("MaxScore",_maxScore);
        PlayerPrefs.SetInt("AmountOfMoney",_amountOfMoney);
        PlayerPrefs.SetInt("Settings", _settings);
        PlayerPrefs.SetFloat("Sound", _audiosettings);

        PlayerPrefs.Save();
    }
    public void GetFromPlayerPrefs()
    {
        _amountOfJumps = PlayerPrefs.GetInt("AmountOfJumps", _amountOfJumps);
        _maxHP= PlayerPrefs.GetInt("MaxHP", _maxHP);
        _startAmountOfBullets = PlayerPrefs.GetInt("StartAmountOfBullets ", _startAmountOfBullets);
        _scoreCoefficient = PlayerPrefs.GetInt("ScoreCoefficcient", _scoreCoefficient);
        _maxScore = PlayerPrefs.GetInt("MaxScore", _maxScore);
        _amountOfMoney = PlayerPrefs.GetInt("AmountOfMoney", _amountOfMoney);
        _settings = PlayerPrefs.GetInt("Settings", _settings);
        _audiosettings = PlayerPrefs.GetFloat("Sound", _audiosettings);
    }
}
