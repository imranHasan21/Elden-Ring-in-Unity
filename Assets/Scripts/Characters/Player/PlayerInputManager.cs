using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager Instance;
    public PlayerManager player;

    PlayerControls playerControls;

    [Header("PLAYER MOVEMENT INPUT")]
    [SerializeField] private Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("CAMERA MOVEMENT INPUT")]
    [SerializeField] private Vector2 cameraMovementInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("PLAYER ACTION INPUT")]
    [SerializeField] private bool dodgeInput = false;
    [SerializeField] private bool sprintInput = false;
    [SerializeField] private bool jumpInput = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        // WHEN SCENE CHANGES RUN THIS LOGIC
        SceneManager.activeSceneChanged += OnSceneChange;
        Instance.enabled = false;
    }

    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        // IF WE ARE LOADING INTO OUR WORLD SCENE, ENABLE OUR PLAYER CONTROLS
        if (newScene.buildIndex == WorldSavedGameManager.Instance.GetWorldSceneIndex())
        {
            Instance.enabled = true;

            // ON SCENE CHANGE APPLY ROOT MOTION, SO PLAYER DONT FALL OFF
            // player.animator.applyRootMotion = true;
        }
        // OTHEREWISE WE ARE IN THE MAIN MENU AND DISABLE PLAYER CONTROLS
        else
        {
            Instance.enabled = false;
        }
    }

    private void OnEnable()
    {
        if (playerControls == null)
        {
            playerControls = new PlayerControls();

            // MOVEMENT INPUT 
            playerControls.Player.Move.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.Player.Move.canceled += i => movementInput = Vector2.zero;

            // CAMERA INPUT
            playerControls.Player.Look.performed += i => cameraMovementInput = i.ReadValue<Vector2>();
            playerControls.Player.Look.canceled += i => cameraMovementInput = Vector2.zero;

            // DODGE INPUT
            playerControls.Player.Dodge.performed += i => dodgeInput = true;

            // SPRINT INPUT
            playerControls.Player.Sprint.performed += i => sprintInput = true;
            playerControls.Player.Sprint.canceled += i => sprintInput = false;

            playerControls.Player.Jump.performed += i => jumpInput = true;
        }
        playerControls.Enable();
    }

    private void OnDestroy()
    {
        // IF WE DESTROY THIS OBJECT UNSUBSCRIBE FROM THIS EVENT
        SceneManager.activeSceneChanged -= OnSceneChange;
    }

    // IF WE MINIMIZE OR LOWER THE WINDOW
    private void OnApplicationFocus(bool focus)
    {
        if (enabled)
        {
            if (focus)
            {
                playerControls.Enable();
            }
            else
            {
                playerControls.Disable();
            }
        }
    }

    private void Update()
    {
        HandleAllInputs();
    }

    private void HandleAllInputs()
    {

        HandlePlayerMovementInput();
        HandleCameraMovementInput();
        HandleDodgeInput();
        HandleSprintInput();
        HandleJumpInput();
    }

    // MOVEMENT

    public void HandlePlayerMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        // RETURNS THE ABSOLUTE NUMBER
        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        // WE CLAMP THE VALUES, SO ITS EITHER 0, 0.5 OR 1
        if (moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount > 0.5 && moveAmount <= 1)
        {
            moveAmount = 1;
        }
        // WHY DO WE PASS 0 ON THE HORIZONTAL? BECAUSE WE ONLY WANT NON-STRAFING MOVEMENT
        //WE USE THE HORIZONTAL WHEN WE ARE STRAFING OR LOCKED ON

        if (player == null)
        {
            return;
        }

        // IF WE ARE NOT LOCKED ON ONLY USE THE MOVE AMOUNT
        player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetworkManager.isSprinting.Value);

        // IF WE ARE LOCKED ON PASS THE HORIZONTAL MOVEMENT AS WELL       
    }

    public void HandleCameraMovementInput()
    {
        cameraVerticalInput = cameraMovementInput.y;
        cameraHorizontalInput = cameraMovementInput.x;
    }

    // ACTION

    public void HandleDodgeInput()
    {
        if (dodgeInput)
        {
            dodgeInput = false;

            // FUTURE NOTE: RETURN (DO NOTHING) IF MENU OR UI WINDOW IS OPEN
            // PERFORM A DODGE
            player.playerLocomotionManager.AttemptToPerformDodge();

        }
    }

    public void HandleSprintInput()
    {
        if (sprintInput)
        {
            // HANDLE SPRINTING
            player.playerLocomotionManager.HandleSprinting();
        }
        else
        {
            player.playerNetworkManager.isSprinting.Value = false;
        }
    }

    public void HandleJumpInput()
    {
        if (jumpInput)
        {
            jumpInput = false;

            // FUTURE NOTE: RETURN (DO NOTHING) IF MENU OR UI WINDOW IS OPEN
            // PERFORM A JUMP
            player.playerLocomotionManager.AttemptToPerformJump();
        }
    }
}
