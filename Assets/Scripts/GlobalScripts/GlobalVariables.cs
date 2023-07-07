using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables instance;
    [SerializeField] private GlobalVariablesObj _globalVariables;
    [SerializeField] private bool _rewriteToPrefs;
    public float MovementSpeed { get => _globalVariables._movementSpeed; set => _globalVariables._movementSpeed = value; }
    public int AmountOfJumps { get => _globalVariables._amountOfJumps; set => _globalVariables._amountOfJumps = value; }
    public float PlayerSpeed { get => _globalVariables._playerSpeed; set => _globalVariables._playerSpeed = value; }
    public int MaxHP { get => _globalVariables._maxHP; set => _globalVariables._maxHP = value; }
    public int StartAmountOfBullets { get => _globalVariables._startAmountOfBullets; set => _globalVariables._startAmountOfBullets = value; }
    public int ScoreCoefficient { get => _globalVariables._scoreCoefficient; set => _globalVariables._scoreCoefficient = value; }
    public int MaxScore { get => _globalVariables._maxScore; set => _globalVariables._maxScore = value; }
    public int CurrentScore { get => _globalVariables._currentScore; set => _globalVariables._currentScore = value; }
    public int AmountOfMoney { get => _globalVariables._amountOfMoney; set => _globalVariables._amountOfMoney = value; }

    public float SoundVolume { get => _globalVariables._audiosettings; set => _globalVariables._audiosettings = value; }
    public int Settings { get => _globalVariables._settings; set => _globalVariables._settings = value; }
    private void Awake()
    {
        if (_rewriteToPrefs)
        {
            _globalVariables.SaveToPlayerPrefs();
           
        }
        else { _globalVariables.GetFromPlayerPrefs();
            AudioListener.volume = this.Settings;
            QualitySettings.SetQualityLevel(Settings);
        }
        CurrentScore = 0;
        if (MovementSpeed == 0)
        {
            MovementSpeed = 5;
        }
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }
    public void SaveToPrefs()
    {
        _globalVariables.SaveToPlayerPrefs();
    }
    private void LoadFromPrefs()
    {
        _globalVariables.GetFromPlayerPrefs();
    }
    public void SaveSessionResults()
    {
        if(CurrentScore > MaxScore)
        {
            MaxScore = CurrentScore;
        }
    }
    public void SetByString(string stat, int value)
    {
        switch ( stat )
        {
            case "MaxHP": MaxHP = value; break;
            case "StartAmountOfBullets": StartAmountOfBullets = value; break;
            case "AmountOfJumps": AmountOfJumps = value; break;
        }
    }
    public int GetByString(string stat)
    {
        switch (stat)
        {
            case "MaxHP": return MaxHP;
            case "StartAmountOfBullets": return StartAmountOfBullets;
            case "AmountOfJumps": return AmountOfJumps;
        }
        return 0;
    }
}

