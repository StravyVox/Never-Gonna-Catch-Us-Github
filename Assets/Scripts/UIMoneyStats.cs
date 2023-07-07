using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIMoneyStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    private GlobalVariables _globalVariables;
    private void Start()
    {
        _globalVariables = GlobalVariables.instance;
        _moneyText.text = _globalVariables.AmountOfMoney.ToString();
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        _moneyText.text = _globalVariables.AmountOfMoney.ToString();
    }
}
