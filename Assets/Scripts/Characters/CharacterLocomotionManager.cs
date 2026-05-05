using System;
using UnityEngine;
using UnityEngine.XR;

public class CharacterLocomotionManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Ground Check & Jumping")]
    [SerializeField] private float gravityForce = 5.55f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckSphereRadius = 1;
    [SerializeField] protected Vector3 yVelocity; // THE FORCE AT WHICH OUR CHARACTER IS PULLED UP OR DOWN (JUMPING OR FALLING)
    [SerializeField] protected float groundedYVelocity = -20f; // THE FORCE AT WHICH OUR CHARACTER IS STICKING TO THE GROUNDED
    [SerializeField] protected float fallStartYVelocity = -5f;
    protected bool fallingVelocityHasBeenSet = false;
    protected float inAirTimer = 0;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    protected virtual void Update()
    {
        HandleGroundedCheck();

        if (character.isGrounded)
        {
            if (yVelocity.y < 0)
            {
                inAirTimer = 0;
                fallingVelocityHasBeenSet = false;
                yVelocity.y = groundedYVelocity;
            }
        }
        else
        {
            if (!character.isJumping && !fallingVelocityHasBeenSet)
            {
                fallingVelocityHasBeenSet = true;
                yVelocity.y = fallStartYVelocity;
            }

            inAirTimer += Time.deltaTime;
            character.animator.SetFloat("InAirTimer", inAirTimer);

            yVelocity.y += gravityForce * Time.deltaTime;
        }
        character.characterController.Move(yVelocity * Time.deltaTime);
    }

    protected void HandleGroundedCheck()
    {
        character.isGrounded = Physics.CheckSphere(character.transform.position, groundCheckSphereRadius, groundLayer);
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(character.transform.position, groundCheckSphereRadius);
    }
}
