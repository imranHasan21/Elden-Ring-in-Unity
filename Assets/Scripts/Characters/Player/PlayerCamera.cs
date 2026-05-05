using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera Instance;

    public PlayerManager player;
    public Camera cameraObject;
    [SerializeField] Transform cameraPivotTransform;

    [Header("Camera Setting")]
    private float cameraSmoothSpeed = 1f; // THE BIGGER THE NUMBER, THE LONGER FOR THE CAMERA TO REACH ITS POSITION DURING MOVEMENT
    [SerializeField] private float leftAndRightRotationSpeed = 220f;
    [SerializeField] private float upAndDownRotationSpeed = 220f;
    [SerializeField] private float minimumPivot = -30; // THE LOWEST POINT YOU ARE ABLE TO LOOK DOWN
    [SerializeField] private float maximumPivot = 60; // THE HIGHEST POINT YOU ARE ABLE TO LOOK UP
    [SerializeField] private float cameraCollisonRadius = 0.2f;
    [SerializeField] private LayerMask collideWithLayers;

    [Header("Camera Values")]
    private Vector3 cameraVelocity;
    private Vector3 cameraObjectPosition;
    [SerializeField] private float leftAndRightLookAngle;
    [SerializeField] private float upAndDownLookAngle;
    private float cameraZPosition; // VALUE USED FOR CAMERA COLLISION
    private float targetCameraZPosition; // VALUE USED FOR CAMERA COLLISION

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
        cameraZPosition = cameraObject.transform.position.z;
    }

    public void HandleAllCameraAction()
    {
        if (player != null)
        {
            // FOLLOW THE PLAYER
            HandleFollowPlayer();

            // ROTATE AROUND THE PLAYER
            HandleRotation();

            // COLLIDE WITH ENVIRONMENT
            HandleCollisions();
        }
    }

    private void HandleFollowPlayer()
    {
        Vector3 targetCameraPosition = Vector3.SmoothDamp(transform.position, player.transform.position, ref cameraVelocity, cameraSmoothSpeed * Time.deltaTime);
        transform.position = targetCameraPosition;
    }

    private void HandleRotation()
    {
        // IF LOCKED ON FORCE ROTATION TOWRD TARGET
        // ELSE ROTATE REGULARLY

        // NORMAL ROTATION
        leftAndRightLookAngle += PlayerInputManager.Instance.cameraHorizontalInput * leftAndRightRotationSpeed * Time.deltaTime;
        upAndDownLookAngle += PlayerInputManager.Instance.cameraVerticalInput * upAndDownRotationSpeed * Time.deltaTime;
        upAndDownLookAngle = Mathf.Clamp(upAndDownLookAngle, minimumPivot, maximumPivot);

        Vector3 cameraRotation = Vector3.zero;
        Quaternion targetRotation;

        // ROTATE THIS GAMEOBJECT LEFT AND RIGHT
        cameraRotation.y = leftAndRightLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        transform.rotation = targetRotation;

        // ROTATE THE PIVOT GAMEOBJECT UP AND DOWN
        cameraRotation = Vector3.zero;
        cameraRotation.x = upAndDownLookAngle;
        targetRotation = Quaternion.Euler(cameraRotation);
        cameraPivotTransform.localRotation = targetRotation;
    }

    private void HandleCollisions()
    {
        //targetCameraZPosition = cameraZPosition;
        targetCameraZPosition = -Mathf.Abs(cameraZPosition);

        RaycastHit hit;
        Vector3 direction = cameraObject.transform.position - cameraPivotTransform.position;
        direction.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, cameraCollisonRadius, direction, out hit, Mathf.Abs(targetCameraZPosition), collideWithLayers))
        {
            float distanceFromHitObject = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetCameraZPosition = -(distanceFromHitObject - cameraCollisonRadius);
        }

        if (Mathf.Abs(targetCameraZPosition) < cameraCollisonRadius)
        {
            targetCameraZPosition = -cameraCollisonRadius;
        }

        cameraObjectPosition = cameraObject.transform.localPosition;
        cameraObjectPosition.z = Mathf.Lerp(cameraObjectPosition.z, targetCameraZPosition, Time.deltaTime * 10f);
        cameraObject.transform.localPosition = cameraObjectPosition;
    }
}
