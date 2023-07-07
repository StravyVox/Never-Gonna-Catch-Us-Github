using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusGenerator : MonoBehaviour
{
    [SerializeField] List<GameObject> _bonusList;
    public void GenerateOnObject(Vector3 pos, Transform parent)
    {
        BonusPool pool = BonusPool.instance;
        pos = new Vector3(pos.x, 5f, pos.z);
            int indexOfBonus = Random.Range(0, _bonusList.Count);
            //Debug.Log("GeneratingOnObject");
            //GameObject bonusObject = Instantiate(_bonusList[indexOfBonus]);
            GameObject bonusObject = pool.FindPrefab(_bonusList[indexOfBonus]);
            bonusObject.SetActive(true);
            bonusObject.transform.position = pos;
            bonusObject.transform.parent = parent;
    }
}
