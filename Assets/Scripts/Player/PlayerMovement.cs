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
    private Renderer renderer;
    
    [Header("Wall Jump Settings")]
    [SerializeField] private float wallJumpHeight = 3f;
    [SerializeField] private int wallJumpCount = 2;
    private int maxWallJumpCount;
    private bool _isTouchingWall;
    //private Vector3 _wallNormal;

    [Header("Slide Settings")]
    [SerializeField] private float slideSpeed;
    [SerializeField] private float slideTime;
    [SerializeField] private float slideYScale;
    private float startYScale;
    private bool sliding;
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
        maxWallJumpCount = wallJumpCount;
        renderer = GetComponent<Renderer>();
    }

    public void Update()
    {
        _isGrounded = _controller.isGrounded;
        
        //Stop jump early
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.up, out hit, renderer.bounds.size.y / 2 + 0.1f))
        {
            if (_playerVelocity.y > 0)
            {
                _playerVelocity.y = 0;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            _isTouchingWall = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Wall"))
        {
            _isTouchingWall = false;
        }
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
            wallJumpCount = maxWallJumpCount;
            _playerVelocity.y = 0;
            _playerVelocity.y = Mathf.Sqrt(jumpHeight * -gravity);
        }
        else if (_isTouchingWall && wallJumpCount > 0)
        {
            wallJumpCount--;
            _playerVelocity.y = 0;
            _playerVelocity.y = Mathf.Sqrt(wallJumpHeight * -gravity);
        }
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
