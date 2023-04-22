using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private bool _isGrounded;
    private Vector3 _playerVelocity;
    private RaycastHit _hitForward, _hitBackward, _hitRight, _hitLeft;
    private Vector3 _wallNormal;
    private bool _detectedForward, _detectedBackward, _detectedRight, _detectedLeft;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float fallingMultiplier = 2.5f;
    [SerializeField] private float wallJumpHeight = 3f;

    public void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        _isGrounded = _controller.isGrounded;
        CardinalRaycast();
    }
    
    //receive inputs for InputManager.cs and apply them to player
    public void ProcessMove(Vector2 input)
    {
        // Moving around
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        
        _controller.Move( speed * Time.deltaTime * transform.TransformDirection(moveDirection));
        //Gravity
        
        if (_playerVelocity.y < 0)
        {
            _playerVelocity.y += fallingMultiplier * gravity * Time.deltaTime;
        }
        else
        {
            _playerVelocity.y += gravity * Time.deltaTime;
        }
        //Jump
        if (_isGrounded && _playerVelocity.y < 0)
            _playerVelocity.y = -2f;
        
        _controller.Move(_playerVelocity * Time.deltaTime);
    }
    
    public void Jump()
    {
        if (_isGrounded)
        {
            _playerVelocity.y = 0;
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -gravity);
        }

        if (TouchingWall())
        {
            _playerVelocity.y = 0;
            _playerVelocity.y = Mathf.Sqrt(wallJumpHeight * -gravity);
        }
    }


    private void CardinalRaycast()
    {
        Vector3 position = transform.position;
        float castDistance = 1f;
        _detectedForward = Physics.Raycast(new Ray(position, transform.forward), out _hitForward, castDistance);
        _detectedBackward = Physics.Raycast(new Ray(position, -transform.forward), out _hitBackward, castDistance);
        _detectedRight = Physics.Raycast(new Ray(position, transform.right), out _hitRight, castDistance);
        _detectedLeft = Physics.Raycast(new Ray(position, -transform.right), out _hitLeft, castDistance);
    }
    
    private bool TouchingWall()
    {
        if (_detectedForward && _hitForward.collider.CompareTag("Wall"))
        {
            _wallNormal = _hitForward.normal;
            return true;
        } else if (_detectedBackward && _hitBackward.collider.CompareTag("Wall"))
        {
            _wallNormal = _hitBackward.normal;
            return true;
        } else if (_detectedLeft && _hitLeft.collider.CompareTag("Wall"))
        {
            _wallNormal = _hitLeft.normal;
            return true;
        } else if (_detectedRight && _hitRight.collider.CompareTag("Wall"))
        {
            _wallNormal = _hitRight.normal;
            return true;
        }

        return false;
    }

}
