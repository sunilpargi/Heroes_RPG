﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementMotor : MonoBehaviour
{
    public float gravityMultiplier = 1f;
    public float lerpTime = 10f;

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 targetDirection = Vector3.zero;
    private float fallVelocity = 0f;

    [HideInInspector]
    public CharacterController charController;

    public float distanceToGround = 0.1f;

    private bool isGrounded;

    private Collider myCollider;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        myCollider = GetComponent<Collider>();
    }
    void Start()
    {
        distanceToGround = myCollider.bounds.extents.y;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = OnGroundCheck();

        moveDirection = Vector3.Lerp(moveDirection, targetDirection, Time.deltaTime * lerpTime);
        moveDirection.y = fallVelocity;

        charController.Move(moveDirection * Time.deltaTime);

        if(!isGrounded)
        {
            fallVelocity -= 90f * gravityMultiplier * Time.deltaTime;
        }
    }

    public bool OnGroundCheck()
    {
        RaycastHit hit;

        if(charController.isGrounded)
        {
            return true;
        }

        if(Physics.Raycast(myCollider.bounds.center,-Vector3.up,out hit,distanceToGround + 0.1f))
        {
            return true;
        }

        return false;
    }

    public void Move(Vector3 dir)
    {
        targetDirection = dir;
    }

    public void Stop()
    {
        moveDirection = Vector3.zero;
        targetDirection = Vector3.zero;
    }

    public void Jump(float JumpSpeed)
    {
        if(isGrounded)
        {
            fallVelocity = JumpSpeed;
        }
    }
}
