using TMPro;
using UnityEngine;

public class UI_Character_Save_Slot : MonoBehaviour
{
    SaveFileDataWriter saveFileWriter;

    [Header("Game Slots")]
    public CharacterSlot characterSlot;

    [Header("Character Info")]
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI timePlayer;

    private void OnEnable()
    {
        LoadSaveSlots();
    }

    private void LoadSaveSlots()
    {
        saveFileWriter = new SaveFileDataWriter();
        saveFileWriter.saveDataDirectoryPath = Application.persistentDataPath;

        if (characterSlot == CharacterSlot.CharacterSlot_01)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot01.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_02)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot02.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_03)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot03.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_04)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot04.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_05)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot05.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_06)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot06.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_07)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot07.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_08)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot08.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_09)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot09.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
        else if (characterSlot == CharacterSlot.CharacterSlot_10)
        {
            saveFileWriter.saveFileName = WorldSavedGameManager.Instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

            if (saveFileWriter.CheckToSeeIfFileExists())
            {
                characterName.text = WorldSavedGameManager.Instance.characterSlot10.characterName;
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

    }

    public void LoadGameFromCharacterSlot()
    {
        WorldSavedGameManager.Instance.currentCharacterSlotBeingUsed = characterSlot;
        WorldSavedGameManager.Instance.LoadGame();
    }

    public void SelectCurrentSlot()
    {
        TitleScreenManager.Instance.SelectCharacterSlot(characterSlot);
    }
}
