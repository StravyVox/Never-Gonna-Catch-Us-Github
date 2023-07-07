using System;
using UnityEngine;

public class ContactedObjectsController : MonoBehaviour
{
    public Action<GameObject> onCoinAction;
    public Action<GameObject> onBonusAction;
    public Action<GameObject> onDamageObjectAction;
    public Action<GameObject> basicActionOnObject;

    private void Awake()
    {
        if (basicActionOnObject == null)
        {
            try
            {
                basicActionOnObject = obj => obj.GetComponent<HP>().HealthPoints = 0;
            }
            catch
            {

            }
        }
    }
    public void CheckShootedAction(GameObject obj)//Invoke actions for objects that was hit by bullet
    {
        switch (obj.tag)
        {
            case "Coin":
                onCoinAction?.Invoke(obj);
                break;
            case "Bonus":
                onBonusAction?.Invoke(obj);
                break;
        }
    }
    public void CheckContactedObject(GameObject obj)//Invoke actions for objects that was hit by player
    {
       // Debug.Log("CheckObjectActivated");
        switch (obj.tag)
        {
            case "Coin":
                onCoinAction?.Invoke(obj);
                basicActionOnObject?.Invoke(obj);
                break;
            case "Bonus":
                //Debug.Log("Bonus Activated");
                onBonusAction?.Invoke(obj);
                //basicActionOnObject?.Invoke(obj);
                break;
            case "DamageObject":
                onDamageObjectAction?.Invoke(obj);
                basicActionOnObject?.Invoke(obj);
                break;
        }
    }
}
