using System;
using UnityEngine;

public class HP : MonoBehaviour
{
    public Action OnHPZero;
    public Action<int> OnHPChanged;
    public Boolean DestroyOnZero;
    [SerializeField] public Boolean TurnOffOnZero;
    [SerializeField] internal int _healthpoints;
    private int _basehp;
    public int HealthPoints { get => _healthpoints; set => HPChanged(value); }
    private void Awake()
    {
        if (_healthpoints==0)
        {
            _healthpoints = 1;
        }
        _basehp = _healthpoints;
    }
    void HPChanged(int value)
    {
        int difference = value - _healthpoints;
        _healthpoints = value;
        if(_healthpoints <= 0)
        {
            OnHPZero?.Invoke();
            if(DestroyOnZero)
            {
                GameObject.Destroy(gameObject);
            }
            else if(TurnOffOnZero)
            {
                gameObject.SetActive(false);
                _healthpoints = _basehp;
            }
        }
        else
        {
            OnHPChanged?.Invoke(difference);
        }
    }
}
