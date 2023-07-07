using System;
using UnityEngine;

public class ShootController : MonoBehaviour
{

    public Action onShoot;
    public Action<GameObject> onHittedTarget;
    public Action onAddedBullets;


    public int AmountOfBullets { get => _amountOfBullets; set 
        {
            _amountOfBullets = value;
            onAddedBullets?.Invoke();
        } }

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _shootPosition;
    private Camera _camera;
    private int _amountOfBullets;
    public GameObject Bullet { get=>_bullet; set=>_bullet=value; }

    private void Start()
    {
        _camera = Camera.main;
        _amountOfBullets = GlobalVariables.instance.StartAmountOfBullets;
    }
    public void Shoot()
    {
        if (_amountOfBullets > 0)
        {
            _amountOfBullets--;
            onShoot?.Invoke();
            GameObject bullet = Instantiate(_bullet);
            bullet.transform.position = _shootPosition.position;
            bullet.transform.parent = _camera.transform;
            if (onHittedTarget != null)
            {
                bullet.gameObject.GetComponent<Bullet>().Hit = onHittedTarget;
            }
        }
    }
}
