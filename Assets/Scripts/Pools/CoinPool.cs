using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool instance;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int count;
    [SerializeField] private List<GameObject> _objects;
    [SerializeField] private Transform _instPosition;
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        _objects = new List<GameObject>();
        for(int i =0; i < count; i++)
        {
           GameObject newObj = Instantiate(_prefab, _instPosition);
            newObj.SetActive(false);
            newObj.transform.parent = _instPosition;
            _objects.Add(newObj);
        }
    }
    public GameObject GetObject()
    {
        foreach(GameObject obj in _objects)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newCoin = Instantiate(_prefab);
        newCoin.SetActive(true);
        newCoin.transform.position = _instPosition.position;
        newCoin.transform.parent = _instPosition;
        _objects.Add(newCoin);
        return newCoin;
            }   
}
