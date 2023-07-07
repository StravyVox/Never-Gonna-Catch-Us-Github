using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class LocationObject
{
    public GameObject _mainLocation;
    public List<GameObject> _preLocations;//
    public List<string> _prelocationNames;
    private string regularExprForPrelocs = @"\w+(tTo)|\w+(nTo)";
    public LocationObject(GameObject mainLocation, List<GameObject> preLocations)
    {
        this._mainLocation = mainLocation;
        this._preLocations = preLocations;  
        getPrelocNames();
    }
    private void getPrelocNames()
    {
        _prelocationNames = new List<string>();
        foreach(GameObject preloc in _preLocations)//Change prolocationNames to the names of their base locations
        {
            string prelocName = preloc.name;
            Match match = Regex.Match(prelocName, regularExprForPrelocs);
            if (match.Success)
            {
                _prelocationNames.Add(match.Value.Substring(0,match.Value.Length-2));
            }
        }
    }
    private GameObject FindJunctionPreLocation(GameObject currentObject)
    {

        if (currentObject.name == _mainLocation.name)
        {
            return _mainLocation;
        }
        for(int i = 0; i< _prelocationNames.Count; i++)
        {
            if(currentObject.name == _prelocationNames[i])
            {
                return _preLocations[i];
            }
        }
        return null;
    }
    public List<GameObject> GetFullLocation(GameObject lastgeneratedLocation) //Find junction location between this.object location and last generated location
    {
        //delete clone from the name
        lastgeneratedLocation.name =  lastgeneratedLocation.name.Replace("(Clone)", "");
        
        List<GameObject> result = new List<GameObject>();//Resulted territories
        if (FindJunctionPreLocation(lastgeneratedLocation).name ==_mainLocation.name) { //if last generated location is the same as this location
            result.Add(FindJunctionPreLocation(lastgeneratedLocation));
            return result;
        }
        else if(FindJunctionPreLocation(lastgeneratedLocation)!=null)
        {
            result.Add(FindJunctionPreLocation(lastgeneratedLocation));
            result.Add(_mainLocation);
            return result;
        }
        else
        {

            return null;
        }
    }
}
