using UnityEngine;

public class CharacterStatsManager : MonoBehaviour
{
    CharacterManager character;

    [Header("Stamina  Regeneration Settings")]
    private float staminaRegenerationTimer = 0;
    private float staminaTickTimer = 0;
    [SerializeField] float staminaRegenerationDelay = 1;
    [SerializeField] float staminaRegenerationAmount = 0.5f;

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }

    public int CalculateStaminaBasedOnEndurenceLevel(int endurance)
    {
        float stamina;

        // CREATE AN EQUATION FOR HOW YOU WANT YOUR STAMINA TO BE CALCULATED
        stamina = endurance * 10;

        return Mathf.RoundToInt(stamina);
    }

    public virtual void RegenerateStamina()
    {
        // ONLY OWNER CAN EDIT THEIR NETWORK VARIABLE
        if (!character.IsOwner)
        {
            return;
        }

        // WE DO NOT WANT TO REGENERATE STAMINA IF WE ARE SPRINTING
        if (character.characterNetworkManager.isSprinting.Value)
        {
            return;
        }

        if (character.isPerformingAction)
        {
            return;
        }

        staminaRegenerationTimer += Time.deltaTime;

        if (staminaRegenerationTimer >= staminaRegenerationDelay)
        {
            if (character.characterNetworkManager.currentStamina.Value < character.characterNetworkManager.maxStamina.Value)
            {
                staminaTickTimer = staminaTickTimer + Time.deltaTime;

                if (staminaTickTimer >= 0.1)
                {
                    staminaTickTimer = 0;
                    character.characterNetworkManager.currentStamina.Value += staminaRegenerationAmount;
                }
            }
        }
    }

    public virtual void ResetStaminaRegenerationTimer(float previousStaminaValue, float currentStaminaValue)
    {
        // WE ONLY WANT TO RESET THE REGENERATION IF THE ACTION USED STAMINA
        // WE DONT WANT TO RESET THE REGENERATION IF WE ARE ALREADY REGENERATING STAMINA
        if (currentStaminaValue < previousStaminaValue)
        {
            staminaRegenerationTimer = 0;
        }
    }

}

