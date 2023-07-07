using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Settings _currentSettings;
    [SerializeField] private CharacterHP _playerHP;
    [SerializeField] private UIButtonsController _buttonsController;
    [SerializeField] private float _timeToUpdate;
    [SerializeField] private bool _scorecount = true;
    private bool _gameOver;
    void Start()
    {
        QualitySettings.SetQualityLevel(_currentSettings.SettingsState);
        AudioListener.volume = _currentSettings.AudioValue;
        _gameOver = false;
        if (_scorecount)
        {
            IEnumerator updatescore = fixedScoreUpdate();
            StartCoroutine(updatescore);
        }
        if (_playerHP != null)
        {
            _playerHP.OnHPZero += () =>
            {
                _gameOver = true;
                IEnumerator deathvar = death();
                StartCoroutine(deathvar);
            };
        }
        if (_buttonsController != null)
        {
            _buttonsController.OnSaveSettings = () =>
            {
                _currentSettings.AudioValue = AudioListener.volume;
                _currentSettings.SettingsState = QualitySettings.GetQualityLevel();
            };
        }
    }
    IEnumerator death()
    {
        IEnumerator updatescore = fixedScoreUpdate();
        StopCoroutine(updatescore);
        yield return new WaitForSeconds(6f);
        _buttonsController.OpenDeathMenu();
    }
    IEnumerator fixedScoreUpdate()
    {
        
        while (!_gameOver)
        {
            yield return new WaitForSeconds(_timeToUpdate);
            GlobalVariables.instance.CurrentScore += 1 * GlobalVariables.instance.ScoreCoefficient;
        }
    }
}
