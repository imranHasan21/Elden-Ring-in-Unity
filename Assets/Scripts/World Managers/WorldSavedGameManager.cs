using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSavedGameManager : MonoBehaviour
{
    public static WorldSavedGameManager Instance;

    [SerializeField] public PlayerManager player;

    [Header("SAVE/LOAD")]
    [SerializeField] private bool isSaveGame;
    [SerializeField] private bool isLoadGame;

    [Header("World Scene Index")]
    [SerializeField] private int worldSceneIndex = 1;

    [Header("Save Data Writer")]
    private SaveFileDataWriter saveFileDataWriter;

    [Header("Current Character Data")]
    public CharacterSlot currentCharacterSlotBeingUsed;
    public CharacterSaveData currentCharacterData;
    private string saveFileName;

    [Header("Character Slots")]
    public CharacterSaveData characterSlot01;
    public CharacterSaveData characterSlot02;
    public CharacterSaveData characterSlot03;
    public CharacterSaveData characterSlot04;
    public CharacterSaveData characterSlot05;
    public CharacterSaveData characterSlot06;
    public CharacterSaveData characterSlot07;
    public CharacterSaveData characterSlot08;
    public CharacterSaveData characterSlot09;
    public CharacterSaveData characterSlot10;

    private void Awake()
    {
        // THERE CAN ONLY BE ONE INSTANCE OF THIS SCRIPT AT ONE TIME, IF ANOTHER EXIST DESTROY IT
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
        LoadAllCharacterSlots();
    }

    private void Update()
    {
        if (isSaveGame)
        {
            isSaveGame = false;
            SaveGame();
        }

        if (isLoadGame)
        {
            isLoadGame = false;
            LoadGame();
        }
    }

    public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot characterSlot)
    {
        string fileName = "";
        switch (characterSlot)
        {
            case CharacterSlot.CharacterSlot_01:
                fileName = "CharacterSlot_01";
                break;
            case CharacterSlot.CharacterSlot_02:
                fileName = "CharacterSlot_02";
                break;
            case CharacterSlot.CharacterSlot_03:
                fileName = "CharacterSlot_03";
                break;
            case CharacterSlot.CharacterSlot_04:
                fileName = "CharacterSlot_04";
                break;
            case CharacterSlot.CharacterSlot_05:
                fileName = "CharacterSlot_05";
                break;
            case CharacterSlot.CharacterSlot_06:
                fileName = "CharacterSlot_06";
                break;
            case CharacterSlot.CharacterSlot_07:
                fileName = "CharacterSlot_07";
                break;
            case CharacterSlot.CharacterSlot_08:
                fileName = "CharacterSlot_08";
                break;
            case CharacterSlot.CharacterSlot_09:
                fileName = "CharacterSlot_09";
                break;
            case CharacterSlot.CharacterSlot_10:
                fileName = "CharacterSlot_10";
                break;
        }
        return fileName;
    }

    public void AttemptToCreateNewGame()
    {
        saveFileDataWriter = new SaveFileDataWriter();

        // CHECK TO SEE IF WE CAN CREATE A NEW SAVE FILE (CHECK FOR OTHER EXISTING FILE FIRST)
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_01;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }

        // CREATE A FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_02;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }

        // CREATE A FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_03;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }

        // CREATE A FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_04;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }


        // CREATE A FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_05;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }


        // CREATE A FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_06);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_06;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }


        // CREATE A FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_07);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_07;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }


        // CREATE A FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_08);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_08;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }


        // CREATE A FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_09);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_09;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }


        // CREATE A FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_10);

        // IF THE PROFILE SLOT IS NOT USED MAKE A NEW ONE USING THIS SLOT
        if (!saveFileDataWriter.CheckToSeeIfFileExists())
        {
            // IF THIS CHARACTER SLOT IIS NOT TAKEN NAME A NEW ONE USING THIS SLOT
            currentCharacterSlotBeingUsed = CharacterSlot.CharacterSlot_10;
            currentCharacterData = new CharacterSaveData();
            NewGame();
            return;
        }

        // IF THERE ARE NO FREE SLOTS, NOTIFY THE PLAYER
        TitleScreenManager.Instance.DisplayNoFreeCharacterSlotPopUp();

    }

    private void NewGame()
    {
        SaveGame();
        StartCoroutine(LoadWorldScene());
    }

    public void LoadGame()
    {
        // LOAD A PREVIOUS FILE, WITH A FILE NAME DEPENDING ON WHICH SLOT WE ARE USING
        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);
        Debug.Log($"Load time save file name : {saveFileName}");
        saveFileDataWriter = new SaveFileDataWriter();
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFileName = saveFileName;
        Debug.Log($"Load time save file data name : {saveFileDataWriter.saveFileName}");

        // WHY NULL?
        currentCharacterData = saveFileDataWriter.LoadSaveFile();
        Debug.Log($"World game current character data : {currentCharacterData}");
        StartCoroutine(LoadWorldScene());
    }

    public void SaveGame()
    {
        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);
        saveFileDataWriter = new SaveFileDataWriter();
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFileName = saveFileName;

        //PASS THE PLAYERS INFO, FROM GAME TO THEIR SAVE FILE
        player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

        // WRITE THAT INFO ONTOA JSON FILE
        saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);

    }

    public void DeleteGame(CharacterSlot characterSlot)
    {
        saveFileDataWriter = new SaveFileDataWriter();
        saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFileName = saveFileName;

        saveFileDataWriter.DeleteSaveFile();

    }

    // LOAD ALL CHARACTER PROFILES ON DEVICE WHEN STARTING ONE
    private void LoadAllCharacterSlots()
    {
        saveFileDataWriter = new SaveFileDataWriter();
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_01);
        characterSlot01 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_02);
        characterSlot02 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_03);
        characterSlot03 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_04);
        characterSlot04 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_05);
        characterSlot05 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_06);
        characterSlot06 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_07);
        characterSlot07 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_08);
        characterSlot08 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_09);
        characterSlot09 = saveFileDataWriter.LoadSaveFile();

        saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot.CharacterSlot_10);
        characterSlot10 = saveFileDataWriter.LoadSaveFile();
    }

    public IEnumerator LoadWorldScene()
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(worldSceneIndex);
        //AsyncOperation loadOperation = SceneManager.LoadSceneAsync(currentCharacterData.sceneIndex);

        player.LoadGameDataFromCurrentCharacterData(ref currentCharacterData);

        yield return null;
    }

    public int GetWorldSceneIndex()
    {
        return worldSceneIndex;
    }
}
