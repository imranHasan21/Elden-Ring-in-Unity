using Unity.Collections;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerNetworkManager : CharacterNetworkManager
{
    PlayerManager player;
    public NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("Character", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [Header("Equipment")]
    public NetworkVariable<int> currentRightHandWeaponID = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> currentLeftHandWeaponID = new(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }

    public void SetNewMaxHealthValue(int oldVitality, int newVitality)
    {
        maxHealth.Value = player.playerStatsManager.CalculateHealthBasedOnVitalityLevel(newVitality);
        // WHEN LEVEL UP UPDATE THE UI ACCORDING TO LEVEL OF STATS
        PlayerUIManager.Instance.playerUIHudManager.SetMaxHealthValue(maxHealth.Value);
        currentHealth.Value = maxHealth.Value;
    }

    public void SetNewMaxStaminaValue(int oldEndurance, int newEndurance)
    {
        maxStamina.Value = player.playerStatsManager.CalculateHealthBasedOnVitalityLevel(newEndurance);
        // WHEN LEVEL UP UPDATE THE UI ACCORDING TO LEVEL OF STATS
        PlayerUIManager.Instance.playerUIHudManager.SetMaxStaminaValue(maxStamina.Value);
        currentStamina.Value = maxStamina.Value;
    }

    public void OnCurrentRightHandWeaponIDChange(int oldID, int newID)
    {
        WeaponItem newWeapon = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(newID));
        player.playerInventoryManager.currentRightHandWeapon = newWeapon;
        player.playerEquipmentManager.LoadRightWeapon();
    }

    public void OnCurrentLeftHandWeaponIDChange(int oldID, int newID)
    {
        WeaponItem newWeapon = Instantiate(WorldItemDatabase.Instance.GetWeaponByID(newID));
        player.playerInventoryManager.currentLeftHandWeapon = newWeapon;
        player.playerEquipmentManager.LoadLeftWeapon();
    }
}
