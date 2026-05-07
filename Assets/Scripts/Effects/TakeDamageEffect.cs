using UnityEngine;

[CreateAssetMenu(menuName = "Character Effects/Instant Effects /Take Damage")]
public class TakeDamageEffect : InstantCharacterEffect
{
    [Header("Character Causing damage")]
    public CharacterManager characterCausingDamage;     // IF THE DAMAGE IS CAUSED BY ANOTHR CHARACTERS ATTACK IT WILL BE TORED HERE

    [Header("Damage")]
    public float physicalDamage = 0;                    // IN THE FUTURE IT WILL BE SPLIT INTO "STANDARD", "STRIKE", "SLASH" AND "PIERCE"
    public float magicDamage = 0;
    public float fireDamage = 0;
    public float holyDamage = 0;

    [Header("Final damage dealth")]
    private int finalDamageDealth = 0;                // THE DAMAGE CHARACTER TAKES AFTER ALL CALCULATIONS ARE MADE

    [Header("Poise")]
    public float poiseDamage = 0;
    public bool isPoiseBroken = false;                  // IF A CHARACTER POISE IS BROKEN AND WILL BE STUNNED AND PLAY A DAMAGE ANIMATION 

    // TODO: BUILD UPS
    // BUILD UP EFFECTS AMOUNT

    [Header("Animations")]
    public bool playDamageAnimation = true;
    public bool manuallySelectDamageAnimation = false;
    public string damageAnimation;

    [Header("Sound FX")]
    public bool willPlayDamageSFX = true;
    public AudioClip elementalDamageSFX;                // USED ON TOP OF REGULAR SFX IF THERE IS ELEMENTAL DAMAGE PRESENT (MAGIC/HOLY/FIRE ETC)

    [Header("Direction Damage Taken From")]
    public float angleHitFrom;                          // USED TO DETERMINE WHAT DAMAGE ANIMATION TO PLAY (MOVE BACKWARD, TO THE LEFT ETC)
    public Vector3 contactPoint;                        // USED TO DETERMINE WHERE THE BLOOD FX TO INSTANTIATE
    public override void ProcessEffect(CharacterManager character)
    {
        base.ProcessEffect(character);

        // IF THE CHARACTER IS DEAD NO ADDITIONAL DAMAGE EFFECT SHOULD BE PROCESSED
        if (character.isDead.Value)
        {
            return;
        }

        // CHECK FOR INVULNERABILITY

        // CALCULATE DAMAGE
        CalculateDamage(character);

        // CHECK WHICH DIRECTION DAMAGE CAME FROM
        // PLAY A DAMAGE ANIMATION
        // CHECK FOR BUILD UPS (POISON,BLEED ETC)
        // PLAY DAMAGE SOUND FX
        // PLAY DAMAGE VFX (BLOOD)

        // IF CHARACTER IS A.I CHECK FOR NEW TARGET IF CHARACTER CAUSING DAMAGE IS PRESENT
    }

    private void CalculateDamage(CharacterManager character)
    {
        if (!character.IsOwner)
        {
            return;
        }

        if (characterCausingDamage != null)
        {
            // CHECK FOR DAMAGE MODIFIERS AND MODIFY BASE DAMAGE 

        }

        // CHECK CHARACTER FOR FLAT DEFENCES AND SUBSTRACT THEM FROM THE DAMAGE

        // CHECK CHARACTER FOR ARMOR ABSORPTION AND SUBSTRACT THE PERCENTAGE FROM THE DAMAGE

        // ADD ALL DAMAGE TYPES TOGETHER AND APPLY FINAL DAMAGE
        finalDamageDealth = Mathf.RoundToInt(physicalDamage + magicDamage + fireDamage + holyDamage);

        if (finalDamageDealth <= 0)
        {
            finalDamageDealth = 1;
        }

        character.characterNetworkManager.currentHealth.Value -= finalDamageDealth;

        // CALCULATE POISE DAMAGE TO DETERMINE IF THE CHARACTER WILL BE STUNNED
    }
}
