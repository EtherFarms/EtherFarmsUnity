using UnityEngine;
using UnityEngine.UI;
using Thirdweb;
using TMPro;
using System.Threading.Tasks;
using System;

public class UIManager : SingletonMonobehaviour<UIManager>
{

    private bool _pauseMenuOn = false;
    [SerializeField] private UIInventoryBar uiInventoryBar = null;
    [SerializeField] private PauseMenuInventoryManagement pauseMenuInventoryManagement = null;
    [SerializeField] private GameObject pauseMenu = null;
    [SerializeField] private GameObject[] menuTabs = null;
    [SerializeField] private Button[] menuButtons = null;

    public bool PauseMenuOn { get => _pauseMenuOn; set => _pauseMenuOn = value; }

    public TextMeshProUGUI textMeshPro;
    public TextMeshProUGUI textMeshPro2;

    protected override void Awake()
    {
        base.Awake();

        pauseMenu.SetActive(false);
    }

    private async void Start()
    {
        textMeshPro2.text = SliceString(PlayerPrefs.GetString("wallet_address"), 10, 5);
        textMeshPro.text = SliceString(PlayerPrefs.GetString("wallet_address"), 10, 5);
    }

    // Update is called once per frame
    private void Update()
    {
        PauseMenu();
        textMeshPro2.text = SliceString(PlayerPrefs.GetString("wallet_address"), 10, 5);
        textMeshPro.text = SliceString(PlayerPrefs.GetString("wallet_address"), 10, 5);
    }

    public static string SliceString(string input, int firstChars, int lastChars)
    {
        if (string.IsNullOrEmpty(input))
        {
            return "";
        }

        if (input.Length <= firstChars + lastChars + 3) // +3 for "..."
        {
            return input;
        }

        string firstPart = input.Substring(0, Math.Min(firstChars, input.Length));
        string lastPart = input.Substring(input.Length - Math.Min(lastChars, input.Length));

        return firstPart + "..." + lastPart;
    }

    private void PauseMenu()
    {
        // Toggle pause menu if escape is pressed

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (PauseMenuOn)
            {
                DisablePauseMenu();
            }
            else
            {
                EnablePauseMenu();
            }
        }
    }

    private void EnablePauseMenu()
    {
        // Destroy any currently dragged items
        uiInventoryBar.DestroyCurrentlyDraggedItems();

        // Clear currently selected items
        uiInventoryBar.ClearCurrentlySelectedItems();

        PauseMenuOn = true;
        Player.Instance.PlayerInputIsDisabled = true;
        Time.timeScale = 0;
        pauseMenu.SetActive(true);

        // Trigger garbage collector
        System.GC.Collect();

        // Highlight selected button
        HighlightButtonForSelectedTab();
    }

    public void DisablePauseMenu()
    {
        // Destroy any currently dragged items
        pauseMenuInventoryManagement.DestroyCurrentlyDraggedItems();

        PauseMenuOn = false;
        Player.Instance.PlayerInputIsDisabled = false;
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    private void HighlightButtonForSelectedTab()
    {
        for (int i = 0; i < menuTabs.Length; i++)
        {
            if (menuTabs[i].activeSelf)
            {
                SetButtonColorToActive(menuButtons[i]);
            }

            else
            {
                SetButtonColorToInactive(menuButtons[i]);
            }
        }
    }

    private void SetButtonColorToActive(Button button)
    {
        ColorBlock colors = button.colors;

        colors.normalColor = colors.pressedColor;

        button.colors = colors;

    }

    private void SetButtonColorToInactive(Button button)
    {
        ColorBlock colors = button.colors;

        colors.normalColor = colors.disabledColor;

        button.colors = colors;

    }

    public void SwitchPauseMenuTab(int tabNum)
    {
        for (int i = 0; i < menuTabs.Length; i++)
        {
            if (i != tabNum)
            {
                menuTabs[i].SetActive(false);
            }
            else
            {
                menuTabs[i].SetActive(true);

            }
        }

        HighlightButtonForSelectedTab();

    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
