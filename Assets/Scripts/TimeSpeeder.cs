using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSpeeder : MonoBehaviour
{
    [SerializeField] private float maxSpeed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float timeForUpdate;
    [SerializeField] private float speedStep;
    void Start()
    {
        GlobalVariables.instance.MovementSpeed = minSpeed;
        IEnumerator coroutine = updateTime();
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    IEnumerator updateTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeForUpdate);

            if (GlobalVariables.instance.MovementSpeed <= maxSpeed && GlobalVariables.instance.MovementSpeed != 0)
            {
                float newSpeed = GlobalVariables.instance.MovementSpeed + speedStep;
                if (newSpeed > maxSpeed)
                {
                    newSpeed = maxSpeed;
                    GlobalVariables.instance.MovementSpeed = newSpeed;
                    break;
                }
                GlobalVariables.instance.MovementSpeed = newSpeed;
            }
        }
    }
}
