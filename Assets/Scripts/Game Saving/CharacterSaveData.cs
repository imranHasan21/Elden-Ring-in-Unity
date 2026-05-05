using UnityEngine;

[System.Serializable]
//WE WANT TO REFERENCE THIS DATA FOR EVERY SAVE FILE , THIS SCRIPT IS NOT A MONOBEHAVIOUR AND IS INSTEAD SERIALIZABLE
public class CharacterSaveData
{
    [Header("SCENE INDEX")]
    public int sceneIndex;

    [Header("Character Name")]
    public string characterName = "Character";

    [Header("Time Played")]
    public float secondsPlayed;

    [Header("World Coordinates")]
    public float xPosition;
    public float yPosition;
    public float zPosition;
}
