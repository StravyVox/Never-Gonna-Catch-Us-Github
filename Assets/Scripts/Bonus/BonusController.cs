using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BonusController : MonoBehaviour
{
    private Camera _camera;
    private List<Bonus> _bonuses;
    private void Start()
    {
        _camera = Camera.main;
        _bonuses = new List<Bonus>();
    }
    public void ActivateBonus(GameObject BonusObject)
    {
        foreach (Bonus bonusObj in _bonuses)
        {
            if (bonusObj.RestartBonus(BonusObject.GetComponent<Bonus>()))
            {
                //GameObject.Destroy(BonusObject);
                BonusObject.SetActive(false);
                return;
            }
        }
        BonusObject.transform.parent = _camera.transform;
        BonusObject.transform.position = new Vector3(0, -10, 0);
        Bonus bonus = BonusObject.GetComponent<Bonus>();
        _bonuses.Add(bonus);
        bonus.onBonusEnd = () =>
        {
            _bonuses.Remove(bonus);
            BonusObject.SetActive(false);
            //GameObject.Destroy(BonusObject);
        };
        bonus.Player = gameObject;
        bonus.BonusFunction();
    }
}
