using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneGenerator : MonoBehaviour
{
    
    public List<GameObject> _locationsQueue;
    public GameObject _lastGeneratedLocation;
    public int WaitSecondToRemoveLocation;
    [SerializeField] int AmountOfLocationsGenerated;
    [SerializeField] List<GameObject> MainLocations;
    [SerializeField] List<GameObject> prelocationsInOrder;
    List<LocationObject> _locations;
    int _currentamount = 0;
    private LocationsPool _locationsPool;
    // Start is called before the first frame update
    void Start()
    {
        _locations = new List<LocationObject>();
        getReadyLocations();
        spawnLocations();
    }
    private void getReadyLocations()
    {
        int amountofPrelocations = MainLocations.Count-1;
        for(int i = 0; i < MainLocations.Count; i++)
        {
            List<GameObject> preLocations = new List<GameObject>();
           for(int j = 0; j < amountofPrelocations; j++) {
                preLocations.Add(prelocationsInOrder[(i*amountofPrelocations)+j]);
            }
            _locations.Add(new LocationObject(MainLocations[i], preLocations));
        }
    }
    void regenerateLocations(GameObject invoker)
    {
        _currentamount--;
        while (_currentamount < AmountOfLocationsGenerated)
        {
            _currentamount += generateLocations();
        }
    }
    void spawnLocations()
    {
        while(_currentamount <= AmountOfLocationsGenerated)
        {
           _currentamount+=generateLocations();
        }
    }
   
    int generateLocations()
    {
        int locIndex = UnityEngine.Random.Range(0, MainLocations.Count);
        int resultOfAddedObjects = 0;
        Vector3 spawnposition;
        List<GameObject> generatedlocations;
        bool firstRun;
        _locationsPool = LocationsPool.instance;
        if (_lastGeneratedLocation != null)
        {
            generatedlocations = _locations[locIndex].GetFullLocation(_lastGeneratedLocation);
            spawnposition = _lastGeneratedLocation.transform.position;
            spawnposition.z += 80;
            firstRun = false;
        }
        else
        {
           generatedlocations = _locations[locIndex].GetFullLocation(_locations[locIndex]._mainLocation);
           spawnposition = new Vector3(-15,0,-30);
            firstRun = true;
        }
        foreach (GameObject loc in generatedlocations)
        {

            //Debug.Log("generateLocations: loop for gen Locations, amount of locs: " + generatedlocations.Count);
            //Debug.Log("generateLocations: loop for gen Locations, spawn position.z: " + spawnposition.z);

            // _lastGeneratedLocation = Instantiate(loc);
            _lastGeneratedLocation = _locationsPool.FindPrefab(loc);
            _lastGeneratedLocation.SetActive(true);
            _lastGeneratedLocation.gameObject.tag = "Ground";
            _lastGeneratedLocation.transform.position = spawnposition;
            _lastGeneratedLocation.GetComponent<TriggerControl>().OnTrigger = RemoveLocation;
            _lastGeneratedLocation.GetComponent<TriggerControl>().OnTrigger += regenerateLocations;
            _locationsQueue.Add(_lastGeneratedLocation);
            if (firstRun)
            {
                firstRun = false;
            }
            else
            {
                _lastGeneratedLocation.GetComponent<ObstaclesGenerator>().StartGenerate();
            }
            spawnposition.z += 80;
            resultOfAddedObjects++;
        }
        return resultOfAddedObjects;
    }
    void RemoveLocation(GameObject invoker)
    {
        GameObject location = _locationsQueue[0];
        _locationsQueue.Remove(location);
        Debug.Log("Invoker is" + invoker.name);
        Debug.Log("Removed " + location.name);
            if (location != null)
            {
                IEnumerator coroutine = TurnOff(location);
                StartCoroutine(coroutine);
                //Destroy(location.gameObject,8f);
            }
    }
    IEnumerator TurnOff(GameObject objectToOff)
    {
        yield return new WaitForSeconds(WaitSecondToRemoveLocation);
        objectToOff.SetActive(false);
        objectToOff.GetComponent<ObstaclesGenerator>().TurnOffGenerated();
    }
}
