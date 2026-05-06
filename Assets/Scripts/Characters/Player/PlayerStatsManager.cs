using UnityEngine;

public class PlayerStatsManager : CharacterStatsManager
{
    PlayerManager player;

    protected override void Awake()
    {
        base.Awake();
        player = GetComponent<PlayerManager>();
    }

    protected override void Start()
    {
        base.Start();

        // WHY CALCULATE THIS HERE?
        // WHEN WE CREATE A CHARACTER CREATION MENU, AND SET THE STATS DEPENDING ON THE CLASS THIS WILL BE CALCULATED THERE
        // UNTIL THEN HOWEVER STATS ARE NEVER CALCULATED SO WE IT HERE ON START IF A SAVE FILE EXISTS THEY WILL BE OVER WRITTEN WHEN LODING INTO SCENE
        CalculateHealthBasedOnVitalityLevel(player.playerNetworkManager.vitality.Value);
        CalculateStaminaBasedOnEndurenceLevel(player.playerNetworkManager.endurance.Value);
    }
}
