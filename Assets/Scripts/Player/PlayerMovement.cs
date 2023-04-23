using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float fallingMultiplier = 2.5f;
    private CharacterController _controller;
    private bool _isGrounded;
    private Vector3 _playerVelocity;
    private Vector3 _playerScale;
    
    [Header("Wall Jump Settings")]
    [SerializeField] private float wallJumpHeight = 3f;
    private RaycastHit _hitForward, _hitBackward, _hitRight, _hitLeft;
    private Vector3 _wallNormal;
    private bool _detectedForward, _detectedBackward, _detectedRight, _detectedLeft;
    
    [Header("Slide Settings")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;
    [SerializeField] private float slideYScale;
    private float startYScale;
    private bool sliding = false;
    private float maxSlideTime;
    private float startSpeed;
    private Vector3 slideDirection;
    private Vector3 moveDirection;

    public void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _playerScale = transform.localScale;
        startYScale = _playerScale.y;
        maxSlideTime = slideTime;
        startSpeed = speed;
        slideDirection = Vector3.zero;
    }

    public void Update()
    {
        _isGrounded = _controller.isGrounded;
        CardinalRaycast();
    }
    
    //receive inputs for InputManager.cs and apply them to player
    public void ProcessMove(Vector2 input)
    {
        // Moving direction
        moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        
        //Gravity
        
        if (_playerVelocity.y < 0)
        {
            _playerVelocity.y += fallingMultiplier * gravity * Time.deltaTime;
        }
        else
        {
            _playerVelocity.y += gravity * Time.deltaTime;
        }

        //Slide
        if (sliding)
        {
            SlidingMovement();
        }
        else
        {
            transform.localScale = new Vector3(_playerScale.x, startYScale, _playerScale.z);
            //Movement
            _controller.Move( speed * Time.deltaTime * transform.TransformDirection(moveDirection));
            
            //Jump
            if (_isGrounded && _playerVelocity.y < 0)
                _playerVelocity.y = -2f;
        }
        
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
    
    public void StartSliding()
    {
       sliding = true;
       slideTime = maxSlideTime;
       speed = slideSpeed;
       slideDirection.x = moveDirection.x;
       slideDirection.z = moveDirection.z;
    }
    
    private void SlidingMovement()
    {
        if (slideTime == maxSlideTime)
        {
            transform.localScale = new Vector3(_playerScale.x, slideYScale, _playerScale.z);
        }
        _controller.Move( speed * Time.deltaTime * transform.TransformDirection(slideDirection));
        slideTime -= Time.deltaTime;
        if (slideTime <= 0)
        {
            StopSliding();
        }
    }
    
    public void StopSliding()
    {
        sliding = false;
        speed = startSpeed;
    }
    
    

}
