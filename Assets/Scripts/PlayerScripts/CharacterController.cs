using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    
    
    public int MoneyCount = 0;

    private Camera _mainCamera;
    [SerializeField] private MovementController _movementController;
    [SerializeField] private InputController _inputController;
    [SerializeField] private ShootController _shootController;
    [SerializeField] private CharacterAnimator _animationCharacterController;
    [SerializeField] private ContactedObjectsController _contactController;
    [SerializeField] private CollissionControl _collissionController;
    [SerializeField] private TriggerControl _triggerController;
    [SerializeField] private CharacterHP _healthController;
    [SerializeField] private BonusController _bonusController;
    [SerializeField] private CharacterAudio _audioController;
     // Start is called before the first frame update

    void Start()
    {
        _mainCamera = Camera.main;

        //What actions should be called on swipe actions from input
        _inputController.SwipeUpAction += _movementController.Jump;
        _inputController.SwipeUpAction += _audioController.PlayJumpAudio;
        _inputController.SwipeDownAction += _movementController.SlideDown;
        _inputController.SwipeXAction += _movementController.MoveByZ;
        _inputController.TouchAction += TouchControl;


        //What functions should be called on collission enter on bullet or person
        _shootController.onHittedTarget += _contactController.CheckShootedAction;
        _collissionController.OnCollission += _contactController.CheckContactedObject;
        _triggerController.OnTrigger += _contactController.CheckContactedObject;

        //How player should react on contacted Objects
        _contactController.onDamageObjectAction += OnHarm; 
        _contactController.onCoinAction += OnCoinHit;
        _contactController.onBonusAction += _bonusController.ActivateBonus;

        //What should we do if HP changes
        _healthController.OnHPChanged += _animationCharacterController.PlayHarm;
        _healthController.OnHPChanged += (obj) => { _audioController.PlayHarmAudio(); };
        _healthController.OnHPZero += _animationCharacterController.PlayDeath;
        
        _healthController.OnHPZero += () =>
        {
            _inputController.SwipeUpAction = null;
            _inputController.SwipeDownAction = null;
            _inputController.SwipeXAction = null;
        };
        _healthController.OnHPZero += _audioController.PlayHarmAudio;
    }

    private void TouchControl(Vector2 inputPos)
    {
        Ray inputRay= _mainCamera.ScreenPointToRay(inputPos);
        RaycastHit hit;
        if (Physics.Raycast(inputRay,out hit, Mathf.Infinity, LayerMask.GetMask("UI")))
        {
            Debug.Log("We hitted smth");
        }
        else
        {
            if (_shootController.AmountOfBullets > 0)
            {
                _animationCharacterController.PlayShoot(_shootController.Shoot);
            }
            else
            {
                _audioController.PlayNonShotAudio();
            }
        }
        
    }
    private void OnHarm(GameObject obj)
    {
        _healthController.HealthPoints -= 1;
    }
    private void OnCoinHit(GameObject obj)
    {
        GlobalVariables.instance.AmountOfMoney += 1;
    }
}
