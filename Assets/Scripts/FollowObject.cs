using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class FollowObject : MonoBehaviour
{
    public GameObject ObjectToCheck;

    public float buffer = 1.0f;
    public float cameraMoveSpeed = 5.0f;

    private Camera mainCamera;
    private bool objectInView = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void FixedUpdate()
    {
        if (ObjectToCheck == null)
        {
            return;
        }

        // Check if the object is within the camera view
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(ObjectToCheck.transform.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
            objectInView = true;
            return;
        }
        else
        {
            objectInView = false;
        }

        // Move the camera to the object if it's not in view
        if (!objectInView)
        {
            //mainCamera.transform.DOMoveZ(ObjectToCheck.transform.position.z, 0.2f);
            mainCamera.transform.DOMoveY(ObjectToCheck.transform.position.y+2, 0.3f);
            if(ObjectToCheck.transform.position.y <= -10)
            {
                SceneManager.LoadScene(1);
            }
        }

        // Check if the object is now within the camera view
        screenPoint = mainCamera.WorldToViewportPoint(ObjectToCheck.transform.position);
        if (screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1)
        {
            objectInView = true;
        }
    }
}
