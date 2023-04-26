using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitPanel : MonoBehaviour
{
    [Header("==== Lista Buttonów ====")]
    [Space(10)]
    [SerializeField] private Button[] buttons;
    [Header("==== Buttony ====")]
    [Space(10)]
    [SerializeField] public Button YESButton;
    [SerializeField] public Button BackButton;
    [SerializeField] public Button NOButton;    
    [Header("==== UI Elements ====")]
    [Space(10)]
    [SerializeField] private GameObject exitPanel;
    [Header("==== Another Scripts ====")]
    [Space(10)]
    [SerializeField] private MainMenuController mainMenuController;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PauseMenu pauseMenu;
    [Header("==== Anothers ====")]
    [Space(10)]
    private int currentIndex = 0;
    public bool inExitPanel = false;
    private bool isMouseOverButton = false;

    public void Update()
    {
        HandleMainMenuPanelInput();
        SetButtonToFirst();
    }
    public void SetButtonToFirst()
    {
        if (inExitPanel)
        {
            EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
            ToggleAttachedObject(buttons[currentIndex], true);
        }
    }
    public void HandleMainMenuPanelInput()
    {
        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            AudioManager.Instance.PlaySFX("ButtonSound");
            ToggleAttachedObject(selectedButton, false);
            currentIndex--;
            if (currentIndex < 0)
            {
                currentIndex = buttons.Length - 1;
            }

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AudioManager.Instance.PlaySFX("ButtonSound");
            ToggleAttachedObject(selectedButton, false);
            currentIndex++;
            if (currentIndex >= buttons.Length)
            {
                currentIndex = 0;
            }
        }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            bool anyButtonHovered = false;
            for (int i = 0; i < buttons.Length; i++)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(buttons[i].GetComponent<RectTransform>(), Input.mousePosition))
                {
                    if (!isMouseOverButton)
                    {
                        AudioManager.Instance.PlaySFX("ButtonSound");
                        isMouseOverButton = true;
                    }
                    anyButtonHovered = true;

                    ToggleAttachedObject(selectedButton, false);
                    currentIndex = i;
                    selectedButton = buttons[currentIndex];
                    ToggleAttachedObject(selectedButton, true);
                    break;
                }

            }
            if (!anyButtonHovered) // if no button is hovered, set the boolean to false
            {
                isMouseOverButton = false;
            }
        }
        else
        {
            isMouseOverButton = false;
        }
        Submit();
    }
    private void Submit()
    {
        Button selectedButton = buttons[currentIndex];
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (selectedButton == YESButton)
            {
                YESButton.onClick.Invoke();
            }
            else if (selectedButton == NOButton)
            {
                NOButton.onClick.Invoke();
            }
            else if (selectedButton == BackButton)
            {
                BackButton.onClick.Invoke();
            }
        }
    }
    private void ToggleAttachedObject(Button button, bool state)
    {
        GameObject attachedObject = button.gameObject.transform.GetChild(0).gameObject;
        attachedObject.SetActive(state);
    }

    public void OnYesExitButton()
    {
        if (!mainMenuController.inMainMenu)
        {
            Application.Quit();
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void OnNoExitButton()
    {
        if (!mainMenuController.inMainMenu)
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            inExitPanel = false;
            mainMenuController.inMainMenu = true;
            mainMenuController.grayOutPanel.SetActive(false);
            exitPanel.SetActive(false);
            mainMenuController.mainMenuImage.SetActive(true);
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void OnExitGameButton()
    {
        if (!pauseMenu.inPauseMenu)
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            inExitPanel = false;
            pauseMenu.inPauseMenu = true;
            pauseMenu.grayOutPanel.SetActive(false);
            exitPanel.SetActive(false);
            pauseMenu.pauseMenuGameObject.SetActive(false);
            Application.Quit();
            Debug.Log("wychodzisz z gry");
        }
        else
        {
            Debug.Log("!");
        }
    }
    public void OnMainMenuButton()
    {
        if (!pauseMenu.inPauseMenu)
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            SceneManager.LoadScene("MainMenu");
            pauseMenu.grayOutPanel.SetActive(false);
            exitPanel.SetActive(false);
            pauseMenu.pauseMenuGameObject.SetActive(false);
            pauseMenu.inPauseMenu = true;
            inExitPanel = false;
            pauseMenu.ResumeGame();
        }
        else
        {
            Debug.Log("!");
        }
    }

    public void OnBackButton()
    {
        if (!pauseMenu.inPauseMenu)
        {
            AudioManager.Instance.PlaySFX("InterfaceBACK");
            inExitPanel = false;
            pauseMenu.inPauseMenu = true;
            pauseMenu.grayOutPanel.SetActive(false);
            exitPanel.SetActive(false);
        }
        else
        {
            Debug.Log("!");
        }
    }
}
