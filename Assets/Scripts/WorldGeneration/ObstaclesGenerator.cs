using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObstaclesGenerator : MonoBehaviour
{
    
    [SerializeField] private BonusGenerator _bonusGenerator;
    [SerializeField] private List<GameObject> _firstHalfObstacles;
    [SerializeField] private List<GameObject> _secondHalfObstacles;
    [SerializeField] private CoinsGenerator _coinsGenerator;
    [SerializeField] private int _minimumAmountOfObstacles;
    [SerializeField] private int _maximumAmountOfObstacles;
    [SerializeField] private Transform _parentForInstance;
    public void StartGenerate()
    {
            
            int minOffset = 70 / _minimumAmountOfObstacles;
            int maxOffset = 70 / _maximumAmountOfObstacles;
            GameObject instantiatedObject;
            Vector3 newPosition = new Vector3(0, 0, transform.position.z + 10);
            Vector3 oldPosition = newPosition;
            ObstaclesPool pool = ObstaclesPool.instance;
            for (int i = 0; i < _maximumAmountOfObstacles; i++)
            {
                if (newPosition.z >= transform.position.z + 80)
                {
                    return;
                }
                if (newPosition.z <= transform.position.z + 40)
                {
                //instantiatedObject = Instantiate(firstHalfObstacles[Random.Range(0, firstHalfObstacles.Count)]);

                instantiatedObject =  pool.FindPrefab(_firstHalfObstacles[Random.Range(0, _firstHalfObstacles.Count)]);
            }
                else
                {
                   //instantiatedObject = Instantiate(secondHalfObstacles[Random.Range(0, secondHalfObstacles.Count)]);

                    instantiatedObject = pool.FindPrefab(_secondHalfObstacles[Random.Range(0, _secondHalfObstacles.Count)]);
            }
                instantiatedObject.transform.position = newPosition;
                instantiatedObject.transform.parent = transform;
                instantiatedObject.SetActive(true);
                GenerateOnObject(newPosition);
                newPosition.z += Random.Range(minOffset,maxOffset);
                //For Coins
                GenerateLineOfCoins(newPosition, oldPosition);
                oldPosition = newPosition;
            }
    }
    private void GenerateOnObject(Vector3 newPosition)
    {
        if (Random.Range(1, 3) == 1)
        {
            _coinsGenerator?.GenerateOnObject(newPosition, _parentForInstance);
        }
        else
        {
            _bonusGenerator?.GenerateOnObject(newPosition, _parentForInstance);
        }
    }
    private void GenerateLineOfCoins(Vector3 newPos, Vector3 oldPos)
    {
        if (_coinsGenerator) 
        {
            float zDistance = newPos.z - oldPos.z;
            int amountOfLines = (int)zDistance / 10;
            for (int i = 0; i < amountOfLines; i++)
            {
                _coinsGenerator.GenerateLine(oldPos, _parentForInstance);
                oldPos.z += 10;
            } 
        }
    }
    public void TurnOffGenerated()
    {
        for(int i = 0; i< _parentForInstance.childCount; i++)
        {
            Transform obj = _parentForInstance.GetChild(i);
            obj.gameObject.SetActive(false);
        }
    }
}
