using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _money;
    [SerializeField] private TextMeshProUGUI _score;
    [SerializeField] private GameObject _player;
    [SerializeField] private UIProgressBarScript _bulletProgress;
    [SerializeField] private Settings _settingsScriptableObject;
    private ShootController _shootController;
    [SerializeField] float _timeToUpdate;
    private void Start()
    {
        if (_money != null) {
            _money.text = GlobalVariables.instance.AmountOfMoney.ToString(); }
        if (_score != null)
        {
            _score.text = GlobalVariables.instance.CurrentScore.ToString();
        }
        _shootController = _player.GetComponent<ShootController>();
    
        _bulletProgress.MaxAmount = GlobalVariables.instance.StartAmountOfBullets;
        _bulletProgress.CurrentAmount = _shootController.AmountOfBullets;
       // _maxScore.text = GlobalVariables.instance.MaxScore.ToString();
        IEnumerator coroutine = uiUpdater();
        StartCoroutine(coroutine);
    }
    IEnumerator uiUpdater()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToUpdate);
            if (_money != null)
            {
                _money.text = GlobalVariables.instance.AmountOfMoney.ToString();
            }
            if (_score != null)
            {
                _score.text = GlobalVariables.instance.CurrentScore.ToString();
            }
            _bulletProgress.CurrentAmount = _shootController.AmountOfBullets;
        }
    }
}
