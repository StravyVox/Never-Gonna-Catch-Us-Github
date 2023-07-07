using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesPool : MonoBehaviour
{
    public static ObstaclesPool instance;
    [SerializeField] Transform _position;
    [SerializeField] int amountOfPregenerated;
    [SerializeField] List<GameObject> _locationsPrefabs;
    private List<GameObject> _instantiatedLocations;

    private void Awake()
    {
        _instantiatedLocations = new List<GameObject>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        foreach (GameObject prefab in _locationsPrefabs)
        {
            for (int i = 0; i < amountOfPregenerated; i++)
            {
                GameObject newObject = Instantiate(prefab);
                newObject.SetActive(false);
                newObject.transform.parent = _position.parent;
                newObject.transform.position = _position.position;
                newObject.name = newObject.name.Replace("(Clone)", "");
                _instantiatedLocations.Add(newObject);
            }
        }
    }

    public GameObject FindPrefab(GameObject expectedPrefab)
    {
        foreach (GameObject instLoc in _instantiatedLocations)
        {
            if (instLoc.name == expectedPrefab.name && instLoc.activeSelf == false)
            {
                return instLoc;
            }
        }
        GameObject newObject = Instantiate(expectedPrefab);
        newObject.transform.position = _position.position;
        newObject.name = newObject.name.Replace("(Clone)", "");
        newObject.SetActive(false);
        _instantiatedLocations.Add(newObject);
        return newObject;
    }
}
