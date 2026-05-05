using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocomotionManager : CharacterLocomotionManager
{
    PlayerManager player;

    [HideInInspector] public float verticalMovement;
    [HideInInspector] public float horizontalMovement;
    [HideInInspector] public float moveAmount;

    [Header("MOVEMENT SETTINGS")]
    private Vector3 moveDirection;
    private Vector3 targetRotationDirection;
    [SerializeField] private float walkingSpeed = 2f;
    [SerializeField] private float runningSpeed = 4f;
    [SerializeField] private float sprintingSpeed = 5.5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private int sprintingStaminaCost = 2;

    [Header("DODGE")]
    private Vector3 rollDirection;
    [SerializeField] float dodgeStaminaCost = 20;

    [Header("JUMP")]
    [SerializeField] float jumpStaminaCost = 20;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }

    protected override void Update()
    {
        base.Update();

        if (player.IsOwner)
        {
            player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
            player.characterNetworkManager.verticalMovement.Value = verticalMovement;
            player.characterNetworkManager.moveAmount.Value = moveAmount;
        }
        else
        {
            horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
            verticalMovement = player.characterNetworkManager.verticalMovement.Value;
            moveAmount = player.characterNetworkManager.moveAmount.Value;

            // IF NOT LOCKED ON PASS MOVE AMOUNT
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);

            // IF LOCKED ON PASS HORIZONTAL AND VERTICAL VALUE
        }
    }

    public void HandleAllMovement()
    {
        // GROUND LEFT RIGHT MOVEMENT
        HandleGroundedMovement();

        // GROUND ROTATION
        HandleRotation();

        // AERIAL MOVEMENT
    }

    private void GetMovementValue()
    {
        verticalMovement = PlayerInputManager.Instance.verticalInput;
        horizontalMovement = PlayerInputManager.Instance.horizontalInput;
        moveAmount = PlayerInputManager.Instance.moveAmount;
    }

    private void HandleGroundedMovement()
    {
        if (!player.canMove)
        {
            return;
        }
        GetMovementValue();

        // OUR MOVEMENT DIRECTION IS BASED ON OUR CAMERAS FACING PERSPECTIVE AND OUR MOVEMENT INPUT
        moveDirection = PlayerCamera.Instance.transform.forward * verticalMovement;
        moveDirection = moveDirection + PlayerCamera.Instance.transform.right * horizontalMovement;
        moveDirection.Normalize();
        moveDirection.y = 0;
        if (player.playerNetworkManager.isSprinting.Value)
        {
            player.characterController.Move(moveDirection * sprintingSpeed * Time.deltaTime);
        }
        else
        {
            if (PlayerInputManager.Instance.moveAmount > 0.5f)
            {
                // MOVE AT RUNNING SPEED
                player.characterController.Move(moveDirection * runningSpeed * Time.deltaTime);
            }
            else if (PlayerInputManager.Instance.moveAmount <= 0.5f)
            {
                // MOVE AT WALKING SPEED
                player.characterController.Move(moveDirection * walkingSpeed * Time.deltaTime);
            }
        }

    }

    private void HandleRotation()
    {
        if (!player.canRotate)
        {
            return;
        }
        targetRotationDirection = Vector3.zero;
        targetRotationDirection = PlayerCamera.Instance.transform.forward * verticalMovement;
        targetRotationDirection = targetRotationDirection + PlayerCamera.Instance.transform.right * horizontalMovement;
        targetRotationDirection.Normalize();
        targetRotationDirection.y = 0;

        if (targetRotationDirection == Vector3.zero)
        {
            targetRotationDirection = transform.forward;
        }

        Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
        Quaternion targetRotaion = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
        transform.rotation = targetRotaion;
    }

    public void HandleSprinting()
    {
        if (player.isPerformingAction)
        {
            // SET SPRINTING TO FALSE
            player.playerNetworkManager.isSprinting.Value = false;
        }

        // IF WE ARE OUT OF STAMINA SET SPRINTING TO FALSE
        if (player.playerNetworkManager.currentStamina.Value <= 0)
        {
            player.playerNetworkManager.isSprinting.Value = false;
            return;
        }

        // IF WE ARE MOVING SET SPRINTING TO TRUE
        if (moveAmount >= 0.5)
        {
            player.playerNetworkManager.isSprinting.Value = true;
        }
        // IF WE ARE STATIONARY SET SPRINTING TO FALSE
        else
        {
            player.playerNetworkManager.isSprinting.Value = false;
        }

        if (player.playerNetworkManager.isSprinting.Value)
        {
            player.playerNetworkManager.currentStamina.Value -= sprintingStaminaCost * Time.deltaTime;
        }
    }

    public void AttemptToPerformDodge()
    {
        if (player.playerNetworkManager.currentStamina.Value <= 0)
        {
            return;
        }

        if (player.isPerformingAction)
        {
            return;
        }

        // IF WE ARE MOVING WHEN ATTEMTING TO DODGE WE PERFORM A ROLL
        if (moveAmount > 0)
        {
            // rollDirection = Vector3.zero;
            rollDirection = PlayerCamera.Instance.transform.forward * PlayerInputManager.Instance.verticalInput;
            rollDirection += PlayerCamera.Instance.transform.right * PlayerInputManager.Instance.horizontalInput;
            rollDirection.y = 0;
            rollDirection.Normalize();

            Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
            player.transform.rotation = playerRotation;

            // PERFORM A ROLL ANIMATION
            player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true);
        }
        // IF WE ARE STATIONARY WE PERFORM A BACKSTEP
        else
        {
            // PERFORM A BACKSTEP ANIMATION
            player.playerAnimatorManager.PlayTargetActionAnimation("Back_Step_01", true);
        }

        player.characterNetworkManager.currentStamina.Value -= dodgeStaminaCost;
    }

    public void AttemptToPerformJump()
    {
        // IF WE ARE PERFORMING A GENERAL ACTION WE DO NOT WANT TO ALLOW A JUMP (WILL CHANGE WHEN COMBAT IS READY)
        if (player.playerNetworkManager.currentStamina.Value <= 0)
        {
            return;
        }

        // IF WE ARE OUT OF STAMINA WE DO NOT WISH T0 ALLOW A JUMP
        if (player.isPerformingAction)
        {
            return;
        }

        // IF WE ARE ALREADY JUMPING WE DO NOT WISH T0 ALLOW A JUMP
        if (player.isJumping)
        {
            return;
        }

        // IF WE ARE NOT GROUNDED WE DO NOT WANT TO ALLOW A JUMP
        if (player.isGrounded)
        {
            return;
        }

        // IF WE ARE TWO HANDING OUR WEAPON PLAY THE TWO HANDED JUMP ANIMATION, OTHERWISE PLAY THE ONE HANDED ANIMATION
        player.playerAnimatorManager.PlayTargetActionAnimation("Main_Jump_01", false);

        player.isJumping = true;

        player.characterNetworkManager.currentStamina.Value -= jumpStaminaCost;
    }

    public void ApplyJumpingVelocity()
    {

    }
}
