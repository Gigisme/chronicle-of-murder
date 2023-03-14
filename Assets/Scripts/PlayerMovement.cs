using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _controller;
    private bool isGrounded;
    private Vector3 playerVelocity;
    private RaycastHit hitForward;
    private RaycastHit hitBackward;
    private RaycastHit hitRight;
    private RaycastHit hitLeft;
    private Vector3 wallNormal;
    private bool detectedForward, detectedBackward, detectedRight, detectedLeft;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float jumpHeight = 3f;
    [SerializeField] private float fallingMultiplier = 2.5f;
    [SerializeField] private float wallJumpHeight = 3f;
    [SerializeField] private float wallJumpDistance = 3f;

    public void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void Update()
    {
        isGrounded = _controller.isGrounded;
        CardinalRaycast();
    }
    
    // TODO - slide
    //receive inputs for InputManager.cs and apply them to player
    public void ProcessMove(Vector2 input)
    {
        // Moving around
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        
        _controller.Move( speed * Time.deltaTime * transform.TransformDirection(moveDirection));
        //Gravity
        
        if (playerVelocity.y < 0)
        {
            playerVelocity.y += fallingMultiplier * gravity * Time.deltaTime;
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime;
        }
        //Jump
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        
        _controller.Move(playerVelocity * Time.deltaTime);
    }

    // TODO - cooldown on wall jump
    // TODO - wallrunning
    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = 0;
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -gravity);
        }

        if (TouchingWall())
        {
            playerVelocity.y = 0;
            playerVelocity.y = Mathf.Sqrt(wallJumpHeight * -gravity);
            // TODO - get a small shove away from the wall, this doesnt seem to work as intended
            // playerVelocity += wallNormal * wallJumpDistance * Time.deltaTime;
        }
    }

    private void CardinalRaycast()
    {
        Vector3 position = transform.position;
        float castDistance = 1f;
        detectedForward = Physics.Raycast(new Ray(position, transform.forward), out hitForward, castDistance);
        detectedBackward = Physics.Raycast(new Ray(position, -transform.forward), out hitBackward, castDistance);
        detectedRight = Physics.Raycast(new Ray(position, transform.right), out hitRight, castDistance);
        detectedLeft = Physics.Raycast(new Ray(position, -transform.right), out hitLeft, castDistance);
    }
    
    private bool TouchingWall()
    {
        if (detectedForward && hitForward.collider.CompareTag("Wall"))
        {
            wallNormal = hitForward.normal;
            return true;
        } else if (detectedBackward && hitBackward.collider.CompareTag("Wall"))
        {
            wallNormal = hitBackward.normal;
            return true;
        } else if (detectedLeft && hitLeft.collider.CompareTag("Wall"))
        {
            wallNormal = hitLeft.normal;
            return true;
        } else if (detectedRight && hitRight.collider.CompareTag("Wall"))
        {
            wallNormal = hitRight.normal;
            return true;
        }

        return false;
    }

}
