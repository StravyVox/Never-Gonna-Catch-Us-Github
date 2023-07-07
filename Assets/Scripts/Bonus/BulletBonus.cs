using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBonus : Bonus
{
    [SerializeField] private GameObject particleObjectPrefab;
    private GameObject _instantiatedPrefab;
    internal override void EndFunction()
    {
        if (_instantiatedPrefab != null)
        {
            _instantiatedPrefab.GetComponent<ParticleSystem>().Stop(true);
            GameObject.Destroy(_instantiatedPrefab, 1F);
        }
        onBonusEnd?.Invoke();
    }

    internal override float StartFunction()
    {
        if (particleObjectPrefab != null)
        {
            _instantiatedPrefab = Instantiate(particleObjectPrefab);

            _instantiatedPrefab.transform.parent = Player.transform;
            _instantiatedPrefab.transform.localPosition = new Vector3(0, 1.5f, 0);
        }
        ShootController shootScript = Player.GetComponent<ShootController>();
            shootScript.AmountOfBullets = GlobalVariables.instance.StartAmountOfBullets;
        return 1F;
    }
}
