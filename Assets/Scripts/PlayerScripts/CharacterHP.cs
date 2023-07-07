using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHP : HP
{

    [SerializeField] private UIProgressBarScript _uIProgressBarScript;
    [SerializeField] private GameObject _healthCanvas;
    private void Start()
    {
        _healthpoints = GlobalVariables.instance.MaxHP;
        DestroyOnZero = false;
        if(_uIProgressBarScript != null && _healthCanvas != null)
        {
            _healthCanvas.SetActive(false);
            _uIProgressBarScript.MaxAmount = GlobalVariables.instance.MaxHP;
            _uIProgressBarScript.CurrentAmount = _healthpoints;
            OnHPChanged += (diff) => {
                _healthCanvas.SetActive(true);
                _uIProgressBarScript.CurrentAmount = _healthpoints;
                IEnumerator coroutine = TurnOffCanvas();
                StartCoroutine(coroutine);
            };
        }
    }
    private IEnumerator TurnOffCanvas()
    {
        yield return new WaitForSeconds(3);
        _healthCanvas.SetActive(false);
    }
    
}
