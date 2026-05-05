using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerNetworkManager playerNetworkManager;
    [HideInInspector] public PlayerStatsManager playerStatsManager;

    private bool isInitialized;

    protected override void Awake()
    {
        base.Awake();

        // DO MORE THINGS ONLY FOE THE PLAYER
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerNetworkManager = GetComponent<PlayerNetworkManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }



    protected override void Update()
    {
        base.Update();

        // IF WE ARE NOT THE OWNER OF THE OBJECT, WE DONT CONTROL IT
        if (!IsOwner)
        {
            return;
        }

        // HANDLE ALL OF OUR PLAYER MOVEMENT
        playerLocomotionManager.HandleAllMovement();

        // REGEN STAMINA
        playerStatsManager.RegenerateStamina();
    }

    protected override void LateUpdate()
    {
        if (!IsOwner)
        {
            return;
        }

        base.LateUpdate();

        PlayerCamera.Instance.HandleAllCameraAction();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // DISABLE MOVEMENT UNTIL LEVEL IS READY


        // IF THIS IS THE PLAYER OBJECT OWNED BY THIS CLIENT
        if (IsOwner)
        {
            PlayerCamera.Instance.player = this;
            PlayerInputManager.Instance.player = this;
            WorldSavedGameManager.Instance.player = this;

            // LoadGameDataFromCurrentCharacterData(ref WorldSavedGameManager.Instance.currentCharacterData);

            playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.Instance.playerUIHudManager.SetNewStaminaValue;
            playerNetworkManager.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenerationTimer;
            //THIS WILL BE MOVED WHEN SAVING AND LODING IS ADDED
            playerNetworkManager.maxStamina.Value = playerStatsManager.CalculateStaminaBasedOnEndurenceLevel(playerNetworkManager.endurance.Value);
            PlayerUIManager.Instance.playerUIHudManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);
            playerNetworkManager.currentStamina.Value = playerStatsManager.CalculateStaminaBasedOnEndurenceLevel(playerNetworkManager.endurance.Value);
        }
    }

    public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        currentCharacterData.sceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
        currentCharacterData.xPosition = transform.position.x;
        currentCharacterData.yPosition = transform.position.y;
        currentCharacterData.zPosition = transform.position.z;
    }

    public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        playerNetworkManager.characterName.Value = currentCharacterData.characterName;
        Vector3 myPosition = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
        transform.position = myPosition;
        Physics.SyncTransforms();
    }
}
