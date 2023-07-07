using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStaticController : MonoBehaviour
{
    [SerializeField]private SceneGenerator _sceneMovementController;
    [SerializeField]private GameObject _player;
    private Camera _camera;
    private float _zDistance;
    
    private void Awake()
    {
        _player.GetComponent<MoveByZ>().onTarget = MoveSceneBackwards;
        _zDistance = _player.GetComponent<MoveByZ>()._zTargetPosition;
        _camera = Camera.main;
    }
    private void MoveSceneBackwards()
    {
        //Debug.Log("MoveSceneBackwardsActivated");
        Vector3 position = new Vector3(_player.transform.position.x, _player.transform.position.y, _player.transform.position.z - _zDistance);
        _player.transform.position = position;
       //GameObject lastgenLoc = _sceneMovementController._lastGeneratedLocation;
        //lastgenLoc.transform.position = new Vector3(lastgenLoc.transform.position.x, lastgenLoc.transform.position.y, lastgenLoc.transform.position.z - _zDistance);
        List<GameObject> listOfLocations = _sceneMovementController._locationsQueue;
        
        _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z - _zDistance);
        foreach (GameObject location in listOfLocations)
        {
            location.transform.position = new Vector3(location.transform.position.x, location.transform.position.y, location.transform.position.z - _zDistance);
        }
    }
}
