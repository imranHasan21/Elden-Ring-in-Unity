using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class TitleScreenManager : MonoBehaviour
{
    public static TitleScreenManager Instance;

    [Header("Menus")]
    [SerializeField] GameObject titleScreenMainMenu;
    [SerializeField] GameObject titleScreenLoadMenu;

    [Header("Buttons")]
    [SerializeField] Button loadMenuReturnButton;
    [SerializeField] Button mainMenuLoadGameButton;
    [SerializeField] Button mainMenuNewGameButton;
    [SerializeField] Button deleteCharacterPopUpConfirmButton;

    [Header("Pop Ups")]
    [SerializeField] GameObject noCharacterSlotPopUp;
    [SerializeField] Button noCharacterSlotOkayButton;
    [SerializeField] GameObject deleteCharacterSlotPopUp;

    [Header("Save Slots")]
    public CharacterSlot currentSelectedSlot = CharacterSlot.NO_SLOT;

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

    // private void Start()
    // {
    //     DontDestroyOnLoad(gameObject);
    // }

    public void StartGameAsHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartNewGame()
    {
        WorldSavedGameManager.Instance.AttemptToCreateNewGame();
    }

    public void OpenLoadGameMenu()
    {
        // CLOSE MAIN MENU
        titleScreenMainMenu.SetActive(false);

        // OPEN LOAD MENU
        titleScreenLoadMenu.SetActive(true);

        // SELECT THE RETURN BUTTON FIRST
        loadMenuReturnButton.Select();
    }

    public void CloseLoadGameMenu()
    {
        // CLOSE LOAD MENU
        titleScreenLoadMenu.SetActive(false);

        // OPEN MAIN MENU
        titleScreenMainMenu.SetActive(true);

        // SELECT THE LOAD BUTTON
        mainMenuLoadGameButton.Select();

    }

    public void DisplayNoFreeCharacterSlotPopUp()
    {
        noCharacterSlotPopUp.SetActive(true);
        noCharacterSlotOkayButton.Select();
    }

    public void CloseNoCharacterSlotPopUp()
    {
        noCharacterSlotPopUp.SetActive(false);
        mainMenuNewGameButton.Select();
    }

    public void SelectCharacterSlot(CharacterSlot characterSlot)
    {
        currentSelectedSlot = characterSlot;
    }

    public void SelectNoSlot()
    {
        currentSelectedSlot = CharacterSlot.NO_SLOT;
    }

    public void AttemptToDeleteCharacterSlot()
    {
        if (currentSelectedSlot != CharacterSlot.NO_SLOT)
        {
            deleteCharacterSlotPopUp.SetActive(true);
            deleteCharacterPopUpConfirmButton.Select();
        }
    }

    public void DeleteCharacterSlot()
    {
        deleteCharacterSlotPopUp.SetActive(false);
        WorldSavedGameManager.Instance.DeleteGame(currentSelectedSlot);

        // WE DISABLE AND THEN ENABLE THE LOAD MENU TO REFRESH THE SLOT
        titleScreenLoadMenu.SetActive(false);
        titleScreenLoadMenu.SetActive(true);

        loadMenuReturnButton.Select();
    }

    public void CloseDeleteCharacterPopUp()
    {
        deleteCharacterSlotPopUp.SetActive(false);
        loadMenuReturnButton.Select();
    }
}
