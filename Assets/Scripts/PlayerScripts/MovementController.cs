using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MovementController : MonoBehaviour
{

    public float JumpForce = 3.0f;
    public int amountOfJumps;
    public bool CanJump = true;
    public bool CanMove = true;
    
    private float _playerSpeed;
    private Rigidbody _body;
    IEnumerator _jumpAwaker;
    private Camera _mainCamera;
    private  void Start()
    {
        _playerSpeed = GlobalVariables.instance.PlayerSpeed;   
        amountOfJumps = GlobalVariables.instance.AmountOfJumps;
        _jumpAwaker = ReturnJump();
        _body = GetComponent<Rigidbody>();
        _mainCamera = Camera.main;
    }
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("OnCollissionEnter activated");
        //Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Untagged")
        {

           // Debug.Log("Stepped on ground");
            amountOfJumps = GlobalVariables.instance.AmountOfJumps;
            CanJump = true;
        }
    }
    public void MoveByZ(Vector2 touchDelta)
    {
         if (touchDelta.x < 0 && ((_mainCamera.transform.position.z - gameObject.transform.position.z)<6)) 
            {
                this.gameObject.transform.DOMoveZ(transform.position.z - _playerSpeed*0.5F, 0.2f);
            }
            else if (touchDelta.x > 0)
            {
                this.gameObject.transform.DOMoveZ(transform.position.z + _playerSpeed, 0.2f);
            }
    }
    public void Jump()
    {
        if (CanJump)
        {
            amountOfJumps -= 1;
            _body.AddForce(Vector3.up * JumpForce, ForceMode.VelocityChange);
            //this.gameObject.transform.DOJump(new Vector3(transform.position.x,transform.position.y,transform.position.z+_playerSpeed),JumpForce,1,1.0f);
            if (amountOfJumps <= 0)
            {
                CanJump = false;
            }
        }
        else
        {
            _jumpAwaker = ReturnJump();
            StopCoroutine(_jumpAwaker);
            StartCoroutine(_jumpAwaker);
        }
    }
    private IEnumerator ReturnJump()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.5f);
            //Debug.Log("Return jump is activated "+i+" times");
            if (!CanJump && this.gameObject.transform.position.y < 0.5f)
            {
                CanJump = true;
                amountOfJumps = GlobalVariables.instance.AmountOfJumps;
                break;
            }
        }
    }
    public void SlideDown()
    {
            _body.AddForce(Vector3.down * JumpForce, ForceMode.VelocityChange);
            //this.gameObject.transform.DOJump(new Vector3(transform.position.x,transform.position.y,transform.position.z+_playerSpeed),JumpForce,1,1.0f);
            if (amountOfJumps <= 0)
            {
                _jumpAwaker = ReturnJump();
                StopCoroutine(_jumpAwaker);
                StartCoroutine(_jumpAwaker);
            }
    }
}
