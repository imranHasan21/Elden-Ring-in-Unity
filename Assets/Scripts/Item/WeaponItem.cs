using UnityEngine;

public class WeaponItem : Item
{
    // ANIMATOR CONTROLLER OVERRIDE (change attack animation based on weapon you are currently using)

    [Header("Weapon Model")]
    public GameObject weaponModel;

    [Header("Weapon Requirements")]
    public int strengthREQ = 0;
    public int dexREQ = 0;
    public int intREQ = 0;
    public int faithREQ = 0;

    [Header("Weapon Base Damage")]
    public int physicalDamage = 0;
    public int magicDamage = 0;
    public int fireDamage = 0;
    public int holyDamage = 0;

    [Header("Weapon base poise damage")]
    public float poiseDamage = 10;
    // OFFENCIVE POISE BONUS WHEN ATTACKING

    // WEAPON MODIFIER
    // LIGHT ATTACK MODIFIER
    // HEAVY ATTACK MODIFIER
    // CRITICAL DAMAGE MODIFIER

    [Header("Stamina cost")]
    public int baseStaminaCost = 20;

    // RUNNING ATTACK STAMINA COST MODIFIER
    // LIGHT ATTACK STAMIAN COST MODIFIER
    // HEAVY ATTACK STAMINA COST MODIFIER

    // ITEM BASED ACTION(RB, RT, LB, LT)

    // ASH OF WAR

    //BLOCKING SOUND

}
